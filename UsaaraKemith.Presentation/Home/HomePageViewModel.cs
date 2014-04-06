using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Microsoft.Phone.BackgroundAudio;
using UsaaraKemith.Client;
using UsaaraKemith.Entities;
using WG.Presentation.WinPRT.ViewModels;

namespace UsaaraKemith.Presentation.Home
{
    public class HomePageViewModel : ViewModelBase
    {
        private SongAnalysis _analysis;
        private Track _currentTrack;
        private bool alreadylaunched;
        private int i;

        public HomePageViewModel()
        {
            Tracks = new ObservableCollection<Track>();
            AnalyzeCommand = new WGCommand(track => SelectTrack(track as Track));
        }

        public string PageBackground
        {
            get { return GetValue("PageBackground", "Blue"); }
            set { SetValue("PageBackground", value); }
        }

        public string Remaining
        {
            get { return GetValue("Remaining", string.Empty); }
            set { SetValue("Remaining", value); }
        }

        public ObservableCollection<Track> Tracks
        {
            get { return GetValue<ObservableCollection<Track>>("Tracks", null); }
            set { SetValue("Tracks", value); }
        }


        public ICommand AnalyzeCommand { get; set; }


        public override void Charged()
        {
            ObserveTracks();
        }

        private void ObserveTracks()
        {
            this.BusinessServices()
                .Song.GetPlaylistsTracks()
                .Subscribe(tracks => { Tracks = new ObservableCollection<Track>(tracks); });
        }

        private void SelectTrack(Track track)
        {
            _currentTrack = track;
            this.BusinessServices().Analyzis.Analyzis(track.StreamUrl)
                .Subscribe(analyze =>
                {
                    _analysis = new SongAnalysis();

                    _analysis = new SimilaritiesAlgo().Preprocess(analyze);
                    Start();
                });
        }

        private void StartLoop()
        {
            var watch = new Stopwatch();
            watch.Start();
            List<Segment> havesimilars = _analysis.Segments.Where(segment => segment.Similars.Any()).ToList();
            while (havesimilars[i].Previous != null)
            {
                if (alreadylaunched)
                {
                    if (BackgroundAudioPlayer.Instance.Position.TotalSeconds - 0.02 >= havesimilars[i].Start)
                    {
                        double test = BackgroundAudioPlayer.Instance.Position.TotalSeconds - 0.02;
                        Segment segmentToJump = _analysis.Segments[havesimilars[i].Place].Similars.First();


                        BackgroundAudioPlayer.Instance.Position =
                            TimeSpan.FromSeconds(segmentToJump.Start + 0.15);
                        if (havesimilars[i] != null)
                        {
                            i = segmentToJump.SimilarsPlace + 1;
                        }
                        else i = 0;

                        PageBackground = PageBackground == "Red" ? "Blue" : "Red";
                        Remaining = havesimilars[i].Start.ToString();
                    }
                }
            }
        }

        private void Start()
        {
            var source = new Uri(string.Format("{0}?client_id={1}", _currentTrack.StreamUrl,
                Constants.SoundCloud.ClientId));
            var audiotrack = new AudioTrack(source, "title", "artist", "album", null);
            BackgroundAudioPlayer.Instance.Track = audiotrack;
            BackgroundAudioPlayer.Instance.PlayStateChanged += (s, e) =>
            {
                if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing)
                {
                    if (!alreadylaunched)
                    {
                        Observable.Timer(TimeSpan.FromSeconds(_analysis.Segments[0].Start)).Subscribe(_ =>
                            StartLoop());
                        alreadylaunched = true;
                    }
                }
            };
        }


        public override void Uncharged()
        {
        }
    }
}