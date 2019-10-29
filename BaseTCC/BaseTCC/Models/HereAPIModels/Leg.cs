using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Leg
    {
        [JsonProperty("start")]
        public Waypoint Start { get; set; }

        [JsonProperty("end")]
        public Waypoint End { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }

        [JsonProperty("travelTime")]
        public long TravelTime { get; set; }

        [JsonProperty("maneuver")]
        public Maneuver[] Maneuver { get; set; }
    }
}
