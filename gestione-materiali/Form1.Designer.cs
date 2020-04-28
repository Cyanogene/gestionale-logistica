namespace gestione_materiali
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Periodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodo_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodo_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodo_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lbl_ComponenteCaricato = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.produzioneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caricaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distintaBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caricaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView_DistintaBase = new System.Windows.Forms.TreeView();
            this.Lbl_Tree = new System.Windows.Forms.Label();
            this.Btn_ProgrammazioneProduzione = new System.Windows.Forms.Button();
            this.pulisciTabellaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Periodo,
            this.periodo_0,
            this.periodo_1,
            this.periodo_2,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(660, 120);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // Periodo
            // 
            this.Periodo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Periodo.HeaderText = "Periodo";
            this.Periodo.MinimumWidth = 100;
            this.Periodo.Name = "Periodo";
            this.Periodo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Periodo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // periodo_0
            // 
            this.periodo_0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.periodo_0.HeaderText = "0";
            this.periodo_0.MinimumWidth = 50;
            this.periodo_0.Name = "periodo_0";
            this.periodo_0.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.periodo_0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.periodo_0.Width = 50;
            // 
            // periodo_1
            // 
            this.periodo_1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.periodo_1.HeaderText = "1";
            this.periodo_1.MinimumWidth = 50;
            this.periodo_1.Name = "periodo_1";
            this.periodo_1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.periodo_1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.periodo_1.Width = 50;
            // 
            // periodo_2
            // 
            this.periodo_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.periodo_2.HeaderText = "2";
            this.periodo_2.MinimumWidth = 50;
            this.periodo_2.Name = "periodo_2";
            this.periodo_2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.periodo_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.periodo_2.Width = 50;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "3";
            this.Column1.MinimumWidth = 50;
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "4";
            this.Column2.MinimumWidth = 50;
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "5";
            this.Column3.MinimumWidth = 50;
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.HeaderText = "6";
            this.Column4.MinimumWidth = 50;
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 50;
            // 
            // Lbl_ComponenteCaricato
            // 
            this.Lbl_ComponenteCaricato.AutoSize = true;
            this.Lbl_ComponenteCaricato.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ComponenteCaricato.Location = new System.Drawing.Point(8, 159);
            this.Lbl_ComponenteCaricato.Name = "Lbl_ComponenteCaricato";
            this.Lbl_ComponenteCaricato.Size = new System.Drawing.Size(183, 20);
            this.Lbl_ComponenteCaricato.TabIndex = 4;
            this.Lbl_ComponenteCaricato.Text = "Carica una distinta base.";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.produzioneToolStripMenuItem,
            this.distintaBaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(773, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // produzioneToolStripMenuItem
            // 
            this.produzioneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salvaToolStripMenuItem,
            this.caricaToolStripMenuItem,
            this.pulisciTabellaToolStripMenuItem});
            this.produzioneToolStripMenuItem.Name = "produzioneToolStripMenuItem";
            this.produzioneToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.produzioneToolStripMenuItem.Text = "Produzione";
            // 
            // salvaToolStripMenuItem
            // 
            this.salvaToolStripMenuItem.Name = "salvaToolStripMenuItem";
            this.salvaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.salvaToolStripMenuItem.Text = "Salva";
            this.salvaToolStripMenuItem.Click += new System.EventHandler(this.salvaToolStripMenuItem_Click);
            // 
            // caricaToolStripMenuItem
            // 
            this.caricaToolStripMenuItem.Name = "caricaToolStripMenuItem";
            this.caricaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.caricaToolStripMenuItem.Text = "Carica";
            this.caricaToolStripMenuItem.Click += new System.EventHandler(this.caricaToolStripMenuItem_Click);
            // 
            // distintaBaseToolStripMenuItem
            // 
            this.distintaBaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.caricaToolStripMenuItem1});
            this.distintaBaseToolStripMenuItem.Name = "distintaBaseToolStripMenuItem";
            this.distintaBaseToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.distintaBaseToolStripMenuItem.Text = "Distinta base";
            // 
            // caricaToolStripMenuItem1
            // 
            this.caricaToolStripMenuItem1.Name = "caricaToolStripMenuItem1";
            this.caricaToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.caricaToolStripMenuItem1.Text = "Carica";
            this.caricaToolStripMenuItem1.Click += new System.EventHandler(this.caricaToolStripMenuItem1_Click);
            // 
            // treeView_DistintaBase
            // 
            this.treeView_DistintaBase.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView_DistintaBase.Location = new System.Drawing.Point(313, 192);
            this.treeView_DistintaBase.Name = "treeView_DistintaBase";
            this.treeView_DistintaBase.Size = new System.Drawing.Size(359, 212);
            this.treeView_DistintaBase.TabIndex = 6;
            this.treeView_DistintaBase.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_DistintaBase_NodeMouseDoubleClick);
            // 
            // Lbl_Tree
            // 
            this.Lbl_Tree.AutoSize = true;
            this.Lbl_Tree.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Tree.Location = new System.Drawing.Point(309, 436);
            this.Lbl_Tree.Name = "Lbl_Tree";
            this.Lbl_Tree.Size = new System.Drawing.Size(298, 20);
            this.Lbl_Tree.TabIndex = 7;
            this.Lbl_Tree.Text = "Nessun componente è stato selezionato.";
            // 
            // Btn_ProgrammazioneProduzione
            // 
            this.Btn_ProgrammazioneProduzione.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ProgrammazioneProduzione.Image = global::gestione_materiali.Properties.Resources.Gears;
            this.Btn_ProgrammazioneProduzione.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_ProgrammazioneProduzione.Location = new System.Drawing.Point(12, 192);
            this.Btn_ProgrammazioneProduzione.Name = "Btn_ProgrammazioneProduzione";
            this.Btn_ProgrammazioneProduzione.Size = new System.Drawing.Size(167, 81);
            this.Btn_ProgrammazioneProduzione.TabIndex = 3;
            this.Btn_ProgrammazioneProduzione.Text = "Programma\r\nproduzione";
            this.Btn_ProgrammazioneProduzione.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn_ProgrammazioneProduzione.UseVisualStyleBackColor = true;
            this.Btn_ProgrammazioneProduzione.Click += new System.EventHandler(this.Btn_ProgrammazioneProduzione_Click);
            // 
            // pulisciTabellaToolStripMenuItem
            // 
            this.pulisciTabellaToolStripMenuItem.Name = "pulisciTabellaToolStripMenuItem";
            this.pulisciTabellaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pulisciTabellaToolStripMenuItem.Text = "Pulisci tabella";
            this.pulisciTabellaToolStripMenuItem.Click += new System.EventHandler(this.pulisciTabellaToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 544);
            this.Controls.Add(this.Lbl_Tree);
            this.Controls.Add(this.treeView_DistintaBase);
            this.Controls.Add(this.Lbl_ComponenteCaricato);
            this.Controls.Add(this.Btn_ProgrammazioneProduzione);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Gestione materiali";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Periodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodo_0;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodo_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn periodo_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button Btn_ProgrammazioneProduzione;
        private System.Windows.Forms.Label Lbl_ComponenteCaricato;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem produzioneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caricaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem distintaBaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caricaToolStripMenuItem1;
        private System.Windows.Forms.TreeView treeView_DistintaBase;
        private System.Windows.Forms.Label Lbl_Tree;
        private System.Windows.Forms.ToolStripMenuItem pulisciTabellaToolStripMenuItem;
    }
}

