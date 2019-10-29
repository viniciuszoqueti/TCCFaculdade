using BaseTCC.Models.App;
using BaseTCC.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfaceTCC
{
    public partial class MainForm : Form
    {
        private HashSet<Cidade> cidades;
        private HashSet<Cidade.CidadeAdper> cidadesRestore;
        HashSet<Cidade> cidadesAlgoritimos = new HashSet<Cidade>();

        private bool dijikstraAplicado { get; set; }


        public MainForm()
        {
            InitializeComponent();
            cidades = new HashSet<Cidade>();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void BtnCidade_Click(object sender, EventArgs e)
        {
            string cidadeNome = nomeCidade.Text;

            if (!string.IsNullOrEmpty(cidadeNome))
            {

                try
                {
                    Cidade cidade = Cidade.BuscaCidade(cidadeNome);

                    if (cidade != null)
                    {

                        if (cidades.Contains(cidade))
                        {
                            MessageBox.Show("Cidade já existente");
                            return;
                        }

                        cidades.Add(cidade);

                        listaCidades.Items.Clear();
                        listaCidades.Items.AddRange(cidades.ToArray());

                        MessageBox.Show("Cidade encontrada e adicionada");
                    }
                    else
                    {
                        MessageBox.Show("Cidade não encontrada pelo 'Here API' verifique se o nome está correto");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro Here API: " + ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("O Campo Nome Cidade não pode ser vazio");
                nomeCidade.Focus();
            }
        }

        private void ListaCidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cidade cidade = (Cidade)listaCidades.SelectedItem;
                if (cidade is null)
                {
                    return;
                }
                listaReferencias.Items.Clear();
                listaReferencias.Items.AddRange(cidade.Locais.ToArray());
                cidadeSelecionada.Text = "Cidade Selecionada: " + cidade.Nome;
                btnAddReferencia.Enabled = true;
                btnSalvar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void BtnAddReferencia_Click(object sender, EventArgs e)
        {
            try
            {
                Cidade cidade = (Cidade)listaCidades.SelectedItem;

                if (string.IsNullOrEmpty(latitudeReferencia.Text))
                {
                    MessageBox.Show("O Campo latitude não pode ser vazio");
                    latitudeReferencia.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(longitudeReferencia.Text))
                {
                    MessageBox.Show("O Campo longitude não pode ser vazio");
                    longitudeReferencia.Focus();
                    return;
                }

                string nomeRef = nomeReferencia.Text;
                double latitude = Convert.ToDouble(latitudeReferencia.Text.Replace(".", ",").Trim());
                double longitude = Convert.ToDouble(longitudeReferencia.Text.Replace(".", ",").Trim());

                if (string.IsNullOrEmpty(nomeRef))
                {
                    MessageBox.Show("O Campo Nome Referencia não pode ser vazio");
                    nomeReferencia.Focus();
                    return;
                }
                else if (!(longitude <= 180 && longitude >= -180))
                {
                    MessageBox.Show("A longitude está fora dos limites -180 ~ 180");
                    longitudeReferencia.Focus();
                    return;
                }
                else if (!(latitude <= 90 && latitude >= -90))
                {
                    MessageBox.Show("A latitude está fora dos limites -90 ~ 90");
                    latitudeReferencia.Focus();
                    return;
                }

                Local local = new Local(nomeRef, (float)latitude, (float)longitude);

                if (!cidade.Locais.Contains(local))
                {
                    cidade.Locais.Add(local);
                    listaReferencias.Items.Clear();
                    listaReferencias.Items.AddRange(cidade.Locais.ToArray());
                    MessageBox.Show("Referencia Adicionada em " + cidade.Nome);
                }
                else
                {
                    MessageBox.Show("Referencia já adicionada!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dijikstraAplicado = false;
            aplicarDijikstra.Text = "Aplicar Dijkstra";

            try
            {

                cidades = new HashSet<Cidade>(Cidade.CalcularDistancias(cidades));

                FileManipulator arquivo = new FileManipulator("Base_de_dados");
                string conteudoArquivo = JsonConvert.SerializeObject(cidades, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });

                if (arquivo.ExistsFiles())
                {
                    arquivo.DeleteFile(arquivo.GetNameFile(0));
                }

                arquivo.CreateFile(conteudoArquivo, "json");

                MessageBox.Show("Base criada com sucesso!");
                carregaDados();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dijikstraAplicado = false;
            aplicarDijikstra.Text = "Aplicar Dijkstra";
            carregaDados();
        }

        private void carregaDados()
        {
            try
            {
                FileManipulator arquivo = new FileManipulator("Base_de_dados");

                if (!arquivo.ExistsFiles())
                {
                    MessageBox.Show("Base de dados inexistente!");
                    return;
                }

                string conteudoArquivo = arquivo.GetTextFile(arquivo.GetNameFile(0));
                cidades = new HashSet<Cidade>();
                cidadesRestore = JsonConvert.DeserializeObject<HashSet<Cidade.CidadeAdper>>(conteudoArquivo, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                });

                cidades = new HashSet<Cidade>(cidadesRestore);

                listaCidades.Items.Clear();
                listaCidades.Items.AddRange(cidades.ToArray());

                MessageBox.Show("Dados carregados!");
                aplicarDijikstra.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AplicarDijikstra_Click(object sender, EventArgs e)
        {

            if (cidadesRestore.Count < 1)
            {
                MessageBox.Show("Os dados ainda não foram carregados!");
                return;
            }

            if (!dijikstraAplicado)
            {
                cidadesAlgoritimos = Dijkstra.Calcular(cidadesRestore);
            }
            else
            {
                Genetic genetic = new Genetic(300);
                cidadesAlgoritimos = genetic.Calcular(cidadesAlgoritimos, cidadesRestore);
            }

            treeViewDijkstra.Nodes.Clear();

            int i = 0;

            foreach (Cidade cidade in cidadesAlgoritimos)
            {
                treeViewDijkstra.Nodes.Add(cidade.ToString());

                foreach (Local local in cidade.Locais)
                {
                    treeViewDijkstra.Nodes[i].Nodes.Add(local.ToString());
                }

                i++;
            }

            if (treeViewDijkstra.Nodes.Count > 0 && !dijikstraAplicado)
            {
                dijikstraAplicado = true;
                aplicarDijikstra.Text = "Aplicar Genetico";
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dijikstraAplicado = false;
            aplicarDijikstra.Text = "Aplicar Dijkstra";

            if (cidadesRestore.Count < 1)
            {
                MessageBox.Show("Os dados ainda não foram carregados!");
                return;
            }

            cidadesRestore = BaseTCC.Utils.Infosiga.AplicarCaminhoMacro(cidadesRestore);

            MessageBox.Show("Dados do Infosiga aplicados!");

        }



    }
}
