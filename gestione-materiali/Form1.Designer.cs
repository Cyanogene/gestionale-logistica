﻿namespace gestione_materiali
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.DataGridView_Produzione = new System.Windows.Forms.DataGridView();
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
            this.caricaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.esportaExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.distintaBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caricaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pulisciTabellaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeView_DistintaBase = new System.Windows.Forms.TreeView();
            this.NumericUpAndDown_Periodi = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_ProgrammazioneProduzione = new System.Windows.Forms.Button();
            this.cms_DistintaBase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visualizzaProduzioneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informazioniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Cms_distintaBaseGiacenza = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.impostaGiacenzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informazioniToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Produzione)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpAndDown_Periodi)).BeginInit();
            this.cms_DistintaBase.SuspendLayout();
            this.Cms_distintaBaseGiacenza.SuspendLayout();
            this.SuspendLayout();
            // 
            // DataGridView_Produzione
            // 
            this.DataGridView_Produzione.AllowUserToAddRows = false;
            this.DataGridView_Produzione.AllowUserToDeleteRows = false;
            this.DataGridView_Produzione.AllowUserToResizeColumns = false;
            this.DataGridView_Produzione.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(239)))), ((int)(((byte)(249)))));
            this.DataGridView_Produzione.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView_Produzione.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGridView_Produzione.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridView_Produzione.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Periodo,
            this.periodo_0,
            this.periodo_1,
            this.periodo_2,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.DataGridView_Produzione.Location = new System.Drawing.Point(16, 126);
            this.DataGridView_Produzione.MaximumSize = new System.Drawing.Size(1100, 140);
            this.DataGridView_Produzione.Name = "DataGridView_Produzione";
            this.DataGridView_Produzione.RowHeadersVisible = false;
            this.DataGridView_Produzione.Size = new System.Drawing.Size(1100, 140);
            this.DataGridView_Produzione.TabIndex = 2;
            this.DataGridView_Produzione.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.DataGridView_Produzione.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
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
            this.Lbl_ComponenteCaricato.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ComponenteCaricato.Location = new System.Drawing.Point(327, 307);
            this.Lbl_ComponenteCaricato.Name = "Lbl_ComponenteCaricato";
            this.Lbl_ComponenteCaricato.Size = new System.Drawing.Size(0, 21);
            this.Lbl_ComponenteCaricato.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.produzioneToolStripMenuItem,
            this.distintaBaseToolStripMenuItem,
            this.pulisciTabellaToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // produzioneToolStripMenuItem
            // 
            this.produzioneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.caricaToolStripMenuItem,
            this.salvaToolStripMenuItem,
            this.esportaExcelToolStripMenuItem});
            this.produzioneToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.produzioneToolStripMenuItem.Name = "produzioneToolStripMenuItem";
            this.produzioneToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.produzioneToolStripMenuItem.Text = "Produzione";
            // 
            // caricaToolStripMenuItem
            // 
            this.caricaToolStripMenuItem.Name = "caricaToolStripMenuItem";
            this.caricaToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.caricaToolStripMenuItem.Text = "Carica";
            this.caricaToolStripMenuItem.Click += new System.EventHandler(this.caricaToolStripMenuItem_Click);
            // 
            // salvaToolStripMenuItem
            // 
            this.salvaToolStripMenuItem.Name = "salvaToolStripMenuItem";
            this.salvaToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.salvaToolStripMenuItem.Text = "Salva";
            this.salvaToolStripMenuItem.Click += new System.EventHandler(this.salvaToolStripMenuItem_Click);
            // 
            // esportaExcelToolStripMenuItem
            // 
            this.esportaExcelToolStripMenuItem.Name = "esportaExcelToolStripMenuItem";
            this.esportaExcelToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.esportaExcelToolStripMenuItem.Text = "Esporta Excel";
            this.esportaExcelToolStripMenuItem.Click += new System.EventHandler(this.esportaExcelToolStripMenuItem_Click);
            // 
            // distintaBaseToolStripMenuItem
            // 
            this.distintaBaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.caricaToolStripMenuItem1});
            this.distintaBaseToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.distintaBaseToolStripMenuItem.Name = "distintaBaseToolStripMenuItem";
            this.distintaBaseToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.distintaBaseToolStripMenuItem.Text = "Distinta base";
            // 
            // caricaToolStripMenuItem1
            // 
            this.caricaToolStripMenuItem1.Name = "caricaToolStripMenuItem1";
            this.caricaToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.caricaToolStripMenuItem1.Text = "Carica";
            this.caricaToolStripMenuItem1.Click += new System.EventHandler(this.caricaToolStripMenuItem1_Click);
            // 
            // pulisciTabellaToolStripMenuItem
            // 
            this.pulisciTabellaToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.pulisciTabellaToolStripMenuItem.Name = "pulisciTabellaToolStripMenuItem";
            this.pulisciTabellaToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.pulisciTabellaToolStripMenuItem.Text = "Pulisci tabella";
            this.pulisciTabellaToolStripMenuItem.Click += new System.EventHandler(this.pulisciTabellaToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.infoToolStripMenuItem.Text = "Manuale";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // TreeView_DistintaBase
            // 
            this.TreeView_DistintaBase.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView_DistintaBase.Location = new System.Drawing.Point(331, 330);
            this.TreeView_DistintaBase.Name = "TreeView_DistintaBase";
            this.TreeView_DistintaBase.Size = new System.Drawing.Size(561, 303);
            this.TreeView_DistintaBase.TabIndex = 6;
            this.TreeView_DistintaBase.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.treeView_DistintaBase_NodeMouseHover_1);
            this.TreeView_DistintaBase.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_DistintaBase_NodeMouseClick);
            // 
            // NumericUpAndDown_Periodi
            // 
            this.NumericUpAndDown_Periodi.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumericUpAndDown_Periodi.Location = new System.Drawing.Point(16, 74);
            this.NumericUpAndDown_Periodi.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpAndDown_Periodi.MinimumSize = new System.Drawing.Size(123, 0);
            this.NumericUpAndDown_Periodi.Name = "NumericUpAndDown_Periodi";
            this.NumericUpAndDown_Periodi.Size = new System.Drawing.Size(126, 24);
            this.NumericUpAndDown_Periodi.TabIndex = 8;
            this.NumericUpAndDown_Periodi.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NumericUpAndDown_Periodi.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 21);
            this.label1.TabIndex = 9;
            this.label1.Text = "Seleziona il numero di periodi";
            // 
            // Btn_ProgrammazioneProduzione
            // 
            this.Btn_ProgrammazioneProduzione.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_ProgrammazioneProduzione.Image = global::gestione_materiali.Properties.Resources.Gears;
            this.Btn_ProgrammazioneProduzione.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_ProgrammazioneProduzione.Location = new System.Drawing.Point(16, 287);
            this.Btn_ProgrammazioneProduzione.MinimumSize = new System.Drawing.Size(167, 81);
            this.Btn_ProgrammazioneProduzione.Name = "Btn_ProgrammazioneProduzione";
            this.Btn_ProgrammazioneProduzione.Size = new System.Drawing.Size(167, 81);
            this.Btn_ProgrammazioneProduzione.TabIndex = 3;
            this.Btn_ProgrammazioneProduzione.Text = "Programma\r\nproduzione";
            this.Btn_ProgrammazioneProduzione.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn_ProgrammazioneProduzione.UseVisualStyleBackColor = true;
            this.Btn_ProgrammazioneProduzione.Click += new System.EventHandler(this.Btn_ProgrammazioneProduzione_Click);
            // 
            // cms_DistintaBase
            // 
            this.cms_DistintaBase.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.cms_DistintaBase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visualizzaProduzioneToolStripMenuItem,
            this.informazioniToolStripMenuItem});
            this.cms_DistintaBase.Name = "cms_DistintaBase";
            this.cms_DistintaBase.Size = new System.Drawing.Size(204, 48);
            // 
            // visualizzaProduzioneToolStripMenuItem
            // 
            this.visualizzaProduzioneToolStripMenuItem.Name = "visualizzaProduzioneToolStripMenuItem";
            this.visualizzaProduzioneToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.visualizzaProduzioneToolStripMenuItem.Text = "Visualizza produzione";
            this.visualizzaProduzioneToolStripMenuItem.Click += new System.EventHandler(this.visualizzaProduzioneToolStripMenuItem_Click);
            // 
            // informazioniToolStripMenuItem
            // 
            this.informazioniToolStripMenuItem.Name = "informazioniToolStripMenuItem";
            this.informazioniToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.informazioniToolStripMenuItem.Text = "Informazioni";
            this.informazioniToolStripMenuItem.Click += new System.EventHandler(this.informazioniToolStripMenuItem_Click);
            // 
            // Cms_distintaBaseGiacenza
            // 
            this.Cms_distintaBaseGiacenza.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.impostaGiacenzaToolStripMenuItem,
            this.informazioniToolStripMenuItem1});
            this.Cms_distintaBaseGiacenza.Name = "Cms_distintaBaseGiacenza";
            this.Cms_distintaBaseGiacenza.Size = new System.Drawing.Size(181, 70);
            // 
            // impostaGiacenzaToolStripMenuItem
            // 
            this.impostaGiacenzaToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.impostaGiacenzaToolStripMenuItem.Name = "impostaGiacenzaToolStripMenuItem";
            this.impostaGiacenzaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.impostaGiacenzaToolStripMenuItem.Text = "Imposta giacenza";
            this.impostaGiacenzaToolStripMenuItem.Click += new System.EventHandler(this.impostaGiacenzaToolStripMenuItem_Click);
            // 
            // informazioniToolStripMenuItem1
            // 
            this.informazioniToolStripMenuItem1.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F);
            this.informazioniToolStripMenuItem1.Name = "informazioniToolStripMenuItem1";
            this.informazioniToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.informazioniToolStripMenuItem1.Text = "Informazioni";
            this.informazioniToolStripMenuItem1.Click += new System.EventHandler(this.informazioniToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NumericUpAndDown_Periodi);
            this.Controls.Add(this.TreeView_DistintaBase);
            this.Controls.Add(this.Lbl_ComponenteCaricato);
            this.Controls.Add(this.Btn_ProgrammazioneProduzione);
            this.Controls.Add(this.DataGridView_Produzione);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(960, 540);
            this.Name = "Form1";
            this.Text = "Gestione materiali";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Produzione)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpAndDown_Periodi)).EndInit();
            this.cms_DistintaBase.ResumeLayout(false);
            this.Cms_distintaBaseGiacenza.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DataGridView_Produzione;
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
        private System.Windows.Forms.TreeView TreeView_DistintaBase;
        private System.Windows.Forms.NumericUpDown NumericUpAndDown_Periodi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip cms_DistintaBase;
        private System.Windows.Forms.ToolStripMenuItem informazioniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pulisciTabellaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem esportaExcelToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip Cms_distintaBaseGiacenza;
        private System.Windows.Forms.ToolStripMenuItem impostaGiacenzaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visualizzaProduzioneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informazioniToolStripMenuItem1;
    }
}

