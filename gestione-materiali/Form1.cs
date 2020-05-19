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
        private bool TabellaGenerata;
        int NumPeriodi;

        string[] titoli = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };

        public Form1()
        {
            InitializeComponent();
            NumPeriodi = 6;
            TabellaGenerata = false;
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
                CaricaDatiTabellaInAlbero();
                Produzione product = new Produzione(distintaBase, NumPeriodi);
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

        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Produzione --> Salva
            if (TabellaGenerata)
            {
                distintaBase.Salva();
            }

            else
            {
                MessageBox.Show("Programma la produzione di una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void caricaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Produzione --> Carica
            SvuotaTabella();
            treeView_DistintaBase.Nodes.Clear();
            TreeNode treeNode = FormCaricaDistintaBase(0);
            if (treeNode != null)
            {
                treeView_DistintaBase.Nodes.Add(treeNode);
                AggiornaTabella(distintaBase.Albero.Produzione);
                TabellaGenerata = true;
            }

        }

        private void pulisciTabellaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Produzione --> Pulisci tabella
            if (TabellaGenerata)
            {
                distintaBase.ResettaProduzioneDistintaBase(distintaBase.Albero);
                SvuotaTabella();
                TabellaGenerata = false;
            }
            else
            {
                MessageBox.Show("Carica una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void caricaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Distinta base --> Carica
            SvuotaTabella();
            treeView_DistintaBase.Nodes.Clear();
            TreeNode treeNode = FormCaricaDistintaBase(dataGridView1.ColumnCount);
            if (treeNode != null)
            {
                treeView_DistintaBase.Nodes.Add(treeNode);
            }
        }

        private void treeView_DistintaBase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode treePadre = null;
            if (treeView_DistintaBase.SelectedNode.Parent != null) treePadre = treeView_DistintaBase.SelectedNode.Parent;
            Componente comp = distintaBase.TreeNodeToNode(e.Node, treePadre);
            if (distintaBase.Albero.Codice == comp.Codice || TabellaGenerata)
            {
                AggiornaTabella(comp.Produzione);
            }
            else
            {
                MessageBox.Show($"Programma la tabella di {distintaBase.Albero.Nome.ToUpper()}.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            ValidaCella();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int NumeroPeriodi = Convert.ToInt32(numericUpAndDown_Periodi.Value);
            if (NumeroPeriodi + 2 > dataGridView1.ColumnCount)
            {
                for (int i = dataGridView1.ColumnCount; i < NumeroPeriodi + 2; i++)
                {
                    DataGridViewColumn newColumn = new DataGridViewColumn();
                    newColumn.Width = 50;
                    newColumn.HeaderText = (i - 1).ToString();
                    newColumn.CellTemplate = new DataGridViewTextBoxCell();
                    dataGridView1.Columns.Add(newColumn);
                    dataGridView1.Columns[dataGridView1.Columns.Count - 1].ReadOnly = true;
                    dataGridView1.Rows[0].Cells[dataGridView1.Columns.Count - 1].ReadOnly = false;
                    dataGridView1.Rows[1].Cells[dataGridView1.Columns.Count - 1].ReadOnly = false;
                }
                NumPeriodi = NumeroPeriodi;
                distintaBase.NumPeriodi = NumeroPeriodi;
                return;
            }
            else
            {
                while (NumeroPeriodi + 2 < dataGridView1.ColumnCount)
                {
                    dataGridView1.Columns.RemoveAt(dataGridView1.ColumnCount - 1);
                }
            }
            NumPeriodi = NumeroPeriodi;
        }

        //
        // FINE ELEMENTI FORM
        //


        /// <summary>
        /// Cancella tutti i valori sulla tabella e li evidenzia per facilitare l'input all'utente.
        /// </summary>
        void SvuotaTabella()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!cell.ReadOnly)
                    {
                        cell.Value = null;
                    }
                    else
                    {
                        if (cell.Value == null || cell.Value.ToString().All(char.IsDigit))
                            cell.Value = null;
                    }
                }
            }
        }

        /// <summary>
        /// Carica una tabella con header fissi.
        /// </summary>
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

        /// <summary>
        /// Cambia lo stile della tabella.
        /// </summary>
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

        /// <summary>
        /// Prende i dati in input e li salva nella lista dei periodi.
        /// </summary>
        /// <returns></returns>
        void CaricaDatiTabellaInAlbero()
        {
            distintaBase.ResettaProduzioneDistintaBase(distintaBase.Albero);
            distintaBase.Albero.Produzione[0].Giacenza = Convert.ToInt32(dataGridView1.Rows[2].Cells[1].Value);
            distintaBase.Albero.Produzione[0].Versamenti = Convert.ToInt32(dataGridView1.Rows[3].Cells[1].Value);
            distintaBase.Albero.Produzione[0].OrdiniProduzione = Convert.ToInt32(dataGridView1.Rows[4].Cells[1].Value);

            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                distintaBase.Albero.Produzione[i - 1].Previsioni = Convert.ToInt32(dataGridView1.Rows[0].Cells[i].Value);
                distintaBase.Albero.Produzione[i - 1].OrdiniVendita = Convert.ToInt32(dataGridView1.Rows[1].Cells[i].Value);
            }
        }

        /// <summary>
        /// Evidenzia tutte le celle vuote.
        /// </summary>
        /// <returns></returns>
        bool ControllaCelleVuote()
        {
            bool Ok = true;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!cell.ReadOnly)
                    {
                        if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                        {
                            cell.Style.BackColor = Color.Tomato;
                            Ok = false;
                        }
                        else
                        {
                            cell.Style.BackColor = Color.White;
                        }
                    }
                }
            }
            return Ok;
        }

        /// <summary>
        ///  Dopo che è stata eseguita la programmazione della produzione, aggiorno i dati della tabella con i calcoli svolti.
        /// </summary>
        /// <param name="inputPeriodo"></param>
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

        /// <summary>
        /// Chiede all'utente una distinta base e succesivamente la carica nel programma.
        /// </summary>
        /// <returns></returns>        
        TreeNode FormCaricaDistintaBase(int NumPeriodi)
        {
            distintaBase.Albero = distintaBase.Carica(NumPeriodi);
            if (distintaBase.Albero != null)
            {
                Lbl_ComponenteCaricato.Text = $"Attualmente è caricata la distinta base '{distintaBase.Albero.Nome.ToUpper()}'";
            }

            else
            {
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

        /// <summary>
        /// Controlla se la cella selezionata è valida.
        /// </summary>
        void ValidaCella()
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
