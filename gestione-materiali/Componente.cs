using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestione_materiali
{
    [Serializable]
    public class Componente
    {
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public string Codice { get; set; }
        public int LeadTime { get; set; }
        public int LeadTimeSicurezza { get; set; }
        public int ScortaSicurezza { get; set; }
        public int Lotto { get; set; }
        public int PeriodoDiCopertura { get; set; }
        public int CoefficenteUtilizzo { get; set; }
        public List<Componente> SottoNodi = new List<Componente>();
        public List<Periodo> Produzione = new List<Periodo>();
    }
}