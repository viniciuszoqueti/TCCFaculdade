using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Maneuver
    {
        [JsonProperty("position")]
        public Position Position { get; set; }

        [JsonProperty("instruction")]
        public string Instruction { get; set; }

        [JsonProperty("travelTime")]
        public long TravelTime { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }
    }
}
