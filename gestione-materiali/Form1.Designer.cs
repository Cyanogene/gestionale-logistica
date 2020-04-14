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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Periodo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodo_0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodo_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.periodo_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Btn_ProgrammazioneProduzione = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(673, 395);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Carica prodotto";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(673, 425);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Salva prodotto";
            this.button2.UseVisualStyleBackColor = true;
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(609, 152);
            this.dataGridView1.TabIndex = 2;
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
            // Btn_ProgrammazioneProduzione
            // 
            this.Btn_ProgrammazioneProduzione.Location = new System.Drawing.Point(326, 225);
            this.Btn_ProgrammazioneProduzione.Name = "Btn_ProgrammazioneProduzione";
            this.Btn_ProgrammazioneProduzione.Size = new System.Drawing.Size(117, 44);
            this.Btn_ProgrammazioneProduzione.TabIndex = 3;
            this.Btn_ProgrammazioneProduzione.Text = "Programma produzione";
            this.Btn_ProgrammazioneProduzione.UseVisualStyleBackColor = true;
            this.Btn_ProgrammazioneProduzione.Click += new System.EventHandler(this.Btn_ProgrammazioneProduzione_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Btn_ProgrammazioneProduzione);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
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
    }
}

