using BaseTCC.Models.HereAPIModels;
using BaseTCC.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace BaseTCC.Models.App
{
    public class Cidade
    {

        private const string URL_LOCAL = "https://geocoder.api.here.com/6.2/geocode.json?&&city={0}&country=BRA&gen=9&app_id={1}&app_code={2}";
        private const string URL_DISTANCIA = "https://route.api.here.com/routing/7.2/calculateroute.json?waypoint0={0}%2C{1}&waypoint1={2}%2C{3}&mode=fastest%3Bcar&language=en-us&metricSystem=metric&app_id={4}&app_code={5}";

        public string Nome { get; private set; }
        public float Lat { get; private set; }
        public float Long { get; private set; }

        public HashSet<Local> Locais { get; set; }



        public Cidade(HashSet<Local.LocalAdpter> Locais, string Nome, float latitude, float logitude)
        {
            this.Nome = Nome;
            this.Lat = latitude;
            this.Long = logitude;
            this.Locais = new HashSet<Local>(Locais);
        }
        public Cidade(string Nome)
        {
            this.Nome = Nome;
            this.Locais = new HashSet<Local>();
        }
        public Cidade(string Nome, float latitude, float logitude)
        {
            this.Nome = Nome;
            this.Lat = latitude;
            this.Long = logitude;
            this.Locais = new HashSet<Local>();
        }
        public static Cidade BuscaCidade(string cidadeNome)
        {
            Cidade cidade = null;
            HereAPI hereAPI = new HereAPI();
            string urlApiLocal = string.Format(URL_LOCAL, cidadeNome, HereAPI.APP_ID, HereAPI.APP_CODE, HereAPI.APP_ID, HereAPI.APP_CODE);

            string respostaJson = hereAPI.RestHttpGet(urlApiLocal);
            Response response = HereRoutingApi.FromJson(respostaJson).Response;

            // pesquisa a lat e long de cada cidade 
            if (response != null)
            {
                if (response.View != null)
                {
                    if (response.View[0].Result != null)
                    {
                        if (response.View[0].Result[0].Location != null)
                        {
                            if (response.View[0].Result[0].Location.DisplayPosition != null)
                            {
                                DisplayPosition displayPosition = response.View[0].Result[0].Location.DisplayPosition;
                                string cidadeNomeAux = response.View[0].Result[0].Location.Address.Label;
                                cidade = new Cidade(cidadeNomeAux);
                                cidade.Lat = (float)displayPosition.Latitude;
                                cidade.Long = (float)displayPosition.Longitude;
                            }
                        }
                    }
                }
            }

            return cidade;

        }
        public static HashSet<CidadeAdper> CalcularDistancias(HashSet<Cidade> cidades)
        {

            HashSet<CidadeAdper> newcidades = new HashSet<CidadeAdper>();

            foreach (Cidade cidade in cidades)
            {
                HereAPI hereAPI = new HereAPI();

                CidadeAdper newCidade = new CidadeAdper(cidade);
                HashSet<Local> newLocaisCidade = new HashSet<Local>();

                // adiciona distancia do ponto central da cidade até cada referencia
                foreach (Local local in cidade.Locais)
                {
                    hereAPI = new HereAPI();
                    string newUrlApi = string.Format(URL_DISTANCIA, cidade.Lat.ToString().Replace(",", "."), cidade.Long.ToString().Replace(",", "."), local.Lat.ToString().Replace(",", "."), local.Long.ToString().Replace(",", "."), HereAPI.APP_ID, HereAPI.APP_CODE);
                    Response response2 = HereRoutingApi.FromJson(hereAPI.RestHttpGet(newUrlApi)).Response;

                    if (response2 != null)
                    {
                        if (response2.Route != null)
                        {
                            if (response2.Route[0].Summary != null)
                            {
                                float distancia = response2.Route[0].Summary.Distance;
                                newLocaisCidade.Add(new Local.LocalAdpter(local.Nome, local.Lat, local.Long, distancia));

                            }
                        }
                    }
                }

                // adiciona os locais atualizados a cidade
                newCidade.Locais = newLocaisCidade;


                foreach (Local local in newCidade.Locais)
                {

                    // calcula a distancia entre os pontos da cidade
                    foreach (Local localAux in newCidade.Locais)
                    {
                        if (!local.Equals(localAux))
                        {
                            hereAPI = new HereAPI();
                            string newUrlApiLocais = string.Format(URL_DISTANCIA, local.Lat.ToString().Replace(",", "."), local.Long.ToString().Replace(",", "."), localAux.Lat.ToString().Replace(",", "."), localAux.Long.ToString().Replace(",", "."), HereAPI.APP_ID, HereAPI.APP_CODE);

                            Response response3 = HereRoutingApi.FromJson(hereAPI.RestHttpGet(newUrlApiLocais)).Response;

                            if (response3 != null)
                            {
                                if (response3.Route != null)
                                {
                                    if (response3.Route[0].Summary != null)
                                    {
                                        float distancia = response3.Route[0].Summary.Distance;
                                        ((Local.LocalAdpter)local).DistanciaLocais.Add(new Local.LocalAdpter(localAux.Nome, localAux.Lat, localAux.Long, distancia));

                                    }
                                }
                            }
                        }
                    }


                }

                // calcula a distancia entre as cidades

                foreach (Cidade cidadeAux in cidades)
                {
                    if (!cidade.Equals(cidadeAux))
                    {
                        hereAPI = new HereAPI();
                        string newUrlApi = string.Format(URL_DISTANCIA, cidade.Lat.ToString().Replace(",", "."), cidade.Long.ToString().Replace(",", "."), cidadeAux.Lat.ToString().Replace(",", "."), cidadeAux.Long.ToString().Replace(",", "."), HereAPI.APP_ID, HereAPI.APP_CODE);

                        Response response2 = HereRoutingApi.FromJson(hereAPI.RestHttpGet(newUrlApi)).Response;

                        if (response2 != null)
                        {
                            if (response2.Route != null)
                            {
                                if (response2.Route[0].Summary != null)
                                {
                                    float distancia = response2.Route[0].Summary.Distance;
                                    newCidade.DistanciaCidades.Add(new CidadeAdper(cidadeAux.Nome, cidadeAux.Lat, cidadeAux.Long, distancia));

                                }
                            }
                        }
                    }
                }


                newcidades.Add(newCidade);

            }


            return newcidades;

        }
        public override bool Equals(object obj)
        {
            return obj is Cidade cidade &&
                   Nome == cidade.Nome &&
                   Lat == cidade.Lat &&
                   Long == cidade.Long;
        }
        public override int GetHashCode()
        {
            var hashCode = 219872753;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            hashCode = hashCode * -1521134295 + Lat.GetHashCode();
            hashCode = hashCode * -1521134295 + Long.GetHashCode();
            return hashCode;
        }
        public override string ToString()
        {
            return Nome;
        }
        public class CidadeAdper : Cidade
        {
            public float Distance { get; set; }
            public HashSet<CidadeAdper> DistanciaCidades { get; set; }

            [JsonConstructor]
            public CidadeAdper(float Distance, HashSet<CidadeAdper> DistanciaCidades, string Nome, float Lat, float Long, HashSet<Local.LocalAdpter> Locais) : base(Locais, Nome, Lat, Long)
            {
                this.Distance = Distance;
                this.DistanciaCidades = DistanciaCidades;
            }
            public CidadeAdper(Cidade cidade) : base(cidade.Nome, cidade.Lat, cidade.Long)
            {
                base.Locais = cidade.Locais;
                this.DistanciaCidades = new HashSet<CidadeAdper>();
            }
            public CidadeAdper(string Nome, float Lat, float Long, float Distance) : base(Nome, Lat, Long)
            {
                this.Distance = Distance;
                this.DistanciaCidades = new HashSet<CidadeAdper>();
            }

            public override string ToString()
            {
                if (Distance == 0) return base.Nome;

                return this.Distance + " - " + base.Nome;
            }
        }



    }

}
