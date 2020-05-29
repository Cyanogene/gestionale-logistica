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
        public DistintaBase distintaBase;
        public int NumPeriodi = 0;
        public int PeriodiNegativi;
        

        public Produzione(DistintaBase albero, int numPeriodi)
        {
            distintaBase = albero;
            NumPeriodi = numPeriodi;
        }

        public void avviaProduzione()
        {
            List<int> FabbisognoComp = new List<int>();
            foreach(Periodo periodo in distintaBase.Albero.Produzione)
            {
                FabbisognoComp.Add(Math.Max(periodo.OrdiniVendita, periodo.Previsioni));
            }
            calcolaProduzioneCompESottonodi(distintaBase.Albero, 0, FabbisognoComp);
        }

        public void calcolaProduzioneCompESottonodi(Componente comp, int LeadTimeNodiSoprastanti, List<int> FabbisognoComp)
        {
            int TempoProduzioneTotale = LeadTimeNodiSoprastanti + comp.LeadTime + comp.LeadTimeSicurezza;
            List<int> fabbisognoComp = new List<int>();
            for (int i=0; i<FabbisognoComp.Count; i++)
            {
                fabbisognoComp.Add(FabbisognoComp[i] * comp.CoefficenteUtilizzo);
            }
            avviaProduzioneComponente(comp, TempoProduzioneTotale, fabbisognoComp);

            foreach (Componente sottoComp in comp.SottoNodi)
            {
                calcolaProduzioneCompESottonodi(sottoComp, TempoProduzioneTotale, fabbisognoComp);
            }
        }

        public void avviaProduzioneComponente(Componente comp, int TempoProduzioneTotale, List<int> FabbisognoComp)//tutti i periodi
        {
            for (int i = 1; i < NumPeriodi+1; i++) //devo fare anche il periodo 0 ?????!!!!
            {
                comp.Produzione[i].FabbisognoLordo = FabbisognoComp[i];
                calcolaPeriodoComponente(comp, TempoProduzioneTotale, i);
            }
        }

        public void calcolaPeriodoComponente(Componente comp, int TempoProduzioneTotale, int periodoAdesso)//di 1 periodo
        {
            comp.Produzione[periodoAdesso].FabbisognoLordo += comp.ScortaSicurezza - comp.Produzione[periodoAdesso - 1].Giacenza;
            comp.Produzione[periodoAdesso].Giacenza = comp.Produzione[periodoAdesso - 1].Giacenza;

            int giacenzainiziale = comp.Produzione[periodoAdesso].Giacenza;
            int giacenzaFinale = giacenzainiziale;

            while (giacenzaFinale < comp.Produzione[periodoAdesso].FabbisognoLordo)
            {
                giacenzaFinale += comp.Lotto;
            }

            comp.Produzione[periodoAdesso].Versamenti = giacenzaFinale - giacenzainiziale;
            if (comp.Produzione[periodoAdesso].Versamenti == 0) return;//non devo produrre

            if (periodoAdesso - TempoProduzioneTotale + 1 >= 0)
            {
                comp.Produzione[periodoAdesso].Giacenza = giacenzaFinale;

                comp.Produzione[periodoAdesso - TempoProduzioneTotale + 1].OrdiniProduzione = comp.Produzione[periodoAdesso].Versamenti;
            }
            else
            {
                comp.Produzione[periodoAdesso].Giacenza = giacenzaFinale;

                int periodiNecessari = Math.Abs(periodoAdesso - TempoProduzioneTotale + 1);
                if (periodiNecessari > PeriodiNegativi)
                {
                    PeriodiNegativi = periodiNecessari;
                }
            }
            //comp.Produzione[periodoAdesso].FabbisognoLordo = giacenzaFinale;
        }
    }
}
