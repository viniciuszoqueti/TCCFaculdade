using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class AdditionalDatum
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
