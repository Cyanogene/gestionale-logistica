using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace gestione_materiali
{
    class Programmazione
    {
        public void Salva(List<Periodo> p)
        {
            SaveFileDialog Sfd_Catalogo = new SaveFileDialog();
            Sfd_Catalogo.InitialDirectory = @"C:\";
            Sfd_Catalogo.RestoreDirectory = true;
            Sfd_Catalogo.FileName = "*.xml";
            Sfd_Catalogo.DefaultExt = "xml";
            Sfd_Catalogo.Filter = "xml files (*.xml)|*.xml";

            if (Sfd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                Stream filesStream = Sfd_Catalogo.OpenFile();
                StreamWriter sw = new StreamWriter(filesStream);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Periodo>));
                serializer.Serialize(sw, p);
                filesStream.Close();
                sw.Close();
            }
        }

        public Componente Carica(Componente componente)
        {
            OpenFileDialog Ofd_Catalogo = new OpenFileDialog();
            Ofd_Catalogo.InitialDirectory = @"C:\";
            Ofd_Catalogo.Filter = "XML|*.xml";

            if (Ofd_Catalogo.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(Ofd_Catalogo.FileName))
                {
                    StreamReader stream = new StreamReader(Ofd_Catalogo.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(Componente));
                    componente = (Componente)serializer.Deserialize(stream);
                    stream.Close();
                }
            }
            return componente;
        }
    }
}
