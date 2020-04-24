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
        /// <summary>
        /// Distinta base caricata in input. MODIFICARE SOLO QUANDO SI CAMBIA DISTINTA BASE
        /// </summary>
        private Componente inputDistintaBase;
        private Componente componenteAttuale;
        private List<DataGridCell> appo;
        private Programmazione program;
        private DistintaBase distintaBase;

        string[] titoli = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };

        public Form1()
        {
            InitializeComponent();
            componenteAttuale = new Componente();
            appo = new List<DataGridCell>();
            program = new Programmazione();
            distintaBase = new DistintaBase();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CaricaHeaderTabella();
            CambiaStileTabella();
        }

        private void Btn_ProgrammazioneProduzione_Click(object sender, EventArgs e)
        {
            Btn_ProgrammazioneProduzione.UseCompatibleTextRendering = true;
            if (inputDistintaBase == null)
            {
                MessageBox.Show("Carica una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TreeNode treeNode = FormCaricaDistintaBase();
                if (treeNode != null)
                {
                    treeView_DistintaBase.Nodes.Add(treeNode);
                }
                return;
            }

            if (ControllaCelleVuote())
            {
                RecuperaDatiTabella();
                Produzione product = new Produzione(componenteAttuale.Produzione, componenteAttuale);
                componenteAttuale.Produzione = product.CalcolaProgrammazioneProduzione();
                AggiornaTabella(componenteAttuale.Produzione);
            }

            else
            {
                MessageBox.Show("Riempi tutti i campi evidenziati.", "Gestione Materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (inputDistintaBase == null)
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
            program.Salva(inputDistintaBase.Produzione);
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
        }

        private void treeView_DistintaBase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Lbl_Tree.Text = $"Attualmente è presente la tabella del componente {e.Node.Text}.";
            componenteAttuale = ComponenteDaCodice(e.Node.Text, inputDistintaBase);
            if (componenteAttuale.Produzione.Count == 0)
                RecuperaDatiTabella();
            else
                AggiornaTabella(componenteAttuale.Produzione);
        }

        public Componente ComponenteDaCodice(string codice, Componente componente)
        {
            if (componente.Nome == codice)
                return componente;

            foreach (Componente comp in componente.SottoNodi)
            {
                if (comp.Nome == codice)
                {
                    componenteAttuale = comp;
                }
                else
                {
                    ComponenteDaCodice(codice, comp);
                }
            }
            return componenteAttuale;
        }
        // 

        //
        // FINE BUTTON / ELEMENTI FORM
        //


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
        void RecuperaDatiTabella()
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                componenteAttuale.Produzione[i - 1].Previsioni = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                componenteAttuale.Produzione[i - 1].OrdiniVendita = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                componenteAttuale.Produzione[i - 1].Giacenza = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                componenteAttuale.Produzione[i - 1].Versamenti = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                componenteAttuale.Produzione[i - 1].OrdiniProduzione = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
            }
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
            inputDistintaBase = program.CaricaDistintaBase(inputDistintaBase);
            if (inputDistintaBase != null)
            {
                Lbl_ComponenteCaricato.Text = $"Attualmente è caricata la distinta base '{inputDistintaBase.Nome.ToUpper()}'";
            }

            else
            {
                MessageBox.Show("Nessun componente è stato caricato.", "Gestione Materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            distintaBase.AggiungiANodiDistintaBase(inputDistintaBase);
            ControllaCelleVuote();
            MessageBox.Show("Riempi tutti i campi evidenziati.", "Gestione Materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return distintaBase.NodeToTreeNode(inputDistintaBase);
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
