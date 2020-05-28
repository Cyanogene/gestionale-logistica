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

        public Componente TreeNodeToNode(TreeNode TreeNode, TreeNode PadreTreeNode)    // input: selected TreeNode _ output: selected Componente
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
        

        public void AggiornaNodi(Componente comp)
        {
            if (comp.SottoNodi != null)
            {
                foreach (Componente sottoComp in comp.SottoNodi)
                {
                    AggiornaNodi(sottoComp);
                }
            }
            Nodi.Add(comp);
        }

        public TreeNode NodeToTreeNode(Componente Node)
        {
            string Nome = Node.Nome;
            if (!(Albero == Node) && Node.CoefficenteUtilizzo > 1)
            {
                Nome = Node.CoefficenteUtilizzo + "× " + Nome;
            }

            TreeNode treeNode = new TreeNode(Nome)
            {
                Tag = Node.Codice
            };
            if (Node.SottoNodi != null && Node.SottoNodi.Count > 0)
            {
                foreach (Componente node in Node.SottoNodi)
                {
                    treeNode.Nodes.Add(NodeToTreeNode(node));
                }
            }
            return treeNode;
        }

        public void ResettaProduzioneDistintaBase(Componente comp)
        {
            comp.Produzione = new List<Periodo>();
            for (int i=0; i< NumPeriodi+1; i++)
            {
                comp.Produzione.Add(new Periodo());
            }
            foreach (Componente sottoComp in comp.SottoNodi)
            {
                ResettaProduzioneDistintaBase(sottoComp);
            }
        }

        public void ResettaProduzioneDistintaBaseDaForm(Componente comp)
        {
            Periodo primoPeriodo = comp.Produzione[0];
            comp.Produzione = new List<Periodo>();
            comp.Produzione.Add(primoPeriodo);
            for (int i = 1; i < NumPeriodi+1; i++)
            {
                comp.Produzione.Add(new Periodo());
            }
            foreach (Componente sottoComp in comp.SottoNodi)
            {
                ResettaProduzioneDistintaBaseDaForm(sottoComp);
            }
        }

        public void Salva()
        {
            SaveFileDialog Sfd_Catalogo = new SaveFileDialog();
            Sfd_Catalogo.InitialDirectory = @"C:\";
            Sfd_Catalogo.RestoreDirectory = true;
            Sfd_Catalogo.FileName = $"{Albero.Nome}_Produzione.xml";
            Sfd_Catalogo.DefaultExt = "xml";
            Sfd_Catalogo.Filter = "xml files (*.xml)|*.xml";

            if (Sfd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                Stream filesStream = Sfd_Catalogo.OpenFile();
                StreamWriter sw = new StreamWriter(filesStream);
                XmlSerializer serializer = new XmlSerializer(typeof(Componente));
                serializer.Serialize(sw, Albero);
                sw.Close();
                filesStream.Close();
            }
        }

        public Componente Carica(int NumPeriodi)
        {
            Componente componente = null;
            OpenFileDialog Ofd_Catalogo = new OpenFileDialog();
            Ofd_Catalogo.InitialDirectory = @"C:\";
            Ofd_Catalogo.Filter = "XML|*.xml";

            if (Ofd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Ofd_Catalogo.FileName))
                {
                    StreamReader stream = new StreamReader(Ofd_Catalogo.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(Componente));
                    try
                    {
                        componente = (Componente)serializer.Deserialize(stream);
                        ResettaProduzioneDistintaBase(componente);
                        Nodi.Clear();
                        AggiornaNodi(componente);
                    }
                    catch
                    {
                        MessageBox.Show("File non valido.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    stream.Close();
                }
            }
            Albero = componente;
            return componente;
        }
        
    }
}
