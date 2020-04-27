using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestione_materiali
{
    class Produzione
    {
        public DistintaBase distintaBase = new DistintaBase();
        private List<int> maxTraPrevEOrdini;

        public Produzione(Periodo p, List<int> x)
        {
            distintaBase.Albero.Produzione[0] = p;
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
            calcolaProduzioneCompESottonodi(comp, comp.LeadTime);
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

            while (giacenzainiziale < comp.ScortaSicurezza)
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
