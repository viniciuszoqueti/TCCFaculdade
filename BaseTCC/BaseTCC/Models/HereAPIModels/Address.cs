using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.HereAPIModels
{
    public partial class Address
    {
        [JsonProperty("Label")]
        public string Label { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("AdditionalData")]
        public AdditionalDatum[] AdditionalData { get; set; }
    }
}
