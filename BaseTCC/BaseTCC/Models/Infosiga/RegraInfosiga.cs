using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Models.Infosiga
{
    public partial class RegraInfosiga
    {
        [JsonProperty("Quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("Porcentagem")]
        public int Porcentagem { get; set; }

        [JsonProperty("TipoAcidente")]
        public string TipoAcidente { get; set; }

        [JsonProperty("TipoDeVia")]
        public string TipoDeVia { get; set; }

    }
}
