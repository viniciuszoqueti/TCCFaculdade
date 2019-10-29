using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Route
    {
        [JsonProperty("waypoint")]
        public Waypoint[] Waypoint { get; set; }

        [JsonProperty("mode")]
        public Mode Mode { get; set; }

        [JsonProperty("leg")]
        public Leg[] Leg { get; set; }

        [JsonProperty("summary")]
        public Summary Summary { get; set; }

        [JsonProperty("shape")]
        public List<string> Shape { get; set; }
    }
}
