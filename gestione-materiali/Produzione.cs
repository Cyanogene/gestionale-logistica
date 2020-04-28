using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace gestione_materiali
{
    class Produzione
    {
        // Tasto destro su nodo treeView:
        // Mostra produzione componente (singola)
        // Mostra produzione componente (unisce tutte le tabelle di un componente in una unica produzione)
        // Informazioni (mostra info Componente)

        public DistintaBase distintaBase;
        private List<int> maxTraPrevEOrdini;

        public Produzione(DistintaBase p, List<int> x)
        {
            distintaBase = p;
            maxTraPrevEOrdini = x;
        }

        public void avviaProduzione()
        {
            calcolaProduzioneCompESottonodi(distintaBase.Albero, 0);
        }

        public void calcolaProduzioneCompESottonodi(Componente comp, int LeadTimeNodiSoprastanti)
        {
            int TempoProduzioneTotale = LeadTimeNodiSoprastanti + comp.LeadTime + comp.LeadTimeSicurezza;
            avviaProduzioneComponente(comp, TempoProduzioneTotale);

            foreach (Componente sottoComp in comp.SottoNodi)
            {
                calcolaProduzioneCompESottonodi(sottoComp, TempoProduzioneTotale);
            }
        }

        public void avviaProduzioneComponente(Componente comp, int TempoProduzioneTotale)//tutti i periodi
        {
            for (int i = 1; i < 7; i++)
            {
                calcolaPeriodoComponente(comp, TempoProduzioneTotale, i);
            }
        }

        public void calcolaPeriodoComponente(Componente comp, int TempoProduzioneTotale, int periodoAdesso)//di 1 periodo
        {
            comp.Produzione[periodoAdesso].Giacenza = comp.Produzione[periodoAdesso - 1].Giacenza - maxTraPrevEOrdini[periodoAdesso];

            int giacenzainiziale = comp.Produzione[periodoAdesso].Giacenza;
            int giacenzaFinale = giacenzainiziale;

            while (giacenzaFinale < comp.ScortaSicurezza)
            {
                giacenzaFinale += comp.Lotto;
            }

            comp.Produzione[periodoAdesso].Versamenti = giacenzaFinale - giacenzainiziale;
            if (comp.Produzione[periodoAdesso].Versamenti == 0) return;//non devo produrre

            if (periodoAdesso - TempoProduzioneTotale + 1 >= 1)
            {
                comp.Produzione[periodoAdesso].Giacenza = giacenzaFinale;
                comp.Produzione[periodoAdesso - TempoProduzioneTotale + 1].OrdiniProduzione = comp.Produzione[periodoAdesso].Versamenti;
            }
            else
            {
                //error ----> non si puo fare, non basta il tempo
            }
        }
    }
}
