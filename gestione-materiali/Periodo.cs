using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestione_materiali
{
    [Serializable]
    class Periodo
    {
        public int Previsioni { get; set; }
        public int OrdiniVendita { get; set; }
        public int Giacenza { get; set; }
        public int Versamenti { get; set; }
        public int OrdiniProduzione { get; set; }
    }
}
