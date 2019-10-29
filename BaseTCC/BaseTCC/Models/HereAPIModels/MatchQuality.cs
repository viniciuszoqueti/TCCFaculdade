using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class MatchQuality
    {
        [JsonProperty("Country")]
        public long Country { get; set; }

        [JsonProperty("City")]
        public long City { get; set; }
    }
}
