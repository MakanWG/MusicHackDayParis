using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UsaaraKemith.Entities
{
    public class Segment
    {
        [JsonProperty("start")]
        public double Start { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("timbre")]
        public List<double> Timbre { get; set; }

        [JsonIgnore]
        public int Place { get; set; }

        [JsonIgnore]
        public Segment Previous { get; set; }

        [JsonIgnore]
        public Segment Next { get; set; }

        [JsonIgnore]
        public List<Segment> Children { get; set; }

        [JsonIgnore]
        public int PlaceInParent { get; set; }

        [JsonIgnore]
        public Segment FirstOverlappin { get; set; }

        [JsonIgnore]
        public List<Segment> AllOverlappin { get; set; }

        [JsonIgnore]
        public List<Segment> Similars { get; set; }

        [JsonIgnore]
        public int SimilarsPlace { get; set; }

        public Segment()
        {
            Children = new List<Segment>();
            AllOverlappin = new List<Segment>();
            Similars = new List<Segment>();
            SimilarsPlace = 0;
        }
    }
}
