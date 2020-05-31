using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace gestione_materiali
{
    class DistintaBase
    {
        public List<Componente> Nodi = new List<Componente>();  
        public Componente Albero = new Componente();    // contiene tutto
        public int NumPeriodi;

        /// <summary>
        /// Trasforma un TreeNode (WinForms.TreeView) in un Nodo (custom).
        /// </summary>
        /// <param name="TreeNode">Il TreeNode da trasformare.</param>
        /// <returns></returns>
        public Componente TreeNodeToNode(TreeNode TreeNode, TreeNode PadreTreeNode)
        {
            Componente Componente = new Componente();
            Componente Padre = new Componente();
            Nodi.Clear();
            AggiornaNodi(Albero);
            if(PadreTreeNode==null)
            {
                return Albero;
            }
            foreach (Componente Nodo in Nodi)
            {
                if (Nodo.Codice == PadreTreeNode.Tag.ToString() && Nodo.SottoNodi.Count == PadreTreeNode.Nodes.Count)
                {
                    Padre = Nodo;
                }
            }
            foreach(Componente sottoNodo in Padre.SottoNodi)
            {
                if (sottoNodo.Codice == TreeNode.Tag.ToString() && sottoNodo.SottoNodi.Count == TreeNode.Nodes.Count)
                {
                    Componente = sottoNodo;
                }
            }
            return Componente;
        }
        
        /// <summary>
        /// Trasforma un Nodo (custom) in un TreeNode (WinForms.TreeView).
        /// </summary>
        /// <param name="Node">Il nodo da trasformare.</param>
        /// <returns></returns>
        public TreeNode NodeToTreeNode(Componente Node)
        {
            string Nome = Node.Nome;
            if (!(Albero == Node) && Node.CoefficenteUtilizzo > 1)
            {
                Nome = Node.CoefficenteUtilizzo + "× " + Nome;
            }

            TreeNode TreeNode = new TreeNode(Nome)
            {
                Tag = Node.Codice
            };
            if (Node.SottoNodi != null && Node.SottoNodi.Count > 0)
            {
                foreach (Componente node in Node.SottoNodi)
                {
                    TreeNode.Nodes.Add(NodeToTreeNode(node));
                }
            }
            return TreeNode;
        }

        /// <summary>
        /// Aggiorna la variabile lista nodi con tutti i nodi presenti attualmente nella variabile albero.
        /// </summary>
        public void AggiornaNodi(Componente Comp)
        {
            if (Comp.SottoNodi != null)
            {
                foreach (Componente sottoComp in Comp.SottoNodi)
                {
                    AggiornaNodi(sottoComp);
                }
            }
            Nodi.Add(Comp);
        }

        /// <summary>
        /// Resetta la produzione del componente in input (variabile di tipo distintaBase).
        /// </summary>
        public void ResettaProduzioneDistintaBase(Componente Comp)
        {
            Comp.Produzione = new List<Periodo>();
            for (int i=0; i< NumPeriodi+1; i++)
            {
                Comp.Produzione.Add(new Periodo());
            }
            foreach (Componente sottoComp in Comp.SottoNodi)
            {
                ResettaProduzioneDistintaBase(sottoComp);
            }
        }

        /// <summary>
        /// Resetta la produzione del componente in input (variabile di tipo produzione).
        /// </summary>
        public void ResettaProduzioneDistintaBaseDaForm(Componente Comp)
        {
            Periodo primoPeriodo = Comp.Produzione[0];
            Comp.Produzione = new List<Periodo>();
            Comp.Produzione.Add(primoPeriodo);
            for (int i = 1; i < NumPeriodi+1; i++)
            {
                Comp.Produzione.Add(new Periodo());
            }
            foreach (Componente SottoComp in Comp.SottoNodi)
            {
                ResettaProduzioneDistintaBaseDaForm(SottoComp);
            }
        }

        /// <summary>
        /// Salva l'albero(all'interno dei componenti sono salvate le variabili dell'ultima produzione calcolata).
        /// </summary>
        public void SalvaProduzione()
        {
            SaveFileDialog Sfd_Catalogo = new SaveFileDialog();
            Sfd_Catalogo.InitialDirectory = @"C:\";
            Sfd_Catalogo.RestoreDirectory = true;
            Sfd_Catalogo.FileName = $"{Albero.Nome}_Produzione.xml";
            Sfd_Catalogo.DefaultExt = "xml";
            Sfd_Catalogo.Filter = "xml files (*.xml)|*.xml";

            if (Sfd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                Stream FilesStream = Sfd_Catalogo.OpenFile();
                StreamWriter Sw = new StreamWriter(FilesStream);
                XmlSerializer Serializer = new XmlSerializer(typeof(Componente));
                Serializer.Serialize(Sw, Albero);
                Sw.Close();
                FilesStream.Close();
            }
        }

        /// <summary>
        /// Carica un albero.
        /// </summary>
        public Componente Carica()
        {
            Componente Componente = null;
            OpenFileDialog Ofd_Catalogo = new OpenFileDialog();
            Ofd_Catalogo.InitialDirectory = @"C:\";
            Ofd_Catalogo.Filter = "XML|*.xml";

            if (Ofd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Ofd_Catalogo.FileName))
                {
                    StreamReader Stream = new StreamReader(Ofd_Catalogo.FileName);
                    XmlSerializer Serializer = new XmlSerializer(typeof(Componente));
                    try
                    {
                        Componente = (Componente)Serializer.Deserialize(Stream);
                        ResettaProduzioneDistintaBase(Componente);
                        Nodi.Clear();
                        AggiornaNodi(Componente);
                    }
                    catch
                    {
                        MessageBox.Show("File non valido.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Stream.Close();
                }
            }
            Albero = Componente;
            return Componente;
        }

        /// <summary>
        /// Carica una produzione.
        /// </summary>
        public Componente CaricaProduzione()
        {
            Componente Componente = null;
            OpenFileDialog Ofd_Catalogo = new OpenFileDialog();
            Ofd_Catalogo.InitialDirectory = @"C:\";
            Ofd_Catalogo.Filter = "XML|*.xml";

            if (Ofd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Ofd_Catalogo.FileName))
                {
                    StreamReader Stream = new StreamReader(Ofd_Catalogo.FileName);
                    XmlSerializer Serializer = new XmlSerializer(typeof(Componente));
                    try
                    {
                        Componente = (Componente)Serializer.Deserialize(Stream);
                        Nodi.Clear();
                        AggiornaNodi(Componente);
                    }
                    catch
                    {
                        MessageBox.Show("File non valido.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Stream.Close();
                }
            }
            Albero = Componente;
            return Componente;
        }

        
        
    }
}
