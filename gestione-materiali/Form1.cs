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
using System.Xml.Serialization;

namespace gestione_materiali
{
    public partial class Form1 : Form
    {
        private List<Periodo> Form_Periodi;
        private Componente componente;
        private List<DataGridCell> appo;
        string[] titoli = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };

        public Form1()
        {
            InitializeComponent();
            Form_Periodi = new List<Periodo>();
            appo = new List<DataGridCell>();
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
            if (ControllaCelleVuote())
            {
                RecuperaDatiTabella();
                Produzione product = new Produzione(Form_Periodi, componente);
                Form_Periodi = product.CalcolaProgrammazioneProduzione();
                AggiornaTabella();
            }

            else
            {
                MessageBox.Show("Riempi tutti i campi evidenziati.", "Gestione Materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        // Prende i dati in input e li salva nella lista dei periodi
        void RecuperaDatiTabella()
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                Form_Periodi.Add(new Periodo()
                {
                    Previsioni = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value),
                    OrdiniVendita = Convert.ToInt32(dataGridView1.Rows[1].Cells[i].Value),
                    Giacenza = Convert.ToInt32(dataGridView1.Rows[2].Cells[i].Value),
                    Versamenti = Convert.ToInt32(dataGridView1.Rows[3].Cells[i].Value),
                    OrdiniProduzione = Convert.ToInt32(dataGridView1.Rows[4].Cells[i].Value)
                });
            }
        }

        bool ControllaCelleVuote()
        {
            bool ris = true;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!cell.ReadOnly)
                    {
                        if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                        {
                            cell.Style.BackColor = Color.Tomato;
                            ris = false;
                        }
                        else
                        {
                            cell.Style.BackColor = Color.White;
                        }
                    }
                }
            }
            return ris;
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
                XmlSerializer serializer = new XmlSerializer(typeof(List<Periodo>));
                serializer.Serialize(sw, Form_Periodi);
                filesStream.Close();
                sw.Close();
            }
        }

        private void Btn_CaricaComponente_Click(object sender, EventArgs e)
        {
            OpenFileDialog Ofd_Catalogo = new OpenFileDialog();
            Ofd_Catalogo.InitialDirectory = @"C:\";
            Ofd_Catalogo.Filter = "XML|*.xml";

            if (Ofd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Ofd_Catalogo.FileName))
                {
                    StreamReader stream = new StreamReader(Ofd_Catalogo.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(Componente));
                    componente = (Componente)serializer.Deserialize(stream);
                    stream.Close();
                    Lbl_ComponenteCaricato.Text = $"Attualmente è caricato il componente '{componente.Nome.ToUpper()}'";
                    Lbl_ComponenteCaricato.Visible = true;
                }
            }
        }
    }
}
