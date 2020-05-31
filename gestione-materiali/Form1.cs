using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace gestione_materiali
{
    public partial class Form1 : Form
    {
        private DistintaBase DistintaBase;
        private bool TabellaGenerata;
        private string[] TitoliProduzioneVuota = new string[]
        {
            "Previsioni di vendita","Ordini di vendita","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };
        private string[] TitoliProduzione = new string[]
        {
            "Fabbisogno lordo (max tra ordini e previsioni)","Disponibilità a magazzino (giacenza)",
            "Versamenti a magazzino entro fine periodo","Ordini di produzione da lanciare a inizio periodo"
        };
        private int NumPeriodi;
        ToolTip ToolTip = new ToolTip();




        public Form1()
        {
            InitializeComponent();
            NumPeriodi = 6;
            TabellaGenerata = false;
            DistintaBase = new DistintaBase();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CambiaStileETitoliTabella(TitoliProduzioneVuota);
            CambiaStileTabella();
            Btn_ProgrammazioneProduzione.UseCompatibleTextRendering = true;

            formOriginalSize = Size;

            menuStrip1OriginalRect = new Rectangle(menuStrip1.Location.X, menuStrip1.Location.Y, menuStrip1.Width, menuStrip1.Height);
            dataGridView1OriginalRect = new Rectangle(DataGridView_Produzione.Location.X, DataGridView_Produzione.Location.Y, DataGridView_Produzione.Width, DataGridView_Produzione.Height);
            label1OriginalRect = new Rectangle(label1.Location.X, label1.Location.Y, label1.Width, label1.Height);
            Lbl_ComponenteCaricatoOriginalRect = new Rectangle(Lbl_ComponenteCaricato.Location.X, Lbl_ComponenteCaricato.Location.Y, Lbl_ComponenteCaricato.Width, Lbl_ComponenteCaricato.Height);
            numericUpAndDown_PeriodiOriginalRect = new Rectangle(NumericUpAndDown_Periodi.Location.X, NumericUpAndDown_Periodi.Location.Y, NumericUpAndDown_Periodi.Width, NumericUpAndDown_Periodi.Height);
            treeView_DistintaBaseOriginalRect = new Rectangle(TreeView_DistintaBase.Location.X, TreeView_DistintaBase.Location.Y, TreeView_DistintaBase.Width, TreeView_DistintaBase.Height);
            Btn_ProgrammazioneProduzioneOriginalRect = new Rectangle(Btn_ProgrammazioneProduzione.Location.X, Btn_ProgrammazioneProduzione.Location.Y, Btn_ProgrammazioneProduzione.Width, Btn_ProgrammazioneProduzione.Height);
        }

        //resizeFormElement---------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Se avviene un resize, questo metodo si occupa di chiamare tutti i metodi necessari
        /// </summary>
        private void resizeChildrenControls()
        {
            resizeControl(menuStrip1OriginalRect, menuStrip1);
            resizeControl(dataGridView1OriginalRect, DataGridView_Produzione);
            resizeControl(label1OriginalRect, label1);
            resizeControl(Lbl_ComponenteCaricatoOriginalRect, Lbl_ComponenteCaricato);
            resizeControl(numericUpAndDown_PeriodiOriginalRect, NumericUpAndDown_Periodi);
            resizeControl(treeView_DistintaBaseOriginalRect, TreeView_DistintaBase);
            resizeControl(Btn_ProgrammazioneProduzioneOriginalRect, Btn_ProgrammazioneProduzione);
        }

        /// <summary>
        /// Metodo principale che sposta il componente in una posizione adeguata alla nuova finestra.
        /// </summary>
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
        /// Avvia il calcolo della produzione della distinta base.
        /// </summary>
        private void Btn_ProgrammazioneProduzione_Click(object sender, EventArgs e)
        {
            if (DistintaBase.Nodi.Count == 0)
            {
                MessageBox.Show("Carica una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ControlloCompilazioneCelle())
            {
                CaricaDatiTabellaInAlbero();

                Produzione Product = new Produzione(DistintaBase, NumPeriodi);
                Product.AvviaProduzione();

                DataGridView_Produzione.Rows.Clear();
                PulisciTabella();
                CambiaStileETitoliTabella(TitoliProduzione);
                AggiornaTabella(DistintaBase.Albero.Produzione);

                TabellaGenerata = true;
                Lbl_ComponenteCaricato.Text = $"Attualmente è mostrata la tabella di '{DistintaBase.Albero.Nome.ToUpper()}'";

                if (Product.PeriodiNegativi > 0)
                {
                    MessageBox.Show("Bisogna anticipare la produzione di " + Product.PeriodiNegativi + " periodi", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            else
            {
                return;
            }
        }

        /// <summary>
        /// Salva la produzione che si sta visualizzando.
        /// </summary>
        private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Produzione --> Salva
            if (TabellaGenerata)
            {
                DistintaBase.SalvaProduzione();
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
            TreeNode TreeNode = FormCaricaProduzione();
            if (TreeNode == null) return;
            TabellaGenerata = true;

            DataGridView_Produzione.Rows.Clear();
            CambiaStileETitoliTabella(TitoliProduzione);
            /*for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[4].Cells[i].ReadOnly = true;
                dataGridView1.Rows[4].Cells[i].Style.BackColor = Color.FromArgb(109, 125, 230);
            }*/
            TreeView_DistintaBase.Nodes.Clear();
            TreeView_DistintaBase.Nodes.Add(TreeNode);
            TreeView_DistintaBase.ExpandAll();
            AggiornaTabella(DistintaBase.Albero.Produzione);
            Lbl_ComponenteCaricato.Text = $"Attualmente è mostrata la tabella di '{DistintaBase.Albero.Nome.ToUpper()}'";
        }

        /// <summary>
        /// carica una distinta base
        /// </summary>
        private void caricaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Distinta base --> Carica
            DistintaBase.NumPeriodi = NumPeriodi;
            TreeNode treeNode = FormCaricaDistintaBase();
            if (treeNode == null) return;
            TabellaGenerata = false;
            DataGridView_Produzione.Rows.Clear();
            CambiaStileETitoliTabella(TitoliProduzioneVuota);
            CambiaStileTabella();
            TreeView_DistintaBase.Nodes.Clear();
            TreeView_DistintaBase.Nodes.Add(treeNode);
            TreeView_DistintaBase.ExpandAll();
            Lbl_ComponenteCaricato.Text = $"Attualmente è mostrata la tabella di '{DistintaBase.Albero.Nome.ToUpper()}'";
        }

        /// <summary>
        /// Cliccata la voce info nel contextMenu viene chiamato il link dove è presente il tutorial se viene cliccato il si.
        /// </summary>
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vuoi aprire il manuale online?", "Gestione materiali", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://github.com/Cyanogene/gestionale-logistica/blob/master/README.md");
            }
        }

        /// <summary>
        /// elimina tutti i dati dalla tabella
        /// </summary>
        private void pulisciTabellaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool TabellaVuota = true;
            foreach (DataGridViewRow Row in DataGridView_Produzione.Rows)
            {
                foreach (DataGridViewCell Cell in Row.Cells)
                {
                    if (!(Cell.Value == null))
                    {
                        if (Cell.Value.ToString().All(char.IsDigit))
                        {
                            if (!(string.IsNullOrWhiteSpace(Cell.Value.ToString())))
                            {
                                TabellaVuota = false;
                            }
                        }
                    }
                }
            }
            if (!(TabellaVuota))
            {
                if (DialogResult.Yes == MessageBox.Show("Vuoi resettare la tabella?", "Gestione materiali", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    //ResettaCelle();
                    TabellaGenerata = false;
                    DataGridView_Produzione.Rows.Clear();
                    CambiaStileETitoliTabella(TitoliProduzioneVuota);
                    CambiaStileTabella();
                    DistintaBase.ResettaProduzioneDistintaBase(DistintaBase.Albero);
                }
            }
            TabellaGenerata = false;
            Lbl_ComponenteCaricato.Text = "";
        }

        /// <summary>
        /// se si clicca con tasto dx su un nodo chiama il contextMenuStrip
        /// </summary>
        private void treeView_DistintaBase_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            TreeNode Node_here = TreeView_DistintaBase.GetNodeAt(e.X, e.Y);
            TreeView_DistintaBase.SelectedNode = Node_here;
            if (Node_here == null) return;

            cms_DistintaBase.Show(TreeView_DistintaBase, new Point(e.X, e.Y));
        }

        /// <summary>
        /// in fase di programmazione con doppio click sul nodo si può impostare la giacenza del nodo al periodo 0
        /// in fase di visualizzazione con doppio click sul nodo si visualizza la programmazione del nodo stesso
        /// </summary>
        private void treeView_DistintaBase_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (TreeView_DistintaBase.SelectedNode == null) return;
            TreeNode TreeNode = null;
            if (TreeView_DistintaBase.SelectedNode.Parent != null) TreeNode = TreeView_DistintaBase.SelectedNode.Parent;
            Componente comp = DistintaBase.TreeNodeToNode(e.Node, TreeNode);
            if (comp != null)
            {
                if (TabellaGenerata)
                {
                    AggiornaTabella(comp.Produzione);
                    Lbl_ComponenteCaricato.Text = $"Attualmente è mostrata la tabella di '{comp.Nome.ToUpper()}'";
                }
                else
                {
                    if (TreeView_DistintaBase.SelectedNode.Parent == null)
                    {
                        DataGridView_Produzione.CurrentCell = DataGridView_Produzione[1, 2];
                    }
                    else
                    {
                        Componente ComponenteSelezionato = DistintaBase.TreeNodeToNode(TreeView_DistintaBase.SelectedNode, TreeView_DistintaBase.SelectedNode.Parent);
                        Form2_GiacenzaSottoComponenti Form2 = new Form2_GiacenzaSottoComponenti(ComponenteSelezionato);//do in input il componente selezionato nella treeview
                        Form2.ShowDialog();
                        ComponenteSelezionato.Produzione[0].Giacenza = Form2.quantità;
                        while (Form2.attendo)
                        {

                        }
                    }
                }

            }
        }

        /// <summary>
        /// in fase di programmazione se si posiziona il mouse sopra un nodo si visualizza la giacenza al periodo 0 del nodo stesso
        /// </summary>
        private void treeView_DistintaBase_NodeMouseHover_1(object sender, TreeNodeMouseHoverEventArgs e)
        {
            ToolTip.RemoveAll();
            if (TabellaGenerata) return;
            TreeNode TreeNode = e.Node;
            Componente Comp = new Componente();
            string Giacenza = "";
            if (TreeNode.Parent == null)
            {
                if (DataGridView_Produzione.Rows[2].Cells[1].Value != null)
                {
                    Giacenza = DataGridView_Produzione.Rows[2].Cells[1].Value.ToString();
                }
                else
                {
                    Giacenza = "0";
                }
            }
            else
            {
                Comp = DistintaBase.TreeNodeToNode(TreeNode, TreeNode.Parent);
                Giacenza = Comp.Produzione[0].Giacenza.ToString();
            }
            ToolTip = new ToolTip();
            ToolTip.SetToolTip(TreeView_DistintaBase, "La giacenza nel primo periodo è " + Giacenza);
        }

        /// <summary>
        /// mostra le informazione del nodo selezionato
        /// </summary>
        private void informazioniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TreeView_DistintaBase.SelectedNode == null) return;
            string Codice = TreeView_DistintaBase.SelectedNode.Tag.ToString();
            Box.Show(InfoComponenteDistintabase(), "Distinta Base", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// permette di selezionare il numero di periodi
        /// </summary>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (TabellaGenerata) return;
            int NumeroPeriodi = Convert.ToInt32(NumericUpAndDown_Periodi.Value);
            if (NumeroPeriodi + 2 > DataGridView_Produzione.ColumnCount)
            {
                for (int i = DataGridView_Produzione.ColumnCount; i < NumeroPeriodi + 2; i++)
                {
                    DataGridViewColumn newColumn = new DataGridViewColumn();
                    newColumn.Width = 50;
                    newColumn.HeaderText = (i - 1).ToString();
                    newColumn.CellTemplate = periodo_0.CellTemplate;
                    DataGridView_Produzione.Columns.Add(newColumn);
                    DataGridView_Produzione.Columns[DataGridView_Produzione.Columns.Count - 1].ReadOnly = true;
                    DataGridView_Produzione.Rows[0].Cells[DataGridView_Produzione.Columns.Count - 1].ReadOnly = false;
                    DataGridView_Produzione.Rows[1].Cells[DataGridView_Produzione.Columns.Count - 1].ReadOnly = false;
                }
            }
            else
            {
                while (NumeroPeriodi + 2 < DataGridView_Produzione.ColumnCount)
                {
                    DataGridView_Produzione.Columns.RemoveAt(DataGridView_Produzione.ColumnCount - 1);
                }
            }
            NumPeriodi = NumeroPeriodi;
            DistintaBase.NumPeriodi = NumeroPeriodi;
            CambiaStileCelleOutput();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (DistintaBase.Nodi.Count == 0)
            {
                MessageBox.Show("Carica una distinta base.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataGridView_Produzione.CurrentCell.Value = null;
                return;
            }
            ValidaCellaCorrente();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == 4 && TabellaGenerata)
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
        }




        //
        // METODI D'APPOGGIO
        //


        /// <summary>
        /// Imposta in colore grigio chiaro le celle della dtgView che non possono essere compilate.
        /// </summary>
        private void CambiaStileCelleOutput()
        {
            foreach (DataGridViewRow Row in DataGridView_Produzione.Rows)
            {
                foreach (DataGridViewCell Cell in Row.Cells)
                {
                    if (Cell.ReadOnly && Cell.Value == null)
                    {
                        Cell.Style.BackColor = Color.LightGray;
                    }
                }
            }
        }

        /// <summary>
        /// Controlla che siano state compilate le celle necessarie alla produzione.
        /// </summary>
        private bool ControlloCompilazioneCelle()
        {
            int CelleCompilate = 0;
            foreach (DataGridViewRow row in DataGridView_Produzione.Rows)
            {
                foreach (DataGridViewCell Cell in row.Cells)
                {
                    if (!Cell.ReadOnly)
                    {
                        if (!(Cell.Value == null))
                        {
                            if (!(string.IsNullOrWhiteSpace(Cell.Value.ToString())))
                            {
                                CelleCompilate++;
                            }
                        }
                    }
                }
            }
            if (DataGridView_Produzione.Rows[2].Cells[1].Value == null)
            {
                if (DialogResult.Yes == MessageBox.Show("Non hai assegnato una giacenza iniziale, vuoi renderla uguale alla scorta di sicurezza?", "Gestione materiali", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                {
                    DataGridView_Produzione.Rows[2].Cells[1].Value = DistintaBase.Albero.ScortaSicurezza;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (CelleCompilate < 2)
            {
                if (DialogResult.Yes == MessageBox.Show("Hai inserito poche informazioni, compilare la tabella.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Exclamation))
                {
                    DataGridView_Produzione.Rows[3].Cells[1].Value = DistintaBase.Albero.ScortaSicurezza;
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
        /// In base all'array che riceve in input cambia lo stile della tabella da inserimento dati a visualizzazione produzione.
        /// </summary>
        private void CambiaStileETitoliTabella(string[] Titoli)
        {
            DataGridView_Produzione.Rows.Add(5);
            DataGridView_Produzione.AutoSize = true;

            for (int i = 0; i < Titoli.Length; i++)
            {
                DataGridView_Produzione.Rows[i].Cells[0].Value = Titoli[i];
                DataGridView_Produzione.Rows[i].Cells[0].ReadOnly = true;
            }

            if (TitoliProduzione == Titoli)
            {
                NumericUpAndDown_Periodi.Enabled = false;
                label1.Enabled = false;
                for (int i = 0; i < Titoli.Length; i++)
                {
                    DataGridView_Produzione.Rows[i].ReadOnly = true;
                }
                for (int i = 0; i < DataGridView_Produzione.Columns.Count; i++)
                {
                    DataGridView_Produzione.Rows[4].Cells[i].Style.BackColor = Color.FromArgb(109, 125, 230);
                }
            }
            else
            {
                NumericUpAndDown_Periodi.Enabled = true;
                label1.Enabled = true;
                for (int i = 0; i < Titoli.Length; i++)
                {
                    if (i >= 2)
                    {
                        DataGridView_Produzione.Rows[i].ReadOnly = true;
                        DataGridView_Produzione.Rows[i].Cells[1].ReadOnly = false;
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
            DataGridView_Produzione.Columns[0].DefaultCellStyle.Font = new Font("Verdana", 9F, FontStyle.Regular);
            DataGridView_Produzione.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 9F, FontStyle.Regular);
            DataGridView_Produzione.BorderStyle = BorderStyle.None;

            DataGridView_Produzione.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            DataGridView_Produzione.BackgroundColor = Color.FromArgb(240, 240, 240);
            DataGridView_Produzione.EnableHeadersVisualStyles = false;

            DataGridView_Produzione.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DataGridView_Produzione.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(109, 125, 230);
            DataGridView_Produzione.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            DataGridView_Produzione.CurrentCell.Selected = false;
        }

        /// <summary>
        /// Carica gli input della tabella nelle rispettive variabili del componente del quale si sta calcolando la produzione.
        /// </summary>
        private void CaricaDatiTabellaInAlbero()
        {
            DistintaBase.ResettaProduzioneDistintaBaseDaForm(DistintaBase.Albero);
            DistintaBase.Albero.Produzione[0].Giacenza = Convert.ToInt32(DataGridView_Produzione.Rows[2].Cells[1].Value);
            DistintaBase.Albero.Produzione[0].Versamenti = Convert.ToInt32(DataGridView_Produzione.Rows[3].Cells[1].Value);
            DistintaBase.Albero.Produzione[0].OrdiniProduzione = Convert.ToInt32(DataGridView_Produzione.Rows[4].Cells[1].Value);

            for (int i = 1; i < DataGridView_Produzione.Columns.Count; i++)
            {
                DistintaBase.Albero.Produzione[i - 1].Previsioni = Convert.ToInt32(DataGridView_Produzione.Rows[0].Cells[i].Value);
                DistintaBase.Albero.Produzione[i - 1].OrdiniVendita = Convert.ToInt32(DataGridView_Produzione.Rows[1].Cells[i].Value);
            }
        }

        /// <summary>
        /// Carica nella tabella i dati della lista di periodi ricevuti in input (visuallizo la produzione di un componente).
        /// </summary>
        /// <param name="inputPeriodo">Lista di periodi contenente la programmazione della produzione del componente.</param>
        private void AggiornaTabella(List<Periodo> inputPeriodo)
        {
            for (int i = 1; i < DataGridView_Produzione.Columns.Count; i++)
            {
                DataGridView_Produzione.Rows[0].Cells[i].Value = inputPeriodo[i - 1].FabbisognoLordo;
                DataGridView_Produzione.Rows[1].Cells[i].Value = inputPeriodo[i - 1].Giacenza;
                DataGridView_Produzione.Rows[2].Cells[i].Value = inputPeriodo[i - 1].Versamenti;
                DataGridView_Produzione.Rows[3].Cells[i].Value = inputPeriodo[i - 1].OrdiniProduzione;
            }
        }

        /// <summary>
        /// Chiede all'utente di selezionare una distinta base e succesivamente la carica nel programma.
        /// </summary>       
        private TreeNode FormCaricaDistintaBase()
        {
            DistintaBase.NumPeriodi = NumPeriodi;
            Componente Comp = DistintaBase.Carica();
            if (Comp == null) return null;
            DistintaBase.Albero = Comp;

            return DistintaBase.NodeToTreeNode(DistintaBase.Albero);
        }

        /// <summary>
        /// Chiede all'utente di selezionare una produzione e succesivamente la carica nel programma.
        /// </summary>
        private TreeNode FormCaricaProduzione()
        {
            Componente Comp = DistintaBase.CaricaProduzione();
            if (Comp == null) return null;
            DistintaBase.Albero = Comp;
            if (DistintaBase.Albero.Produzione.Count() == 0)
            {
                MessageBox.Show("File non valido.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            NumPeriodi = DistintaBase.Albero.Produzione.Count() - 1;
            DistintaBase.NumPeriodi = NumPeriodi;

            if (NumPeriodi + 2 > DataGridView_Produzione.ColumnCount)
            {
                for (int i = DataGridView_Produzione.ColumnCount; i < NumPeriodi + 2; i++)
                {
                    DataGridViewColumn NewColumn = new DataGridViewColumn();
                    NewColumn.Width = 50;
                    NewColumn.HeaderText = (i - 1).ToString();
                    NewColumn.CellTemplate = periodo_0.CellTemplate;
                    DataGridView_Produzione.Columns.Add(NewColumn);
                    DataGridView_Produzione.Columns[DataGridView_Produzione.Columns.Count - 1].ReadOnly = true;
                    DataGridView_Produzione.Rows[0].Cells[DataGridView_Produzione.Columns.Count - 1].ReadOnly = false;
                    DataGridView_Produzione.Rows[1].Cells[DataGridView_Produzione.Columns.Count - 1].ReadOnly = false;
                }
            }
            else
            {
                while (NumPeriodi + 2 < DataGridView_Produzione.ColumnCount)
                {
                    DataGridView_Produzione.Columns.RemoveAt(DataGridView_Produzione.ColumnCount - 1);
                }
            }
            NumericUpAndDown_Periodi.Value = NumPeriodi;

            if (DistintaBase.Albero == null)
            {
                return null;
            }


            return DistintaBase.NodeToTreeNode(DistintaBase.Albero);
        }

        /// <summary>
        /// Pulisce tutte le celle della dataGridView
        /// </summary>
        private void PulisciTabella()
        {
            foreach (DataGridViewRow Row in DataGridView_Produzione.Rows)
            {
                foreach (DataGridViewCell Cell in Row.Cells)
                {
                    Cell.Value = null;
                }
            }
            TabellaGenerata = false;
        }

        /// <summary>
        /// Controlla se la cella selezionata è valida (ha solo numeri).
        /// </summary>
        private void ValidaCellaCorrente()
        {
            if (DataGridView_Produzione.CurrentCell.Value != null)
            {
                bool ris = DataGridView_Produzione.CurrentCell.Value.ToString().All(char.IsDigit);

                if (!ris)
                {
                    MessageBox.Show("Inserisci un numero intero e positivo.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataGridView_Produzione.CurrentCell.Value = null;
                }

            }
        }


        private string InfoComponenteDistintabase()
        {
            TreeNode TreePadre = null;
            if (TreeView_DistintaBase.SelectedNode.Parent != null)
                TreePadre = TreeView_DistintaBase.SelectedNode.Parent;
            Componente Componente = DistintaBase.TreeNodeToNode(TreeView_DistintaBase.SelectedNode, TreePadre);
            if (Componente == null) return "selezionare un componente";
            return "NOME --> " + Componente.Nome + "\nCODICE --> " + Componente.Codice + "\nDESCRIZIONE --> " + Componente.Descrizione + "\nLEAD TIME --> " + Componente.LeadTime + "\nLEAD TIME SICUREZZA --> " + Componente.LeadTimeSicurezza + "\nLOTTO --> " + Componente.Lotto + "\nSCORTA DI SICUREZZA --> " + Componente.ScortaSicurezza + "\nPERIODO DI COPERTURA --> " + Componente.PeriodoDiCopertura;
        }

    }
}
