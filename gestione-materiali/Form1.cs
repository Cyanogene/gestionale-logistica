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
        private string[] TitoliProduzioneVuota = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };
        private string[] TitoliProduzione = new string[]
        {
            "Fabbisogno lordo","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };
        private int NumPeriodi;
        ToolTip toolTip = new ToolTip();




        public Form1()
        {
            InitializeComponent();
            NumPeriodi = 6;
            TabellaGenerata = false;
            distintaBase = new DistintaBase();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CambiaStileETitoliTabella(TitoliProduzioneVuota);
            CambiaStileTabella();
            Btn_ProgrammazioneProduzione.UseCompatibleTextRendering = true;

            formOriginalSize = Size;

            menuStrip1OriginalRect = new Rectangle(menuStrip1.Location.X, menuStrip1.Location.Y, menuStrip1.Width, menuStrip1.Height);
            dataGridView1OriginalRect = new Rectangle(dataGridView1.Location.X, dataGridView1.Location.Y, dataGridView1.Width, dataGridView1.Height);
            label1OriginalRect = new Rectangle(label1.Location.X, label1.Location.Y, label1.Width, label1.Height);
            Lbl_ComponenteCaricatoOriginalRect = new Rectangle(Lbl_ComponenteCaricato.Location.X, Lbl_ComponenteCaricato.Location.Y, Lbl_ComponenteCaricato.Width, Lbl_ComponenteCaricato.Height);
            numericUpAndDown_PeriodiOriginalRect = new Rectangle(numericUpAndDown_Periodi.Location.X, numericUpAndDown_Periodi.Location.Y, numericUpAndDown_Periodi.Width, numericUpAndDown_Periodi.Height);
            treeView_DistintaBaseOriginalRect = new Rectangle(treeView_DistintaBase.Location.X, treeView_DistintaBase.Location.Y, treeView_DistintaBase.Width, treeView_DistintaBase.Height);
            Btn_ProgrammazioneProduzioneOriginalRect = new Rectangle(Btn_ProgrammazioneProduzione.Location.X, Btn_ProgrammazioneProduzione.Location.Y, Btn_ProgrammazioneProduzione.Width, Btn_ProgrammazioneProduzione.Height);
        }

        //resizeFormElement---------------------------------------------------------------------------------------------------------------------------------------------

        private void resizeChildrenControls()
        {
            resizeControl(menuStrip1OriginalRect, menuStrip1);
            resizeControl(dataGridView1OriginalRect, dataGridView1);
            resizeControl(label1OriginalRect, label1);
            resizeControl(Lbl_ComponenteCaricatoOriginalRect, Lbl_ComponenteCaricato);
            resizeControl(numericUpAndDown_PeriodiOriginalRect, numericUpAndDown_Periodi);
            resizeControl(treeView_DistintaBaseOriginalRect, treeView_DistintaBase);
            resizeControl(Btn_ProgrammazioneProduzioneOriginalRect, Btn_ProgrammazioneProduzione);
        }

        private void resizeControl(Rectangle originalControlRect, Control control)
        {
            float xRatio = (float)(Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(Height) / (float)(formOriginalSize.Height);

            int newX = (int)(originalControlRect.Location.X * xRatio);
            int newY = (int)(originalControlRect.Location.Y * yRatio);
            int newWidth = (int)(originalControlRect.Size.Width * xRatio);
            int newHeight = (int)(originalControlRect.Size.Height * yRatio);

            control.Location = new Point(newX, newY);
            control.Size = new Size(newWidth, newHeight);
        }

        private Rectangle menuStrip1OriginalRect;
        private Rectangle dataGridView1OriginalRect;
        private Rectangle label1OriginalRect;
        private Rectangle Lbl_ComponenteCaricatoOriginalRect;
        private Rectangle numericUpAndDown_PeriodiOriginalRect;
        private Rectangle treeView_DistintaBaseOriginalRect;
        private Rectangle Btn_ProgrammazioneProduzioneOriginalRect;

        private Size formOriginalSize;

        private void Form1_Resize(object sender, EventArgs e)
        {
            resizeChildrenControls();
        }


        // METODI FORM


        /// <summary>
        /// Avvia il calcolo della produzione
        /// </summary>
        private void Btn_ProgrammazioneProduzione_Click(object sender, EventArgs e)
        {
            if (distintaBase.Nodi.Count == 0)
            {
                MessageBox.Show("Carica una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ControlloCompilazioneCelle())
            {
                CaricaDatiTabellaInAlbero();
                Produzione product = new Produzione(distintaBase, NumPeriodi);
                product.avviaProduzione();
                dataGridView1.Rows.Clear();
                CambiaStileETitoliTabella(TitoliProduzione);
                //ControlloInputCella();
                AggiornaTabella(distintaBase.Albero.Produzione);
                TabellaGenerata = true;
                if (product.PeriodiNegativi > 0)
                {
                    MessageBox.Show("Bisogna anticipare la produzione di " + product.PeriodiNegativi + " periodi", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            else
            {
                return;
            }
        }

        /// <summary>
        /// salva la produzione che si sta visualizzando
        /// </summary>
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
            }

        }

        /// <summary>
        /// carica una produzione salvata in xml
        /// </summary>
        private void caricaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Produzione --> Carica
            TreeNode treeNode = FormCaricaProduzione();
            if (treeNode == null) return;
            TabellaGenerata = true;

            dataGridView1.Rows.Clear();
            CambiaStileETitoliTabella(TitoliProduzione);
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[4].Cells[i].ReadOnly = true;
                dataGridView1.Rows[4].Cells[i].Style.BackColor = Color.FromArgb(109, 125, 230);
            }
            ControlloInputCella();
            treeView_DistintaBase.Nodes.Clear();
            treeView_DistintaBase.Nodes.Add(treeNode);
            AggiornaTabella(distintaBase.Albero.Produzione);
            Lbl_ComponenteCaricato.Text = $"Attualmente è mostrata la tabella di '{distintaBase.Albero.Nome.ToUpper()}'";
        }

        /// <summary>
        /// carica una distinta base
        /// </summary>
        private void caricaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Distinta base --> Carica
            distintaBase.NumPeriodi = NumPeriodi;
            TreeNode treeNode = FormCaricaDistintaBase();
            if (treeNode == null) return;
            TabellaGenerata = false;
            dataGridView1.Rows.Clear();
            CambiaStileETitoliTabella(TitoliProduzioneVuota);
            CambiaStileTabella();
            treeView_DistintaBase.Nodes.Clear();
            treeView_DistintaBase.Nodes.Add(treeNode);
            Lbl_ComponenteCaricato.Text = $"Attualmente è mostrata la tabella di '{distintaBase.Albero.Nome.ToUpper()}'";
        }

        /// <summary>
        /// elimina tutti i dati dalla tabella
        /// </summary>
        private void pulisciTabellaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool tabellaVuota = true;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null || cell.Value.ToString().All(char.IsDigit))
                    {
                        if (!(string.IsNullOrWhiteSpace(cell.Value.ToString())))
                        {
                            tabellaVuota = false;
                        }
                    }
                }
            }
            if (!(tabellaVuota))
            {
                if (DialogResult.Yes == MessageBox.Show("Vuoi resettare la tabella?", "Gestione materiali", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    ControlloInputCella();
                    TabellaGenerata = false;
                    dataGridView1.Rows.Clear();
                    CambiaStileETitoliTabella(TitoliProduzioneVuota);
                    CambiaStileTabella();
                    distintaBase.ResettaProduzioneDistintaBase(distintaBase.Albero);
                }
            }

        }

        /// <summary>
        /// se si clicca con tasto dx su un nodo chiama il contextMenuStrip
        /// </summary>
        private void treeView_DistintaBase_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            TreeNode node_here = treeView_DistintaBase.GetNodeAt(e.X, e.Y);
            treeView_DistintaBase.SelectedNode = node_here;
            if (node_here == null) return;

            cms_DistintaBase.Show(treeView_DistintaBase, new Point(e.X, e.Y));
        }

        /// <summary>
        /// in fase di programmazione con doppio click sul nodo si può impostare la giacenza del nodo al periodo 0
        /// in fase di visualizzazione con doppio click sul nodo si visualizza la programmazione del nodo stesso
        /// </summary>
        private void treeView_DistintaBase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView_DistintaBase.SelectedNode == null) return;
            TreeNode treePadre = null;
            if (treeView_DistintaBase.SelectedNode.Parent != null) treePadre = treeView_DistintaBase.SelectedNode.Parent;
            Componente comp = distintaBase.TreeNodeToNode(e.Node, treePadre);
            if (comp != null)
            {
                if (TabellaGenerata)
                {
                    AggiornaTabella(comp.Produzione);
                    Lbl_ComponenteCaricato.Text = $"Attualmente è mostrata la tabella di '{comp.Nome.ToUpper()}'";
                }
                else
                {
                    if (treeView_DistintaBase.SelectedNode.Parent == null)
                    {
                        dataGridView1.CurrentCell = dataGridView1[1, 2];
                    }
                    else
                    {
                        Componente componenteSelezionato = distintaBase.TreeNodeToNode(treeView_DistintaBase.SelectedNode, treeView_DistintaBase.SelectedNode.Parent);
                        Form2_GiacenzaSottoComponenti form = new Form2_GiacenzaSottoComponenti(componenteSelezionato);//do in input il componente selezionato nella treeview
                        form.ShowDialog();
                        componenteSelezionato.Produzione[0].Giacenza = form.quantità;
                        while (form.attendo)
                        {

                        }
                    }
                }

            }
        }

        /// <summary>
        /// mostra le informazione del nodo selezionato
        /// </summary>
        private void informazioniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView_DistintaBase.SelectedNode == null) return;
            string Codice = treeView_DistintaBase.SelectedNode.Tag.ToString();
            Box.Show(InfoComponenteDistintabase(), "Distinta Base", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (distintaBase.Nodi.Count == 0)
            {
                MessageBox.Show("Carica una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.CurrentCell.Value = null;
                return;
            }
            ValidaCellaCorrente();
        }

        /// <summary>
        /// permette di selezionare il numero di periodi
        /// </summary>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (TabellaGenerata) return;
            int NumeroPeriodi = Convert.ToInt32(numericUpAndDown_Periodi.Value);
            if (NumeroPeriodi + 2 > dataGridView1.ColumnCount)
            {
                for (int i = dataGridView1.ColumnCount; i < NumeroPeriodi + 2; i++)
                {
                    DataGridViewColumn newColumn = new DataGridViewColumn();
                    newColumn.Width = 50;
                    newColumn.HeaderText = (i - 1).ToString();
                    newColumn.CellTemplate = periodo_0.CellTemplate;
                    dataGridView1.Columns.Add(newColumn);
                    dataGridView1.Columns[dataGridView1.Columns.Count - 1].ReadOnly = true;
                    dataGridView1.Rows[0].Cells[dataGridView1.Columns.Count - 1].ReadOnly = false;
                    dataGridView1.Rows[1].Cells[dataGridView1.Columns.Count - 1].ReadOnly = false;
                }
            }
            else
            {
                while (NumeroPeriodi + 2 < dataGridView1.ColumnCount)
                {
                    dataGridView1.Columns.RemoveAt(dataGridView1.ColumnCount - 1);
                }
            }
            NumPeriodi = NumeroPeriodi;
            distintaBase.NumPeriodi = NumeroPeriodi;
            CambiaStileCelleOutput();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == 4 && TabellaGenerata)
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
        }

        /// <summary>
        /// in fase di programmazione se si posiziona il mouse sopra un nodo si visualizza la giacenza al periodo 0 del nodo stesso
        /// </summary>
        private void treeView_DistintaBase_NodeMouseHover_1(object sender, TreeNodeMouseHoverEventArgs e)
        {
            toolTip.RemoveAll();
            if (TabellaGenerata) return;
            TreeNode treeNode = e.Node;
            Componente comp = new Componente();
            if (treeNode.Parent == null)
            {
                comp = distintaBase.Albero;
            }
            else
            {
                comp = distintaBase.TreeNodeToNode(treeNode, treeNode.Parent);
            }
            toolTip = new ToolTip();
            toolTip.SetToolTip(treeView_DistintaBase, "La giacenza nel primo periodo è " + comp.Produzione[0].Giacenza.ToString());
        }



        //
        // METODI D'APPOGGIO
        //

        private void CambiaStileCelleOutput()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ReadOnly && cell.Value == null)
                    {
                        cell.Style.BackColor = Color.LightGray;
                    }
                }
            }
        }


        /// <summary>
        /// Controlla se ogni cella ha caratteri diversi da numeri
        /// </summary>
        private void ControlloInputCella()
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
        /// controlla se sono state compilate abbastanza celle
        /// </summary>
        private bool ControlloCompilazioneCelle()
        {
            int CelleCompilate = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!cell.ReadOnly)
                    {
                        if (!(cell.Value == null))
                        {
                            if (!(string.IsNullOrWhiteSpace(cell.Value.ToString())))
                            {
                                CelleCompilate++;
                            }
                        }
                    }
                }
            }
            if (dataGridView1.Rows[3].Cells[1].Value == null)
            {
                if (DialogResult.Yes == MessageBox.Show("Non hai assegnato una giacenza iniziale, vuoi renderla uguale alla scorta di sicurezza?", "Gestione materiali", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    dataGridView1.Rows[2].Cells[1].Value = distintaBase.Albero.ScortaSicurezza;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (CelleCompilate < 5)
            {
                if (DialogResult.Yes == MessageBox.Show("Hai inserito poche informazioni, vuoi continuare?", "Gestione materiali", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    dataGridView1.Rows[3].Cells[1].Value = distintaBase.Albero.ScortaSicurezza;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Carica una tabella con header fissi.
        /// </summary>
        private void CambiaStileETitoliTabella(string[] titoli)
        {
            dataGridView1.Rows.Add(5);
            dataGridView1.AutoSize = true;

            for (int i = 0; i < titoli.Length; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = titoli[i];
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }

            if (TitoliProduzione == titoli)
            {
                numericUpAndDown_Periodi.Enabled = false;
                label1.Enabled = false;
                for (int i = 0; i < titoli.Length; i++)
                {
                    dataGridView1.Rows[i].ReadOnly = true;
                }
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Rows[4].Cells[i].Style.BackColor = Color.FromArgb(109, 125, 230);
                }
            }
            else
            {
                numericUpAndDown_Periodi.Enabled = true;
                label1.Enabled = true;
                for (int i = 0; i < titoli.Length; i++)
                {
                    if (i >= 2)
                    {
                        dataGridView1.Rows[i].ReadOnly = true;
                        dataGridView1.Rows[i].Cells[1].ReadOnly = false;
                    }
                }
                CambiaStileCelleOutput();
            }
        }

        /// <summary>
        /// Cambia lo stile della tabella.
        /// </summary>
        private void CambiaStileTabella()
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
        /// Salva i dati (in input) inseriti nella tabella.
        /// </summary>
        private void CaricaDatiTabellaInAlbero()
        {
            distintaBase.ResettaProduzioneDistintaBaseDaForm(distintaBase.Albero);
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
        /// Dopo aver eseguito la programmazione della produzione, aggiorno i dati della tabella con i calcoli svolti.
        /// </summary>
        /// <param name="inputPeriodo">Lista di periodi contenente la programmazione della produzione del componente.</param>
        private void AggiornaTabella(List<Periodo> inputPeriodo)
        {
            for (int i = 1; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = inputPeriodo[i - 1].FabbisognoLordo;
                dataGridView1.Rows[1].Cells[i].Value = inputPeriodo[i - 1].Giacenza;
                dataGridView1.Rows[2].Cells[i].Value = inputPeriodo[i - 1].Versamenti;
                dataGridView1.Rows[3].Cells[i].Value = inputPeriodo[i - 1].OrdiniProduzione;
            }
        }

        /// <summary>
        /// Chiede all'utente di selezionare una distinta base e succesivamente la carica nel programma.
        /// </summary>       
        private TreeNode FormCaricaDistintaBase()
        {
            distintaBase.NumPeriodi = NumPeriodi;
            distintaBase.Albero = distintaBase.Carica();

            if (distintaBase.Albero == null)
            {
                return null;
            }


            return distintaBase.NodeToTreeNode(distintaBase.Albero);
        }

        /// <summary>
        /// Chiede all'utente di selezionare una produzione e succesivamente la carica nel programma.
        /// </summary>
        private TreeNode FormCaricaProduzione()
        {
            distintaBase.Albero = distintaBase.CaricaProduzione();
            if (distintaBase.Albero.Produzione.Count() == 0)
            {
                MessageBox.Show("File non valido.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            NumPeriodi = distintaBase.Albero.Produzione.Count() - 1;
            distintaBase.NumPeriodi = NumPeriodi;

            if (NumPeriodi + 2 > dataGridView1.ColumnCount)
            {
                for (int i = dataGridView1.ColumnCount; i < NumPeriodi + 2; i++)
                {
                    DataGridViewColumn newColumn = new DataGridViewColumn();
                    newColumn.Width = 50;
                    newColumn.HeaderText = (i - 1).ToString();
                    newColumn.CellTemplate = periodo_0.CellTemplate;
                    dataGridView1.Columns.Add(newColumn);
                    dataGridView1.Columns[dataGridView1.Columns.Count - 1].ReadOnly = true;
                    dataGridView1.Rows[0].Cells[dataGridView1.Columns.Count - 1].ReadOnly = false;
                    dataGridView1.Rows[1].Cells[dataGridView1.Columns.Count - 1].ReadOnly = false;
                }
            }
            else
            {
                while (NumPeriodi + 2 < dataGridView1.ColumnCount)
                {
                    dataGridView1.Columns.RemoveAt(dataGridView1.ColumnCount - 1);
                }
            }
            numericUpAndDown_Periodi.Value = NumPeriodi;

            if (distintaBase.Albero == null)
            {
                return null;
            }


            return distintaBase.NodeToTreeNode(distintaBase.Albero);
        }

        /// <summary>
        /// Pulisce tutte le celle della dataGridView
        /// </summary>
        private void PulisciTabella()
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

        /// <summary>
        /// Controlla se la cella selezionata è valida.
        /// </summary>
        private void ValidaCellaCorrente()
        {
            if (dataGridView1.CurrentCell.Value != null)
            {
                bool ris = dataGridView1.CurrentCell.Value.ToString().All(char.IsDigit);

                if (!ris)
                {
                    MessageBox.Show("Inserisci un numero.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.CurrentCell.Value = null;
                }

            }
        }

        /// <summary>
        /// Ritorna una stringa contenente le informazioni sul componenete selezionato.
        /// </summary>
        private string InfoComponenteDistintabase()
        {
            TreeNode treePadre = null;
            if (treeView_DistintaBase.SelectedNode.Parent != null)
                treePadre = treeView_DistintaBase.SelectedNode.Parent;
            Componente componente = distintaBase.TreeNodeToNode(treeView_DistintaBase.SelectedNode, treePadre);
            if (componente == null) return "selezionare un componente";
            return "NOME --> " + componente.Nome + "\nCODICE --> " + componente.Codice + "\nDESCRIZIONE --> " + componente.Descrizione + "\nLEAD TIME --> " + componente.LeadTime + "\nLEAD TIME SICUREZZA --> " + componente.LeadTimeSicurezza + "\nLOTTO --> " + componente.Lotto + "\nSCORTA DI SICUREZZA --> " + componente.ScortaSicurezza + "\nPERIODO DI COPERTURA --> " + componente.PeriodoDiCopertura;
        }

    }
}
