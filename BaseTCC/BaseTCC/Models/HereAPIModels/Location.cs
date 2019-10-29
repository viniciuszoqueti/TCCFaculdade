using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Location
    {
        [JsonProperty("LocationId")]
        public string LocationId { get; set; }

        [JsonProperty("LocationType")]
        public string LocationType { get; set; }

        [JsonProperty("DisplayPosition")]
        public DisplayPosition DisplayPosition { get; set; }

        [JsonProperty("NavigationPosition")]
        public DisplayPosition[] NavigationPosition { get; set; }

        [JsonProperty("MapView")]
        public MapView MapView { get; set; }

        [JsonProperty("Address")]
        public Address Address { get; set; }
    }
}
