using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Response
    {
        [JsonProperty("metaInfo")]
        public MetaInfo MetaInfo { get; set; }

        [JsonProperty("route")]
        public Route[] Route { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("View")]
        public View[] View { get; set; }


    }
}
