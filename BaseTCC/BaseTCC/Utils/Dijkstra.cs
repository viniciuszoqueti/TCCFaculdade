using BaseTCC.Models.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Utils
{
    public static class Dijkstra
    {
        public static HashSet<Cidade> Calcular(HashSet<Cidade.CidadeAdper> cidades)
        {
            HashSet<Cidade> cidadesDijkstra = new HashSet<Cidade>();
            List<Cidade.CidadeAdper> cidadesList = new List<Cidade.CidadeAdper>(cidades);



            Cidade.CidadeAdper firtCidade = cidadesList[0];
            Cidade newFirstCidade = new Cidade(firtCidade.Nome, firtCidade.Lat, firtCidade.Long);
            newFirstCidade.Locais = OrdenaLocais(firtCidade.Locais);

            cidadesDijkstra.Add(newFirstCidade);
            Cidade.CidadeAdper cidadeAux = firtCidade;


            if (cidades.Count == 1)
            {
                return cidadesDijkstra;
            }

            // organiza a ordem das cidades
            for (int i = 0; i < cidadesList.Count; i++)
            {

                Cidade.CidadeAdper proximaCidade = null;


                List<Cidade.CidadeAdper> distCidades = new List<Cidade.CidadeAdper>(cidadeAux.DistanciaCidades);

                if (distCidades.Count > 0)
                {
                    proximaCidade = distCidades[0];
                }

                bool flag = false;

                foreach (Cidade.CidadeAdper cidadeDistAux in distCidades)
                {

                    if (flag)
                    {
                        proximaCidade = cidadeDistAux;
                    }

                    flag = false;

                    foreach (Cidade cidadeDijkstra in cidadesDijkstra)
                    {
                        if (cidadeDistAux.Equals(cidadeDijkstra))
                        {
                            flag = true;
                            break;
                        }

                    }

                    if (flag == false && cidadeDistAux.Distance < proximaCidade.Distance)
                    {
                        proximaCidade = cidadeDistAux;
                    }

                    if (proximaCidade != cidadeDistAux)
                    {
                        flag = false;
                    }

                }

                cidadesDijkstra.Add(proximaCidade);

                // procura a proxima cidade na lista de cidades
                foreach (Cidade.CidadeAdper cidade in cidadesList)
                {
                    if (cidade.Equals(proximaCidade))
                    {
                        cidadeAux = cidade;
                        proximaCidade.Locais = OrdenaLocais(cidade.Locais);
                        break;
                    }
                }

            }



            return cidadesDijkstra;

        }


        private static HashSet<Local> OrdenaLocais(HashSet<Local> locais)
        {

            HashSet<Local> locaisDijkstra = new HashSet<Local>();
            List<Local> locaisList = new List<Local>(locais);

            if (locais.Count <= 0)
            {
                return locaisDijkstra;
            }

            Local.LocalAdpter firtLocal = (Local.LocalAdpter)locaisList[0];

            foreach (Local.LocalAdpter local in locaisList)
            {
                if (local.DistanciaReferencia < firtLocal.DistanciaReferencia)
                {
                    firtLocal = local;
                }
            }


            locaisDijkstra.Add(new Local(firtLocal.Nome, firtLocal.Lat, firtLocal.Long));
            Local.LocalAdpter localAux = firtLocal;

            if (locais.Count == 1)
            {
                return locaisDijkstra;
            }

            for (int i = 0; i < locaisList.Count; i++)
            {

                Local.LocalAdpter proximoLocal = null;


                List<Local.LocalAdpter> distLocais = new List<Local.LocalAdpter>(localAux.DistanciaLocais);

                if (distLocais.Count > 0)
                {
                    proximoLocal = distLocais[0];
                }

                bool flag = false;

                foreach (Local.LocalAdpter localDistAux in distLocais)
                {

                    if (flag)
                    {
                        proximoLocal = localDistAux;
                    }

                    flag = false;

                    foreach (Local localDijkstra in locaisDijkstra)
                    {
                        if (localDistAux.Equals(localDijkstra))
                        {
                            flag = true;
                            break;
                        }


                    }

                    if (flag == false && localDistAux.DistanciaReferencia < proximoLocal.DistanciaReferencia)
                    {
                        proximoLocal = localDistAux;
                    }

                    if (proximoLocal != localDistAux)
                    {
                        flag = false;
                    }

                }

                locaisDijkstra.Add(proximoLocal);

                // procura a proxima cidade na lista de cidades
                foreach (Local.LocalAdpter local in locaisList)
                {
                    if (local.Equals(proximoLocal))
                    {
                        localAux = local;
                        break;
                    }
                }

            }

            return locaisDijkstra;
        }


    }
}
