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
        public DistintaBase DistintaBase;
        public int NumPeriodi = 0;
        public int PeriodiNegativi;
        

        public Produzione(DistintaBase Albero, int NumPeriodi)
        {
            DistintaBase = Albero;
            this.NumPeriodi = NumPeriodi;
        }

        /// <summary>
        /// Metodo chiamato dal form all'avvio della produzione, ottiene dalla tabella il fabbisogno lordo del componente "base".
        /// </summary>
        public void AvviaProduzione()
        {
            List<int> FabbisognoComp = new List<int>();
            foreach(Periodo Periodo in DistintaBase.Albero.Produzione)
            {
                FabbisognoComp.Add(Math.Max(Periodo.OrdiniVendita, Periodo.Previsioni));
            }
            CalcolaProduzioneRadice(DistintaBase.Albero, FabbisognoComp);
            foreach(Componente SottoComponente in DistintaBase.Albero.SottoNodi)
            {
                CalcolaProduzioneSottonodi(DistintaBase.Albero, SottoComponente);
            }
        }

        /// <summary>
        /// Calcola la produzione del componente in input (deve essere la radice della distintaBase).
        /// </summary>
        public void CalcolaProduzioneRadice(Componente Comp, List<int> FabbisognoComp)
        {
            for (int i=0; i<FabbisognoComp.Count; i++)
            {
                Comp.Produzione[i].FabbisognoLordo = FabbisognoComp[i] * Comp.CoefficenteUtilizzo;
            }
            AvviaProduzioneComponente(Comp);
        }

        /// <summary>
        /// Calcola la produzione del componente in input (deve essere un sottocomp della distintaBase).
        /// </summary>
        public void CalcolaProduzioneSottonodi(Componente Padre, Componente Comp)
        {
            int TempoProduzioneComp = Comp.LeadTime + Comp.LeadTimeSicurezza;
            int[] FabbisognoComp = new int[Comp.Produzione.Count];
            for (int i = 0; i < Comp.Produzione.Count - TempoProduzioneComp+1; i++)
            {
                FabbisognoComp[i] = Padre.Produzione[i + TempoProduzioneComp-1].Versamenti;
                Comp.Produzione[i].FabbisognoLordo = FabbisognoComp[i] * Comp.CoefficenteUtilizzo;
            }
            AvviaProduzioneComponente(Comp);
            if(Comp.PeriodoDiCopertura>1)
            {
                for(int i=0; i<Comp.Produzione.Count-Comp.PeriodoDiCopertura+1; i++)
                {
                    if(Comp.Produzione[i].Versamenti!=0)
                    {
                        ImplementazionePeriodoCompertura(Comp, i);
                    }
                }
            }
            foreach (Componente sottoComp in Comp.SottoNodi)
            {
                CalcolaProduzioneSottonodi(Comp,sottoComp);
            }
        }

        /// <summary>
        /// Per ogni periodo chiama il metodo calcolaPeriodoComponente (calcoli svolti sul componente in input).
        /// </summary>
        /// <param name="Comp"></param>
        public void AvviaProduzioneComponente(Componente Comp)//tutti i periodi
        {
            int TempoProdizioneComp = Comp.LeadTime + Comp.LeadTimeSicurezza;
            for (int i = 1; i < NumPeriodi+1; i++) //devo fare anche il periodo 0 ?????!!!!
            {
                calcolaPeriodoComponente(Comp, TempoProdizioneComp, i);
            }
        }

        /// <summary>
        /// Svolge i vari calcoli sul componente dato in input al periodo dato in input (periodoAdesso)
        /// </summary>
        public void calcolaPeriodoComponente(Componente Comp, int TempoProduzioneTotale, int PeriodoAttuale)//di 1 periodo
        {
            int FabbisognoNetto = (Comp.Produzione[PeriodoAttuale].FabbisognoLordo+Comp.ScortaSicurezza) - Comp.Produzione[PeriodoAttuale - 1].Giacenza;
            Comp.Produzione[PeriodoAttuale].Giacenza = Comp.Produzione[PeriodoAttuale - 1].Giacenza - Comp.Produzione[PeriodoAttuale].FabbisognoLordo;
            

            if (FabbisognoNetto < 0) FabbisognoNetto = 0;

            while (Comp.Produzione[PeriodoAttuale-1].Versamenti < FabbisognoNetto)
            {
                Comp.Produzione[PeriodoAttuale-1].Versamenti += Comp.Lotto;
            }
            
            if (Comp.Produzione[PeriodoAttuale-1].Versamenti == 0) return;//non devo produrre

            if (PeriodoAttuale - TempoProduzioneTotale >= 0)
            {
                Comp.Produzione[PeriodoAttuale].Giacenza = (Comp.Produzione[PeriodoAttuale-1].Versamenti + Comp.Produzione[PeriodoAttuale - 1].Giacenza) - Comp.Produzione[PeriodoAttuale].FabbisognoLordo;

                Comp.Produzione[PeriodoAttuale - TempoProduzioneTotale].OrdiniProduzione = Comp.Produzione[PeriodoAttuale-1].Versamenti;
            }
            else
            {
                Comp.Produzione[PeriodoAttuale].Giacenza = (Comp.Produzione[PeriodoAttuale-1].Versamenti + Comp.Produzione[PeriodoAttuale - 1].Giacenza) - Comp.Produzione[PeriodoAttuale].FabbisognoLordo; 

                int PeriodiNecessariInPiu = Math.Abs(PeriodoAttuale - TempoProduzioneTotale + 1);
                if (PeriodiNecessariInPiu > PeriodiNegativi)
                {
                    PeriodiNegativi = PeriodiNecessariInPiu;
                }
            }
            //comp.Produzione[periodoAdesso].FabbisognoLordo = giacenzaFinale;
        }

        /// <summary>
        /// Calcola e svolge i vari calcoli per "accorpare" i fabbisogni di più periodi di un componente se questo ha PeriodoDiCopertura>1 .
        /// </summary>
        public void ImplementazionePeriodoCompertura(Componente Comp,int PeriodoInizio)
        {
            for(int i=1; i<Comp.PeriodoDiCopertura; i++)
            {
                Comp.Produzione[PeriodoInizio].Giacenza += Comp.Produzione[PeriodoInizio + i].Giacenza;
                Comp.Produzione[PeriodoInizio].Versamenti += Comp.Produzione[PeriodoInizio + i].Versamenti;
                Comp.Produzione[PeriodoInizio + i].Versamenti = 0;
                if(PeriodoInizio-(Comp.LeadTime + Comp.LeadTimeSicurezza) + 1 > 0)
                {
                    Comp.Produzione[PeriodoInizio - (Comp.LeadTime + Comp.LeadTimeSicurezza) + 1].OrdiniProduzione += Comp.Produzione[PeriodoInizio - (Comp.LeadTime + Comp.LeadTimeSicurezza) + 1 + i].OrdiniProduzione;
                    Comp.Produzione[PeriodoInizio - (Comp.LeadTime + Comp.LeadTimeSicurezza) + 1 + i].OrdiniProduzione = 0;
                }
            }
        }
    }
}
