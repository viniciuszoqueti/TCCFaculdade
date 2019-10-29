using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class MapView
    {
        [JsonProperty("TopLeft")]
        public DisplayPosition TopLeft { get; set; }

        [JsonProperty("BottomRight")]
        public DisplayPosition BottomRight { get; set; }
    }
}
