using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Result
    {
        [JsonProperty("Relevance")]
        public long Relevance { get; set; }

        [JsonProperty("MatchLevel")]
        public string MatchLevel { get; set; }

        [JsonProperty("MatchQuality")]
        public MatchQuality MatchQuality { get; set; }

        [JsonProperty("Location")]
        public Location Location { get; set; }
    }
}
