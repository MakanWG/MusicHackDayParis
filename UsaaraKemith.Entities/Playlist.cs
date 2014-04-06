using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UsaaraKemith.Entities
{
    public class Playlist
    {
        public Playlist()
        {

        }

        [JsonProperty("tracks")]
        public Track[] Tracks { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
