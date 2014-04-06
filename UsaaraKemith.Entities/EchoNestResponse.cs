using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UsaaraKemith.Entities
{
    public class EchoNestResponseWrapper
    {
        [JsonProperty("response")]
        public EchoNestResponse Response { get; set; }
    }

    public class EchoNestTrack
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("audio_summary")]
        public EchoNestSummary AudioSummary { get; set; }
    }

    public class EchoNestSummary
    {
        [JsonProperty("analysis_url")]
        public string AnalysisUrl { get; set; }
    }

    public class EchoNestResponse
    {
        //[JsonProperty("status")]
        //public string Status { get; set; }

        //[JsonProperty("code")]
        //public int Code { get; set; }

        [JsonProperty("track")]
        public EchoNestTrack Track { get; set; }
    }
}
