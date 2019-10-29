using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Mode
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("transportModes")]
        public string[] TransportModes { get; set; }

        [JsonProperty("trafficMode")]
        public string TrafficMode { get; set; }

        [JsonProperty("feature")]
        public object[] Feature { get; set; }
    }
}
