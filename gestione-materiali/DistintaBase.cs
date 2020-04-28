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

        public Componente TreeNodeToNode(TreeNode TreeNode)    // input: selected TreeNode _ output: selected Componente
        {
            Componente Componente = new Componente();
            Nodi.Clear();
            AggiornaNodi(Albero);
            foreach (Componente Nodo in Nodi)
            {
                if (Nodo.Codice == TreeNode.Tag.ToString() && Nodo.SottoNodi.Count == TreeNode.Nodes.Count)
                {
                    Componente = Nodo;
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
            TreeNode treeNode = new TreeNode(Node.Nome);
            treeNode.Tag = Node.Codice;
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
            comp.Produzione = new List<Periodo>() { new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo() };
            foreach (Componente component in comp.SottoNodi)
            {
                component.Produzione = new List<Periodo>() { new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo(), new Periodo() };
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

        public Componente Carica()
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
                        componente.Produzione.RemoveRange(0, 7);
                        FixDistintaBase(componente);
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

        public void FixDistintaBase(Componente comp)
        {
            foreach (Componente componente in comp.SottoNodi)
            {
                componente.Produzione.RemoveRange(0, 7);
                FixDistintaBase(componente);
            }
        }
    }
}
