using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestione_materiali
{
    public partial class Form1 : Form
    {
        private List<Periodo> Form_Periodi;
        private Salvataggio salvataggio;
        string[] titoli = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };

        public Form1()
        {
            InitializeComponent();
            Form_Periodi = new List<Periodo>();
            salvataggio = new Salvataggio();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CaricaHeaderTabella();
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

        private void Btn_ProgrammazioneProduzione_Click(object sender, EventArgs e)
        {
            RecuperaDatiTabella();
            Produzione product = new Produzione(Form_Periodi);
            Form_Periodi = product.CalcolaProgrammazioneProduzione();
            AggiornaTabella();
        }

        // Prende i dati in input e li salva nella lista dei periodi
        void RecuperaDatiTabella()
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
        }

        // Dopo che è stata eseguita la programmazione della produzione, aggiorno i dati della tabella con i calcoli svolti
        void AggiornaTabella()
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = Form_Periodi[i - 1].Previsioni;
                dataGridView1.Rows[1].Cells[i].Value = Form_Periodi[i - 1].OrdiniVendita;
                dataGridView1.Rows[2].Cells[i].Value = Form_Periodi[i - 1].Giacenza;
                dataGridView1.Rows[3].Cells[i].Value = Form_Periodi[i - 1].Versamenti;
                dataGridView1.Rows[4].Cells[i].Value = Form_Periodi[i - 1].OrdiniProduzione;
            }
        }

        private void Btn_SalvataggioProgrammazione_Click(object sender, EventArgs e)
        {
            SaveFileDialog Sfd_Catalogo = new SaveFileDialog();
            Sfd_Catalogo.InitialDirectory = @"C:\";
            Sfd_Catalogo.RestoreDirectory = true;
            Sfd_Catalogo.FileName = "*.xml";
            Sfd_Catalogo.DefaultExt = "xml";
            Sfd_Catalogo.Filter = "xml files (*.xml)|*.xml";
            if (Sfd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                Stream filesStream = Sfd_Catalogo.OpenFile();
                StreamWriter sw = new StreamWriter(filesStream);
                //distintaBase.catalogo = NodiTreeView;
                salvataggio.SalvaProgrammazione(Form_Periodi, sw);
                sw.Close();
                filesStream.Close();
            }
        }
    }
}
