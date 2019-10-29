using BaseTCC.Models.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseTCC.Utils
{
    public class Genetic
    {
        private int NUMERO_CIDADES = 0;
        private int NUMERO_POPULACAO = 5;
        private int nEvolucoes = 0;
        private bool notOrder = false;
        private List<Cidade> dijkstra;


        public Genetic(int nEvolucoes)
        {
            this.nEvolucoes = nEvolucoes;
        }

        public HashSet<Cidade> Calcular(HashSet<Cidade> dijkstra, HashSet<Cidade.CidadeAdper> hashSetcidadesLigacao)
        {

            List<Cidade.CidadeAdper> cidadesLigacao = new List<Cidade.CidadeAdper>(hashSetcidadesLigacao);
            this.dijkstra = new List<Cidade>(dijkstra);

            NUMERO_CIDADES = cidadesLigacao.Count;
            // NUMERO_POPULACAO = cidadesLigacao.Count;
            float[][] mapa = new float[NUMERO_CIDADES][];
            Cidade[] cidades = new Cidade[NUMERO_CIDADES];


            for (int i = 0; i < NUMERO_CIDADES; i++)
            {
                cidades[i] = cidadesLigacao[i];

                float[] distancias = new float[NUMERO_CIDADES];
                int auxJ = 0;

                for (int j = 0; j < NUMERO_CIDADES; j++)
                {

                    if (i == j)
                    {
                        distancias[j] = 0;
                    }
                    else
                    {
                        distancias[j] = new List<Cidade.CidadeAdper>(cidadesLigacao[i].DistanciaCidades)[auxJ].Distance;
                        auxJ++;
                    }
                }

                mapa[i] = distancias;

            }


            /*  float[][] mapa = {
                  new float[] { 0, 42, 61, 30, 17, 82, 31, 11 },
                  new float[] { 42, 0, 14, 87, 28, 70, 19, 33 },
                  new float[] { 61, 14, 0, 20, 81, 21, 8, 29  },
                  new float[] { 30, 87, 20, 0, 34, 33, 91, 10 },
                  new float[] { 17, 28, 81, 34, 0, 41, 34, 82 },
                  new float[] { 82, 70, 21, 33, 41, 0, 19, 32 },
                  new float[] { 31, 19, 8, 91, 34, 19, 0, 59  },
                  new float[] { 11, 33, 29, 10, 82, 32, 59, 0 }
              };
              */

            /*   String[] cidades = { "A", "B", "C", "D", "E", "F", "G", "H" };*/

            float[] resultados = new float[NUMERO_POPULACAO];
            int[][] cromossomos = new int[NUMERO_POPULACAO][];


            // inclui o dijkstra como primeiro cromossomo






            IniciarGenetico(resultados, cromossomos, mapa, cidades, nEvolucoes);

            return Resultado(cromossomos, resultados, cidades);
        }


        private void IniciarGenetico(float[] resultados, int[][] cromossomos, float[][] mapa, Cidade[] cidades, int numeroEvolucoes)
        {
            // definicoes iniciais
            bool mostrarEvolucao = true;
            float taxaMortalidade = (float)0.5;


            for (int k = 0; k < NUMERO_POPULACAO; k++)
            {
                int[] inteiros = new int[NUMERO_CIDADES];
                for (int j = 0; j < NUMERO_CIDADES; j++)
                {
                    inteiros[j] = 0;
                }

                cromossomos[k] = inteiros;
            }

            int[] valueDijkstra = new int[NUMERO_CIDADES];

            for (int k = 0; k < cidades.Length; k++)
            {
                for (int c = 0; c < dijkstra.Count; c++)
                {


                    if (dijkstra[c].Equals(cidades[k]))
                    {
                        valueDijkstra[c] = k;
                        break;
                    }
                }

            }

            cromossomos[0] = valueDijkstra;

            GerarCromossomosAleatoriamente(cromossomos);
            CalcularResultado(cromossomos, resultados, mapa);
            Ordenar(cromossomos, resultados);
            if (mostrarEvolucao)
                Imprimir(cromossomos, resultados, cidades);

            int i;
            for (i = 0; i < numeroEvolucoes; i++)
            {
                RenovarCromossomos(cromossomos, resultados, taxaMortalidade);
                CalcularResultado(cromossomos, resultados, mapa);
                Ordenar(cromossomos, resultados);
                if (mostrarEvolucao)
                {
                    Console.Write("Geracao: " + (i + 1) + "\n");
                    Imprimir(cromossomos, resultados, cidades);
                }
            }


        }

        private HashSet<Cidade> Resultado(int[][] cromossomos, float[] resultados, Cidade[] cidades)
        {
            int i, i2;
            i = 0;
            Console.Write("\n Resultado Final \n\n");
            for (i2 = 0; i2 < NUMERO_CIDADES; i2++)
            {
                Console.Write(cidades[cromossomos[i][i2]] + " => ");
            }
            Console.Write(cidades[cromossomos[i][0]] + " ");
            Console.Write(" Resultado: " + resultados[i]);
            Console.Write("\n\n");
            return new HashSet<Cidade>(dijkstra);
        }

        private void RenovarCromossomos(int[][] cromossomos, float[] resultados, float taxaMortalidade)
        {

            int inicioExcluidos = (int)(taxaMortalidade * NUMERO_POPULACAO);

            int i, i2 = 0;

            for (i = inicioExcluidos; i < NUMERO_POPULACAO; i++)
            {

                bool valido = false;

                while (!valido)
                {

                    int[] c_tmp = resetaCromossomo();
                    c_tmp[0] = 0;

                    // pegando 2 pais aleatoriamente
                    int pai1, pai2;

                    pai1 = new Random().Next(inicioExcluidos);
                    do
                    {
                        pai2 = new Random().Next(inicioExcluidos);
                    } while ((pai1 == pai2)
                            && (resultados[pai1] != resultados[pai2]));

                    // pegando 3 caracteristicas do pai 1 aleatoriamente
                    for (i2 = 0; i2 < (int)NUMERO_CIDADES / 4; i2++)
                    {

                        int pos;
                        pos = new Random().Next(1, NUMERO_CIDADES);
                        c_tmp[pos] = cromossomos[pai1][pos];
                    }
                    // pegando restante do pai 2
                    for (i2 = 0; i2 < (int)NUMERO_CIDADES / 4; i2++)
                    {
                        int pos = new Random().Next(1, NUMERO_CIDADES);
                        if (c_tmp[pos] == -1)
                        {
                            if (ValorValidoNoCromossomo(cromossomos[pai2][pos],
                                    c_tmp))
                            {
                                c_tmp[pos] = cromossomos[pai2][pos];
                            }
                        }
                    }

                    // preenchendo o restante com aleatorios
                    for (i2 = 1; i2 < NUMERO_CIDADES; i2++)
                    {
                        if (c_tmp[i2] == -1)
                        {
                            int crom_temp = ValorValidoNoCromossomo(c_tmp);
                            c_tmp[i2] = crom_temp;
                        }
                    }

                    // verificando se é valido
                    valido = CromossomoValido(c_tmp, cromossomos);
                    if (valido)
                    {
                        cromossomos[i] = c_tmp;
                    }
                }
            }

        }

        private int[][] GerarCromossomosAleatoriamente(int[][] cromossomos)
        {
            notOrder = true;
            // inicializando cromossomos aleatoriamente
            int[] c_tmp = new int[NUMERO_CIDADES];


            int i, i2;
            for (i = 1; i < NUMERO_POPULACAO; i++)
            {
                bool crom_valido = false;
                while (!crom_valido)
                {
                    crom_valido = true;
                    c_tmp = resetaCromossomo();
                    c_tmp[0] = 0;

                    // gerando cromossomo - ok
                    for (i2 = 1; i2 < NUMERO_CIDADES; i2++)
                    {
                        c_tmp[i2] = ValorValidoNoCromossomo(c_tmp);
                    }
                    crom_valido = CromossomoValido(c_tmp, cromossomos);
                }
                cromossomos[i] = c_tmp;
            }

            return cromossomos;
        }

        private int[] resetaCromossomo()
        {
            int[] c = new int[NUMERO_CIDADES];
            int i;
            for (i = 0; i < NUMERO_CIDADES; i++)
            {
                c[i] = -1;
            }
            return c;
        }

        private int ValorValidoNoCromossomo(int[] c_tmp)
        {
            int crom_temp;
            bool valido;
            do
            {
                crom_temp = new Random().Next(NUMERO_CIDADES);
                valido = true;
                for (int ii = 0; ii < NUMERO_CIDADES; ii++)
                {
                    if (c_tmp[ii] == crom_temp)
                        valido = false;
                }
            } while (!valido);
            return crom_temp;
        }

        private bool ValorValidoNoCromossomo(int valor, int[] c_tmp)
        {
            int crom_temp = valor;
            bool valido;

            valido = true;
            for (int ii = 0; ii < NUMERO_CIDADES; ii++)
            {
                if (c_tmp[ii] == crom_temp)
                    valido = false;
            }

            return valido;
        }

        private bool CromossomoValido(int[] c_tmp, int[][] cromossomos)
        {
            int j, j2;
            bool crom_valido = true;

            for (j = 0; j < NUMERO_POPULACAO; j++)
            {
                int n_iguais = 0;
                for (j2 = 0; j2 < NUMERO_CIDADES; j2++)
                {
                    if (c_tmp[j2] == cromossomos[j][j2])
                    {
                        n_iguais++;
                    }
                }

                if (n_iguais == NUMERO_CIDADES)
                    crom_valido = false;
            }
            return crom_valido;
        }

        private void Imprimir(int[][] cromossomos, float[] resultados, Cidade[] cidades)
        {
            int i, i2;
            for (i = 0; i < NUMERO_POPULACAO; i++)
            {
                for (i2 = 0; i2 < NUMERO_CIDADES; i2++)
                {
                    Console.Write(cidades[cromossomos[i][i2]] + " => ");
                }
                Console.Write(cidades[cromossomos[i][0]] + " ");
                Console.WriteLine(" Resultados: " + resultados[i]);
            }
        }

        private void CalcularResultado(int[][] cromossomos, float[] resultados, float[][] mapa)
        {
            int i, i2;
            // calculando o resultado
            for (i = 0; i < NUMERO_POPULACAO; i++)
            {
                float resTmp = 0;
                for (i2 = 0; i2 < NUMERO_CIDADES - 1; i2++)
                {
                    resTmp += mapa[cromossomos[i][i2]][cromossomos[i][i2 + 1]];
                }
           //     resTmp += mapa[cromossomos[i][0]][cromossomos[i][i2]];
                resultados[i] = resTmp;
            }

        }

        private void Ordenar(int[][] cromossomos, float[] resultados)
        {
            // ordenando       

            int i, i2;


            for (i = 0; i < NUMERO_POPULACAO; i++)
            {
                if (!notOrder)
                {

                    for (i2 = i; i2 < NUMERO_POPULACAO; i2++)
                    {
                        if (resultados[i] > resultados[i2])
                        {
                            float vTmp;
                            int[] vvTmp = new int[NUMERO_POPULACAO];
                            vTmp = resultados[i];
                            resultados[i] = resultados[i2];
                            resultados[i2] = vTmp;

                            vvTmp = cromossomos[i];
                            cromossomos[i] = cromossomos[i2];
                            cromossomos[i2] = vvTmp;
                        }
                    }
                }
                notOrder = false;
            }
        }
    }
}
