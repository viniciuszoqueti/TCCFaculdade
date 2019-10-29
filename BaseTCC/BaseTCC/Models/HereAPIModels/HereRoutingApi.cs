using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace BaseTCC.Models.HereAPIModels
{

    public partial class HereRoutingApi
    {
        [JsonProperty("response")]
        public Response Response { get; set; }

        public static HereRoutingApi FromJson(string json) => JsonConvert.DeserializeObject<HereRoutingApi>(json, Converter.Settings);

    }

    public static class Serialize
    {
        public static string ToJson(this HereRoutingApi self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}