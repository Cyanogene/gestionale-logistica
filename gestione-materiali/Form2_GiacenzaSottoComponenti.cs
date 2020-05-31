using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestione_materiali
{
    public partial class Form2_GiacenzaSottoComponenti : Form
    {
        public int quantità;
        bool ok = false;
        public bool attendo = true;
        int quantitàPreEsistente;

        public Form2_GiacenzaSottoComponenti(Componente componente)
        {
            InitializeComponent();
            lbl_titolo.Text += componente.Nome;
            quantitàPreEsistente = componente.Produzione[0].Giacenza;
            NumUpD_quantità.Value = quantitàPreEsistente;
            CenterToParent();
        }

        private void Btn_aggiungi_Click(object sender, EventArgs e)
        {
            quantità = Convert.ToInt32(NumUpD_quantità.Value.ToString());
            ok = true;
            Close();
        }

        private void Form2_GiacenzaSottoComponenti_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!ok)
            {
                quantità = quantitàPreEsistente;
            }
            attendo = false;
        }

        private void Form2_GiacenzaSottoComponenti_Load(object sender, EventArgs e)
        {

        }
    }
}
