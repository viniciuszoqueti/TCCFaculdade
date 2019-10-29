namespace InterfaceTCC
{
    partial class MainForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCidade = new System.Windows.Forms.Button();
            this.nomeCidade = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listaCidades = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listaReferencias = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nomeReferencia = new System.Windows.Forms.TextBox();
            this.latitudeReferencia = new System.Windows.Forms.TextBox();
            this.longitudeReferencia = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddReferencia = new System.Windows.Forms.Button();
            this.treeViewDijkstra = new System.Windows.Forms.TreeView();
            this.aplicarDijikstra = new System.Windows.Forms.Button();
            this.cidadeSelecionada = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.carregarBasesDeDados = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCidade
            // 
            this.btnCidade.Location = new System.Drawing.Point(12, 76);
            this.btnCidade.Name = "btnCidade";
            this.btnCidade.Size = new System.Drawing.Size(91, 38);
            this.btnCidade.TabIndex = 0;
            this.btnCidade.Text = "Adicionar Cidade";
            this.btnCidade.UseVisualStyleBackColor = true;
            this.btnCidade.Click += new System.EventHandler(this.BtnCidade_Click);
            // 
            // nomeCidade
            // 
            this.nomeCidade.Location = new System.Drawing.Point(12, 40);
            this.nomeCidade.Name = "nomeCidade";
            this.nomeCidade.Size = new System.Drawing.Size(195, 20);
            this.nomeCidade.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cidade";
            // 
            // listaCidades
            // 
            this.listaCidades.FormattingEnabled = true;
            this.listaCidades.Location = new System.Drawing.Point(226, 40);
            this.listaCidades.Name = "listaCidades";
            this.listaCidades.Size = new System.Drawing.Size(263, 82);
            this.listaCidades.TabIndex = 3;
            this.listaCidades.SelectedIndexChanged += new System.EventHandler(this.ListaCidades_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Clique na cidade desejada para obter suas referencias ";
            // 
            // listaReferencias
            // 
            this.listaReferencias.FormattingEnabled = true;
            this.listaReferencias.Location = new System.Drawing.Point(226, 219);
            this.listaReferencias.Name = "listaReferencias";
            this.listaReferencias.Size = new System.Drawing.Size(263, 108);
            this.listaReferencias.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nome Referencia";
            // 
            // nomeReferencia
            // 
            this.nomeReferencia.Location = new System.Drawing.Point(15, 219);
            this.nomeReferencia.Name = "nomeReferencia";
            this.nomeReferencia.Size = new System.Drawing.Size(192, 20);
            this.nomeReferencia.TabIndex = 7;
            // 
            // latitudeReferencia
            // 
            this.latitudeReferencia.Location = new System.Drawing.Point(15, 265);
            this.latitudeReferencia.Name = "latitudeReferencia";
            this.latitudeReferencia.Size = new System.Drawing.Size(88, 20);
            this.latitudeReferencia.TabIndex = 8;
            // 
            // longitudeReferencia
            // 
            this.longitudeReferencia.Location = new System.Drawing.Point(119, 265);
            this.longitudeReferencia.Name = "longitudeReferencia";
            this.longitudeReferencia.Size = new System.Drawing.Size(88, 20);
            this.longitudeReferencia.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Latitude";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Longitude";
            // 
            // btnAddReferencia
            // 
            this.btnAddReferencia.Enabled = false;
            this.btnAddReferencia.Location = new System.Drawing.Point(15, 291);
            this.btnAddReferencia.Name = "btnAddReferencia";
            this.btnAddReferencia.Size = new System.Drawing.Size(90, 38);
            this.btnAddReferencia.TabIndex = 12;
            this.btnAddReferencia.Text = "Adicionar Referencia";
            this.btnAddReferencia.UseVisualStyleBackColor = true;
            this.btnAddReferencia.Click += new System.EventHandler(this.BtnAddReferencia_Click);
            // 
            // treeViewDijkstra
            // 
            this.treeViewDijkstra.Location = new System.Drawing.Point(575, 40);
            this.treeViewDijkstra.Name = "treeViewDijkstra";
            this.treeViewDijkstra.Size = new System.Drawing.Size(305, 238);
            this.treeViewDijkstra.TabIndex = 13;
            // 
            // aplicarDijikstra
            // 
            this.aplicarDijikstra.Enabled = false;
            this.aplicarDijikstra.Location = new System.Drawing.Point(781, 291);
            this.aplicarDijikstra.Name = "aplicarDijikstra";
            this.aplicarDijikstra.Size = new System.Drawing.Size(99, 52);
            this.aplicarDijikstra.TabIndex = 14;
            this.aplicarDijikstra.Text = "Aplicar Dijkstra";
            this.aplicarDijikstra.UseVisualStyleBackColor = true;
            this.aplicarDijikstra.Click += new System.EventHandler(this.AplicarDijikstra_Click);
            // 
            // cidadeSelecionada
            // 
            this.cidadeSelecionada.AutoSize = true;
            this.cidadeSelecionada.Location = new System.Drawing.Point(226, 200);
            this.cidadeSelecionada.Name = "cidadeSelecionada";
            this.cidadeSelecionada.Size = new System.Drawing.Size(105, 13);
            this.cidadeSelecionada.TabIndex = 15;
            this.cidadeSelecionada.Text = "Cidade Selecionada:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Enabled = false;
            this.btnSalvar.Location = new System.Drawing.Point(10, 373);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(93, 52);
            this.btnSalvar.TabIndex = 16;
            this.btnSalvar.Text = "Calcular e Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(122, 373);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 52);
            this.button2.TabIndex = 17;
            this.button2.Text = "Carregar JSON";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // carregarBasesDeDados
            // 
            this.carregarBasesDeDados.Location = new System.Drawing.Point(575, 291);
            this.carregarBasesDeDados.Name = "carregarBasesDeDados";
            this.carregarBasesDeDados.Size = new System.Drawing.Size(99, 52);
            this.carregarBasesDeDados.TabIndex = 18;
            this.carregarBasesDeDados.Text = "Carregar Bases de Dados";
            this.carregarBasesDeDados.UseVisualStyleBackColor = true;
            this.carregarBasesDeDados.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 437);
            this.Controls.Add(this.carregarBasesDeDados);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.cidadeSelecionada);
            this.Controls.Add(this.aplicarDijikstra);
            this.Controls.Add(this.treeViewDijkstra);
            this.Controls.Add(this.btnAddReferencia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.longitudeReferencia);
            this.Controls.Add(this.latitudeReferencia);
            this.Controls.Add(this.nomeReferencia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listaReferencias);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listaCidades);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nomeCidade);
            this.Controls.Add(this.btnCidade);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Calculador de Rota";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCidade;
        private System.Windows.Forms.TextBox nomeCidade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listaCidades;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listaReferencias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nomeReferencia;
        private System.Windows.Forms.TextBox latitudeReferencia;
        private System.Windows.Forms.TextBox longitudeReferencia;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddReferencia;
        private System.Windows.Forms.TreeView treeViewDijkstra;
        private System.Windows.Forms.Button aplicarDijikstra;
        private System.Windows.Forms.Label cidadeSelecionada;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button carregarBasesDeDados;
    }
}

