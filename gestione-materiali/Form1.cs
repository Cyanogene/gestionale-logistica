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
        private DistintaBase distintaBase;
        private bool TabellaGenerata = false;

        string[] titoli = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };

        public Form1()
        {
            InitializeComponent();
            distintaBase = new DistintaBase();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CaricaHeaderTabella();
            CambiaStileTabella();
            Btn_ProgrammazioneProduzione.UseCompatibleTextRendering = true;
        }

        private void Btn_ProgrammazioneProduzione_Click(object sender, EventArgs e)
        {
            if (distintaBase.Nodi.Count == 0)
            {
                MessageBox.Show("Carica una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ControllaCelleVuote())
            {
                List<int> p = RecuperaDatiTabellaAlbero();
                Produzione product = new Produzione(distintaBase, p);
                product.avviaProduzione();
                AggiornaTabella(distintaBase.Albero.Produzione);
                TabellaGenerata = true;
            }

            else
            {
                MessageBox.Show("Riempi tutti i campi evidenziati.", "Gestione Materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (distintaBase.Nodi.Count == 0)
            {
                MessageBox.Show("Carica una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.CurrentCell.Value = null;
                return;
            }
            ValidaCelle();
        }

        private void caricaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Produzione --> Carica
        }

        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Produzione --> Salva
            Salva(distintaBase.Albero.Produzione);
        }

        private void caricaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Distinta base --> Carica
            treeView_DistintaBase.Nodes.Clear();
            TreeNode treeNode = FormCaricaDistintaBase();
            if (treeNode != null)
            {
                treeView_DistintaBase.Nodes.Add(treeNode);
            }
            ResettaTabella();
        }

        private void treeView_DistintaBase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Componente comp = distintaBase.TreeNodeToNode(e.Node);
            if (distintaBase.Albero.Codice == comp.Codice || TabellaGenerata)
            {
                Lbl_Tree.Text = $"Attualmente è presente la tabella del componente {e.Node.Text.ToUpper()}.";
                AggiornaTabella(comp.Produzione);
            }
            else
            {
                MessageBox.Show($"Programma la tabella di {distintaBase.Albero.Nome.ToUpper()}.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void resettaTabellaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResettaProduzioneDistintaBase(distintaBase.Albero);
            ResettaTabella();
        }


        //
        // FINE BUTTON / ELEMENTI FORM
        //


        // da spostare
        void ResettaProduzioneDistintaBase(Componente comp)
        {
            comp.Produzione = new List<Periodo>() { new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo() };
            foreach (Componente component in comp.SottoNodi)
            {
                component.Produzione = new List<Periodo>() { new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo() };
            }
        }

        void ResettaTabella()
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = null;
                dataGridView1.Rows[1].Cells[i].Value = null;
                dataGridView1.Rows[2].Cells[i].Value = null;
                dataGridView1.Rows[3].Cells[i].Value = null;
                dataGridView1.Rows[4].Cells[i].Value = null;
            }
        }

        // da spostare / modificare: salvare la DistintaBase caricata coi valori aggiornata
        void Salva(List<Periodo> p)
        {
            SaveFileDialog Sfd_Catalogo = new SaveFileDialog();
            Sfd_Catalogo.InitialDirectory = @"C:\";
            Sfd_Catalogo.RestoreDirectory = true;
            Sfd_Catalogo.FileName = "*_Produzione.xml";
            Sfd_Catalogo.DefaultExt = "xml";
            Sfd_Catalogo.Filter = "xml files (*.xml)|*.xml";

            if (Sfd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                Stream filesStream = Sfd_Catalogo.OpenFile();
                StreamWriter sw = new StreamWriter(filesStream);
                XmlSerializer serializer = new XmlSerializer(typeof(Componente));
                serializer.Serialize(sw, distintaBase.Albero);
                sw.Close();
                filesStream.Close();
            }
        }

        // Carica una tabella con header fissi.
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

        // Cambia lo stile della datagridview.
        void CambiaStileTabella()
        {
            dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("Verdana", 9F, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 9F, FontStyle.Regular);
            dataGridView1.BorderStyle = BorderStyle.None;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.BackgroundColor = Color.FromArgb(240, 240, 240);
            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(109, 125, 230);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.CurrentCell.Selected = false;
        }

        // Prende i dati in input e li salva nella lista dei periodi.
        List<int> RecuperaDatiTabellaAlbero()
        {
            List<int> max = new List<int>();
            distintaBase.Albero.Produzione[0].Giacenza = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value);
            distintaBase.Albero.Produzione[0].Versamenti = Convert.ToInt32(dataGridView1.Rows[3].Cells[1].Value);
            distintaBase.Albero.Produzione[0].OrdiniProduzione = Convert.ToInt32(dataGridView1.Rows[4].Cells[1].Value);

            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                max.Add(Math.Max(Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value), Convert.ToInt32(dataGridView1.Rows[1].Cells[i].Value)));
                distintaBase.Albero.Produzione[i - 1].Previsioni = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                distintaBase.Albero.Produzione[i - 1].OrdiniVendita = Convert.ToInt32(dataGridView1.Rows[1].Cells[i].Value);
            }
            return max;
        }

        // Evidenzia tutte le celle vuote.
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
        void AggiornaTabella(List<Periodo> inputPeriodo)
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = inputPeriodo[i - 1].Previsioni;
                dataGridView1.Rows[1].Cells[i].Value = inputPeriodo[i - 1].OrdiniVendita;
                dataGridView1.Rows[2].Cells[i].Value = inputPeriodo[i - 1].Giacenza;
                dataGridView1.Rows[3].Cells[i].Value = inputPeriodo[i - 1].Versamenti;
                dataGridView1.Rows[4].Cells[i].Value = inputPeriodo[i - 1].OrdiniProduzione;
            }
        }

        // Chiede all'utente una distinta base e succesivamente la carica nel programma.
        TreeNode FormCaricaDistintaBase()
        {
            distintaBase.Albero = distintaBase.CaricaDistintaBase();
            if (distintaBase.Albero != null)
            {
                Lbl_ComponenteCaricato.Text = $"Attualmente è caricata la distinta base '{distintaBase.Albero.Nome.ToUpper()}'";
            }

            else
            {
                MessageBox.Show("Nessun componente è stato caricato.", "Gestione Materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = null;
                dataGridView1.Rows[1].Cells[i].Value = null;
                dataGridView1.Rows[2].Cells[i].Value = null;
                dataGridView1.Rows[3].Cells[i].Value = null;
                dataGridView1.Rows[4].Cells[i].Value = null;
            }

            ControllaCelleVuote();
            return distintaBase.NodeToTreeNode(distintaBase.Albero);
        }

        // Controlla se la cella selezionata è valida.
        void ValidaCelle()
        {
            if (dataGridView1.CurrentCell.Value != null)
            {
                bool ris = dataGridView1.CurrentCell.Value.ToString().All(char.IsDigit);

                if (!ris)
                {
                    MessageBox.Show("Inserisci un numero.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.CurrentCell.Value = null;
                    dataGridView1.CurrentCell.Style.BackColor = Color.Tomato;
                }

                else
                {
                    dataGridView1.CurrentCell.Style.BackColor = Color.White;
                }
            }
            else
            {
                MessageBox.Show("Inserisci un numero.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.CurrentCell.Value = null;
                dataGridView1.CurrentCell.Style.BackColor = Color.Tomato;
            }
        }
    }
}
