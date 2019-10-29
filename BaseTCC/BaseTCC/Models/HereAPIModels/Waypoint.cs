using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Waypoint
    {
        [JsonProperty("linkId")]
        public string LinkId { get; set; }

        [JsonProperty("mappedPosition")]
        public Position MappedPosition { get; set; }

        [JsonProperty("originalPosition")]
        public Position OriginalPosition { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("spot")]
        public double Spot { get; set; }

        [JsonProperty("sideOfStreet")]
        public string SideOfStreet { get; set; }

        [JsonProperty("mappedRoadName")]
        public string MappedRoadName { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("shapeIndex")]
        public long ShapeIndex { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }
}
