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

        public Componente CaricaDistintaBase()
        {
            Componente componente = new Componente();
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
                    }
                    catch
                    {
                        MessageBox.Show("File non valido.", "Gestione materiali", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    stream.Close();
                }
            }
            Albero = componente;
            AggiornaNodi(Albero);
            return componente;
        }
    }
}
