using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class MetaInfo
    {
        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("mapVersion")]
        public string MapVersion { get; set; }

        [JsonProperty("moduleVersion")]
        public string ModuleVersion { get; set; }

        [JsonProperty("interfaceVersion")]
        public string InterfaceVersion { get; set; }

        [JsonProperty("availableMapVersion")]
        public string[] AvailableMapVersion { get; set; }

        [JsonProperty("Timestamp")]
        public string sTimestamp { get; set; }
    }
}
