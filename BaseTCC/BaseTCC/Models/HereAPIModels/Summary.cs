using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Summary
    {
        [JsonProperty("distance")]
        public long Distance { get; set; }

        [JsonProperty("trafficTime")]
        public long TrafficTime { get; set; }

        [JsonProperty("baseTime")]
        public long BaseTime { get; set; }

        [JsonProperty("flags")]
        public string[] Flags { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("travelTime")]
        public long TravelTime { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }
    }
}
