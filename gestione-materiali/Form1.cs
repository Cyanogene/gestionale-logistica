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
    public partial class Form1 : Form
    {
        private List<Periodo> Form_Periodi;
        string[] titoli = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };

        public Form1()
        {
            Form_Periodi = new List<Periodo>();
            InitializeComponent();
        }

        // Carico tabella con header fissi
        void CaricaHeaderTabella()
        {
            dataGridView1.Rows.Add(5);
            dataGridView1.AutoSize = true;

            for (int i = 0; i < titoli.Length; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = titoli[i];
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;

                if (i >= 2)
                {
                    dataGridView1.Rows[i].ReadOnly = true;
                    dataGridView1.Rows[i].Cells[1].ReadOnly = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CaricaHeaderTabella();
        }

        private void Btn_ProgrammazioneProduzione_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                Form_Periodi.Add(new Periodo()
                {
                    Previsioni = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value ?? -1), // Se valore == null, la variabile è uguale a -1
                    OrdiniVendita = Convert.ToInt32(dataGridView1.Rows[1].Cells[i].Value ?? -1),
                    Giacenza = Convert.ToInt32(dataGridView1.Rows[2].Cells[i].Value ?? -1),
                    Versamenti = Convert.ToInt32(dataGridView1.Rows[3].Cells[i].Value ?? -1),
                    OrdiniProduzione = Convert.ToInt32(dataGridView1.Rows[4].Cells[i].Value ?? -1)
                });
            }

            Produzione product = new Produzione(Form_Periodi);
        }
    }
}
