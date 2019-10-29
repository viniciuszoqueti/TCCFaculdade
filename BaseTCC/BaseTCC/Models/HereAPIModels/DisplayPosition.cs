using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class DisplayPosition
    {
        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }
    }
}
