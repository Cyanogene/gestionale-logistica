using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace gestione_materiali
{
    class Salvataggio
    {
        public void SalvaProgrammazione(List<Periodo> periodi, StreamWriter sw)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Periodo>));
            serializer.Serialize(sw, periodi);
            sw.Close();
        }

        /// Per Giacomo: mettere caricamento dati da altro programma    |
        ///                                                             |
        ///                                                             v

        //public List<Node> CaricaCatalogo(string filePosition)
        //{
        //    List<Node> risultato = new List<Node>();
        //    if (File.Exists(filePosition))
        //    {
        //        StreamReader stream = new StreamReader(filePosition);
        //        XmlSerializer serializer = new XmlSerializer(typeof(List<Node>));
        //        risultato = (List<Node>)serializer.Deserialize(stream);
        //        stream.Close();
        //    }
        //    return risultato;
        //}
    }
}
