using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UsaaraKemith.Entities
{
    public class Track
    {
        public Track()
        {

        }

        [JsonProperty("id")]
        public Int64 Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("stream_url")]
        public string StreamUrl { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("artwork_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("playback_count")]
        public long ViewCount { get; set; }

        [JsonProperty("favoritings_count")]
        public long LikesCount { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("playlist")]
        public string AssociatedPlaylist { get; set; }
    }
}
