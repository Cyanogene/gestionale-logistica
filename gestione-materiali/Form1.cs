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
        private List<Periodo> asd;
        string[] titoli = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino",
            "Da versare a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };

        public Form1()
        {
            asd = new List<Periodo>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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

        private void Btn_ProgrammazioneProduzione_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 1; j < dataGridView1.Rows[i].Cells.Count + 1; j++)
                {
                    int p = 
                }
            }

        }
    }
}
