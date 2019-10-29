using BaseTCC.Models.App;
using BaseTCC.Models.Infosiga;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static BaseTCC.Models.App.Cidade;
using Newtonsoft.Json;
using System.Linq;
using System.Globalization;
using System.Net;
using BaseTCC.Models.HereAPIModels;
using static BaseTCC.Models.App.Local;

namespace BaseTCC.Utils
{
    public static class Infosiga
    {
        private const string URL_ROTA = "https://route.api.here.com/routing/7.2/calculateroute.json?waypoint0={0},{1}&waypoint1={2},{3}&mode=fastest%3Bcar%3Btraffic%3Aenabled&routeattributes=sh&maneuverattributes=di%2Csh&app_id={4}&app_code={5}";

        public static HashSet<Cidade.CidadeAdper> AplicarCaminhoMacro(HashSet<Cidade.CidadeAdper> cidades)
        {
            List<Obito> obitos = new List<Obito>();
            List<RegraInfosiga> regras = new List<RegraInfosiga>();

            string caminho = @"..\..\..\BaseTCC\BaseTCC\Media\";

            StreamReader regrasInfosiga = new StreamReader(caminho + "RegrasInfosiga.json");
            string conteudoRegrasInfosiga = regrasInfosiga.ReadToEnd();

            StreamReader jsonInfosiga = new StreamReader(caminho + "JsonInfosiga.json");
            string conteudoInfosiga = jsonInfosiga.ReadToEnd();

            obitos = JsonConvert.DeserializeObject<List<Obito>>(conteudoInfosiga);

            regras = JsonConvert.DeserializeObject<List<RegraInfosiga>>(conteudoRegrasInfosiga);

            foreach (Cidade.CidadeAdper cidade in cidades)
            {

                var nomeCidadeInicialCompleto = cidade.Nome.Split(',');
                string cidadeInicial = RemoverAcentuacao(nomeCidadeInicialCompleto[0]).ToUpper();

                var listaCidadeInicial = obitos.FindAll(o => o.CidadeConsiderada.Contains(cidadeInicial));

                foreach (CidadeAdper cidadesDistancia in cidade.DistanciaCidades)
                {
                    var nomeCidadeFinalCompleto = cidadesDistancia.Nome.Split(',');
                    string cidadeFinal = RemoverAcentuacao(nomeCidadeFinalCompleto[0]).ToUpper();

                    var listaCidadeFinal = obitos.FindAll(o => o.CidadeConsiderada.Contains(cidadeFinal));

                    List<string> rota = GerarRota(cidade, cidadesDistancia);

                    List<KeyValuePair<string, string>> pontuacao1 = ValidarQntObitos(rota, listaCidadeInicial.FindAll(o => o.TipoDeVia.Contains("Rodovias")), 1);
                    List<KeyValuePair<string, string>> pontuacao2 = ValidarQntObitos(rota, listaCidadeFinal.FindAll(o => o.TipoDeVia.Contains("Rodovias")), 1);
                    pontuacao1.AddRange(pontuacao2);

                    double aux = 0;

                    foreach (KeyValuePair<string, string> pontuacao in pontuacao1)
                    {
                        foreach (RegraInfosiga regra in regras)
                        {
                            if (regra.TipoAcidente.Equals(pontuacao.Key) && regra.TipoDeVia.Equals(pontuacao.Value))
                            {
                                aux += (double)((double)regra.Porcentagem / 100) * (cidadesDistancia.Distance / 2);
                            }
                        }
                    }

                    cidadesDistancia.Distance += (float)aux;

                }

                cidade.Locais = AplicarCaminhoMicro(cidade.Locais, listaCidadeInicial.FindAll(o => o.TipoDeVia.Contains("Vias Municipais")), regras);
            }

            return cidades;
        }

        public static HashSet<Local> AplicarCaminhoMicro(HashSet<Local> locais, List<Obito> lista, List<RegraInfosiga> regras)
        {

            foreach (LocalAdpter local in locais)
            {

                foreach (LocalAdpter locaisDistancia in local.DistanciaLocais)
                {

                    List<string> rota = GerarRotaMicro(local, locaisDistancia);

                    List<KeyValuePair<string, string>> pontuacao1 = ValidarQntObitos(rota, lista, 0.1);

                    double aux = 0;

                    foreach (KeyValuePair<string, string> pontuacao in pontuacao1)
                    {
                        foreach (RegraInfosiga regra in regras)
                        {
                            if (regra.TipoAcidente.Equals(pontuacao.Key) && regra.TipoDeVia.Equals(pontuacao.Value))
                            {
                                aux += (double)((double)regra.Porcentagem / 100) * (locaisDistancia.DistanciaReferencia / 2);
                            }
                        }
                    }

                    locaisDistancia.DistanciaReferencia += (float)aux;
                }

            }

            return locais;
        }

        public static string RemoverAcentuacao(this string text)
        {
            return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        public static List<string> GerarRota(Cidade.CidadeAdper cidade, CidadeAdper cidadesDistancia)
        {
            HereAPI hereAPI = new HereAPI();
            string newUrlApi = string.Format(URL_ROTA, cidade.Lat.ToString().Replace(",", "."), cidade.Long.ToString().Replace(",", "."), cidadesDistancia.Lat.ToString().Replace(",", "."), cidadesDistancia.Long.ToString().Replace(",", "."), HereAPI.APP_ID, HereAPI.APP_CODE);

            Response response2 = HereRoutingApi.FromJson(hereAPI.RestHttpGet(newUrlApi)).Response;

            return response2.Route[0].Shape;
        }

        public static List<string> GerarRotaMicro(LocalAdpter local, LocalAdpter localDistancia)
        {
            HereAPI hereAPI = new HereAPI();
            string newUrlApi = string.Format(URL_ROTA, local.Lat.ToString().Replace(",", "."), local.Long.ToString().Replace(",", "."), localDistancia.Lat.ToString().Replace(",", "."), localDistancia.Long.ToString().Replace(",", "."), HereAPI.APP_ID, HereAPI.APP_CODE);

            Response response2 = HereRoutingApi.FromJson(hereAPI.RestHttpGet(newUrlApi)).Response;

            return response2.Route[0].Shape;
        }

        private static double Distance(double lat1, double lon1, double lat2, double lon2, String unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == "K")
            {
                dist = dist * 1.609344;
            }
            else if (unit == "N")
            {
                dist = dist * 0.8684;
            }

            return (dist);
        }

        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        /*::    This function converts decimal degrees to radians                         :*/
        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        /*::    This function converts radians to decimal degrees                         :*/
        /*:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::*/
        private static double rad2deg(double rad)
        {
            return (rad * 180 / Math.PI);
        }

        private static List<KeyValuePair<string, string>> ValidarQntObitos(List<string> rota, List<Obito> lista, double km)
        {
            List<KeyValuePair<string, string>> pontuacao = new List<KeyValuePair<string, string>>();

            foreach (Obito obito in lista)
            {

                double lat1 = double.Parse(obito.Lat.Replace(".", ","));
                double lng1 = double.Parse(obito.Long.Replace(".", ","));

                bool flag = false;

                foreach (string ponto in rota)
                {
                    string[] coordenadas = ponto.Split(',');

                    double lat2 = double.Parse(coordenadas[0].Replace(".", ","));
                    double lng2 = double.Parse(coordenadas[1].Replace(".", ","));

                    double distancia = Distance(lat1, lng1, lat2, lng2, "k");

                    if (distancia <= km)
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    pontuacao.Add(new KeyValuePair<string, string>(obito.TipoAcidente, obito.TipoDeVia));
                }
            }

            return pontuacao;
        }


    }
}
