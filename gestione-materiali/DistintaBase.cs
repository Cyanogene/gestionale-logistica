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

        public Componente TreeViewToNode(TreeView TreeView)
        {
            Componente Node = TreeNodeToNode(TreeView.Nodes[0]);
            return Node;
        }

        public Componente TreeNodeToNode(TreeNode TreeNode)
        {
            Componente Componente = new Componente
            {
                SottoNodi = new List<Componente>()
            };
            foreach (Componente Nodo in Nodi)
            {
                if (Nodo.Code == TreeNode.Tag.ToString())
                {
                    Componente = Nodo;
                }
            }
            Componente.SottoNodi.Clear();
            // Crea un nodo per ogni figlio 
            foreach (TreeNode tn in TreeNode.Nodes)
            {
                Componente.SottoNodi.Add(TreeNodeToNode(tn));
            }
            return Componente;
        }

        public TreeNode NodeToTreeNode(Componente Node)
        {
            TreeNode Nodo = new TreeNode(Node.Nome);
            Nodo.Tag = Node.Code;
            if (Node.SottoNodi != null)
            {
                foreach (Componente node in Node.SottoNodi)
                {
                    Nodo.Nodes.Add(NodeToTreeNode(node));
                }
            }
            return Nodo;
        }

        public void AggiungiANodiDistintaBase(Componente comp)
        {
            foreach (Componente sottoComp in comp.SottoNodi)
            {
                AggiungiANodiDistintaBase(sottoComp);
            }

            if (!Nodi.Contains(comp))
            {
                Nodi.Add(comp);
            }
        }
    }
}
