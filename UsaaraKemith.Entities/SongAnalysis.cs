using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UsaaraKemith.Entities
{
    public class SongAnalysis
    {
        public SongAnalysis()
        {
            FilteredSegments = new List<Segment>();
        }
        [JsonProperty("tatums")]
        public List<Segment> Tatums { get; set; }

        [JsonProperty("beats")]
        public List<Segment> Beats { get; set; }

        [JsonProperty("bars")]
        public List<Segment> Bars { get; set; }

        [JsonProperty("segments")]
        public List<Segment> Segments { get; set; }

        [JsonProperty("sections")]
        public List<Segment> Sections { get; set; }

        [JsonIgnore]
        public List<Segment> FilteredSegments { get; set; }
    }
}
