using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.Infosiga
{
    public partial class Obito
    {
        [JsonProperty("CidadeConsiderada")]
        public string CidadeConsiderada { get; set; }

        [JsonProperty("TipoAcidente")]
        public string TipoAcidente { get; set; }

        [JsonProperty("TipoDeVia")]
        public string TipoDeVia { get; set; }

        [JsonProperty("Localizacao")]
        public string Localizacao { get; set; }

        [JsonProperty("Lat")]
        public string Lat { get; set; }

        [JsonProperty("Long")]
        public string Long { get; set; }

    }
}
