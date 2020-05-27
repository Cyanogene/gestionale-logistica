namespace gestione_materiali
{
    partial class Form2_GiacenzaSottoComponenti
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2_GiacenzaSottoComponenti));
            this.lbl_titolo = new System.Windows.Forms.Label();
            this.Btn_aggiungi = new System.Windows.Forms.Button();
            this.NumUpD_quantità = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.NumUpD_quantità)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_titolo
            // 
            this.lbl_titolo.AutoSize = true;
            this.lbl_titolo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titolo.Location = new System.Drawing.Point(12, 9);
            this.lbl_titolo.Name = "lbl_titolo";
            this.lbl_titolo.Size = new System.Drawing.Size(237, 28);
            this.lbl_titolo.TabIndex = 1;
            this.lbl_titolo.Text = "Disponibilità a magazzino (giacenza)\r\ndel componente: ";
            // 
            // Btn_aggiungi
            // 
            this.Btn_aggiungi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_aggiungi.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F);
            this.Btn_aggiungi.Location = new System.Drawing.Point(414, 72);
            this.Btn_aggiungi.Name = "Btn_aggiungi";
            this.Btn_aggiungi.Size = new System.Drawing.Size(100, 32);
            this.Btn_aggiungi.TabIndex = 2;
            this.Btn_aggiungi.Text = "AGGIUNGI";
            this.Btn_aggiungi.UseVisualStyleBackColor = true;
            this.Btn_aggiungi.Click += new System.EventHandler(this.Btn_aggiungi_Click);
            // 
            // NumUpD_quantità
            // 
            this.NumUpD_quantità.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumUpD_quantità.Location = new System.Drawing.Point(249, 72);
            this.NumUpD_quantità.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.NumUpD_quantità.Name = "NumUpD_quantità";
            this.NumUpD_quantità.Size = new System.Drawing.Size(78, 22);
            this.NumUpD_quantità.TabIndex = 3;
            this.NumUpD_quantità.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form2_GiacenzaSottoComponenti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 116);
            this.Controls.Add(this.NumUpD_quantità);
            this.Controls.Add(this.Btn_aggiungi);
            this.Controls.Add(this.lbl_titolo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2_GiacenzaSottoComponenti";
            this.Text = "Gestione materiali";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_GiacenzaSottoComponenti_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.NumUpD_quantità)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_titolo;
        private System.Windows.Forms.Button Btn_aggiungi;
        private System.Windows.Forms.NumericUpDown NumUpD_quantità;
    }
}