using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsaaraKemith.Entities;

namespace UsaaraKemith.Presentation.Home
{
    public class SimilaritiesAlgo
    {
        private List<Segment> _bars;
        private List<Segment> _sections;
        private List<Segment> _beats;
        private List<Segment> _tatums;
        private List<Segment> _segments;

        public SimilaritiesAlgo()
        {
            
        }
        private SongAnalysis _analysis;
        public SongAnalysis Preprocess(SongAnalysis analysis)
        {
            _analysis = analysis;
            _sections = new List<Segment>();
            var count = 0;
            foreach (var section in analysis.Sections)
            {
                section.Place = count;
                if (count > 0)
                {
                    section.Previous = analysis.Sections[count - 1];
                }
                else section.Previous = null;
                if (count < analysis.Sections.Count - 1)
                {
                    section.Next = analysis.Sections[count + 1];
                }
                else section.Next = null;
                count ++;
                _sections.Add(section);
                
            }

            count = 0;
            _bars = new List<Segment>();
            foreach (var section in analysis.Bars)
            {
                section.Place = count;
                if (count > 0)
                {
                    section.Previous = analysis.Bars[count - 1];
                }
                else section.Previous = null;
                if (count < analysis.Bars.Count - 1)
                {
                    section.Next = analysis.Bars[count + 1];
                }
                else section.Next = null;
                count++;
                _bars.Add(section);
            }
            count = 0;
            _beats = new List<Segment>();
            foreach (var section in analysis.Beats)
            {
                section.Place = count;
                if (count > 0)
                {
                    section.Previous = analysis.Beats[count - 1];
                }
                else section.Previous = null;
                if (count < analysis.Beats.Count - 1)
                {
                    section.Next = analysis.Beats[count + 1];
                }
                else section.Next = null;
                count++;
                _beats.Add(section);
            }
            count = 0;
            _tatums = new List<Segment>();
            foreach (var section in analysis.Tatums)
            {
                section.Place = count;
                if (count > 0)
                {
                    section.Previous = analysis.Tatums[count - 1];
                }
                else section.Previous = null;
                if (count < analysis.Tatums.Count - 1)
                {
                    section.Next = analysis.Tatums[count + 1];
                }
                else section.Next = null;
                count++;
                _tatums.Add(section);
            }
            count = 0;
            
            _segments = new List<Segment>();
            foreach (var section in analysis.Segments)
            {
                section.Place = count;
                if (count > 0)
                {
                    section.Previous = analysis.Segments[count - 1];
                }
                else section.Previous = null;
                if (count < analysis.Segments.Count - 1)
                {
                    section.Next = analysis.Segments[count + 1];
                }
                else section.Next = null;
                count++;
                _segments.Add(section);
            }

            Connect(_sections, _bars);
            Connect(_bars, _beats);
            Connect(_beats, _tatums);
            Connect(_tatums, _segments);

            ConnextToFirstSeg(_bars);
            ConnextToFirstSeg(_beats);
            ConnextToFirstSeg(_tatums);

            ConnectToSegs(_bars);
            ConnectToSegs(_beats);
            ConnectToSegs(_tatums);

            FilterSegments();
            _analysis.Sections = _sections;
            _analysis.Bars = _bars;
            _analysis.Beats = _beats;
            _analysis.Tatums = _tatums;

            var allsimilars = 0;
            List<Segment> haveSimilars = new List<Segment>();
            for (int i = 0; i < _segments.Count; i++)
            {
                var segment1 = _segments[i];
                for (int j = 0; j < _segments.Count; j++)
                {
                    var segment2 = _segments[j];
                    if (i != j && AreSimilar(segment1, segment2))
                    {
                        segment1.Similars.Add(segment2);
                        haveSimilars.Add(segment1);
                        _segments[i].SimilarsPlace = allsimilars;
                        allsimilars++;
                    }
                }
            }
            _analysis.Segments = _segments;
            return _analysis;
        }

        private void Connect(List<Segment> parent, List<Segment> child )
        {
            foreach (var section in parent)
            {
                foreach (var item in child)
                {
                    if (item.Start >= section.Start && item.Start <= section.Start + section.Duration)
                    {
                        item.PlaceInParent = section.Children.Count;
                        section.Children.Add(item);
                    }
                    else if (item.Start > section.Start)
                    {
                        break;
                    }
                }
            }
        }

        private void ConnextToFirstSeg(List<Segment> list)
        {
            foreach (var section in list)
            {
                foreach (var segment in _segments)
                {
                    if (segment.Start >= section.Start)
                    {
                        section.FirstOverlappin = segment;
                        break;
                        
                    }
                }
            }
        }

        private void ConnectToSegs(List<Segment> list)
        {
            foreach (var section in list)
            {
                foreach (var segment in _segments)
                {
                    if (segment.Start + segment.Duration < section.Start)
                    {
                        continue;
                    }
                    if (segment.Start > section.Start + section.Duration)
                    {
                        break;
                    }
                    section.AllOverlappin.Add(segment);
                }
            }
        }

        private void FilterSegments()
        {
            var threshold = 0.3;
            _analysis.FilteredSegments.Add(_segments[0]);

            for (int i = 1; i < _segments.Count; i++)
            {
                var segment = _segments[i];
                var previous = _analysis.FilteredSegments[_analysis.FilteredSegments.Count - 1];
                if (AreSimilar(segment, previous) && segment.Confidence < threshold)
                {
                    _analysis.FilteredSegments[_analysis.FilteredSegments.Count - 1].Duration += segment.Duration;
                }
                else
                {
                    _analysis.FilteredSegments.Add(segment);
                }
            }
            
        }

        private bool AreSimilar(Segment seg1, Segment seg2)
        {
            var threshold = 1;
            var distance = TimbreDistance(seg1.Timbre, seg2.Timbre);
            return (distance < threshold);
        }

        private double TimbreDistance( List<double> firstTimbre, List<double> secondTimbre )
        {
            var sum = 0.0;
            for (var i = 0; i < 3; i++)
            {
                var delta = firstTimbre[i] - secondTimbre[i];
                sum += delta * delta;
            }
            
            return Math.Sqrt(sum); 
        }


    }
}
