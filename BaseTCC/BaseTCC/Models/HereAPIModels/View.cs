using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class View
    {
        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("ViewId")]
        public long ViewId { get; set; }

        [JsonProperty("Result")]
        public Result[] Result { get; set; }
    }
}
