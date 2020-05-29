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
            CalcolaProduzioneRadice(distintaBase.Albero, 0, FabbisognoComp);
            foreach(Componente sottoNodo in distintaBase.Albero.SottoNodi)
            {
                CalcolaProduzioneSottonodi(distintaBase.Albero, sottoNodo, distintaBase.Albero.LeadTime + distintaBase.Albero.LeadTimeSicurezza);
            }
        }

        public void CalcolaProduzioneRadice(Componente comp, int LeadTimeNodiSoprastanti, List<int> FabbisognoComp)
        {
            int TempoProduzioneTotale = LeadTimeNodiSoprastanti + comp.LeadTime + comp.LeadTimeSicurezza;
            List<int> fabbisognoComp = new List<int>();
            for (int i=0; i<FabbisognoComp.Count; i++)
            {
                fabbisognoComp.Add(FabbisognoComp[i] * comp.CoefficenteUtilizzo);
            }
            avviaProduzioneComponente(comp, TempoProduzioneTotale, fabbisognoComp);
        }

        public void CalcolaProduzioneSottonodi(Componente padre, Componente comp, int LeadTimeNodiSoprastanti)
        {
            int TempoProduzioneTotale = LeadTimeNodiSoprastanti + comp.LeadTime + comp.LeadTimeSicurezza;
            int[] FabbisognoComp = new int[comp.Produzione.Count];
            for (int i = 0; i < comp.Produzione.Count - LeadTimeNodiSoprastanti-1; i++)
            {
                FabbisognoComp[i] = padre.Produzione[i + LeadTimeNodiSoprastanti-1].Versamenti;
            }
            List<int> fabbisognoSottocomp = new List<int>();
            for (int i = 0; i < FabbisognoComp.Count(); i++)
            {
                fabbisognoSottocomp.Add(FabbisognoComp[i]);
            }
            avviaProduzioneComponente(comp, TempoProduzioneTotale, fabbisognoSottocomp);
            foreach (Componente sottoComp in comp.SottoNodi)
            {
                CalcolaProduzioneSottonodi(comp,sottoComp,TempoProduzioneTotale);
            }
        }

        public void avviaProduzioneComponente(Componente comp, int TempoProduzioneTotale, List<int> FabbisognoComp)//tutti i periodi
        {
            for (int i = 1; i < NumPeriodi+1; i++) //devo fare anche il periodo 0 ?????!!!!
            {
                calcolaPeriodoComponente(comp, TempoProduzioneTotale, i, FabbisognoComp[i]);
            }
        }

        public void calcolaPeriodoComponente(Componente comp, int TempoProduzioneTotale, int periodoAdesso, int FabbisognoCompPeriodoAttuale)//di 1 periodo
        {
            comp.Produzione[periodoAdesso].FabbisognoLordo = FabbisognoCompPeriodoAttuale + comp.ScortaSicurezza;
            int fabbisognoNetto = comp.Produzione[periodoAdesso].FabbisognoLordo - comp.Produzione[periodoAdesso - 1].Giacenza;
            comp.Produzione[periodoAdesso].Giacenza = comp.Produzione[periodoAdesso - 1].Giacenza - FabbisognoCompPeriodoAttuale;

            comp.Produzione[periodoAdesso].Versamenti = 0;

            if (fabbisognoNetto < 0) fabbisognoNetto = 0;

            while (comp.Produzione[periodoAdesso].Versamenti < fabbisognoNetto)
            {
                comp.Produzione[periodoAdesso].Versamenti += comp.Lotto;
            }
            
            if (comp.Produzione[periodoAdesso].Versamenti == 0) return;//non devo produrre

            if (periodoAdesso - TempoProduzioneTotale + 1 >= 0)
            {
                comp.Produzione[periodoAdesso].Giacenza = (comp.Produzione[periodoAdesso].Versamenti + comp.Produzione[periodoAdesso - 1].Giacenza) - FabbisognoCompPeriodoAttuale;

                comp.Produzione[periodoAdesso - TempoProduzioneTotale + 1].OrdiniProduzione = comp.Produzione[periodoAdesso].Versamenti;
            }
            else
            {
                comp.Produzione[periodoAdesso].Giacenza = (comp.Produzione[periodoAdesso].Versamenti + comp.Produzione[periodoAdesso - 1].Giacenza) - FabbisognoCompPeriodoAttuale;

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
