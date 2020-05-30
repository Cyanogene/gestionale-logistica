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

        /// <summary>
        /// Metodo chiamato dal form all'avvio della produzione, ottiene dalla tabella il fabbisogno lordo del componente "base".
        /// </summary>
        public void avviaProduzione()
        {
            List<int> FabbisognoComp = new List<int>();
            foreach(Periodo periodo in distintaBase.Albero.Produzione)
            {
                FabbisognoComp.Add(Math.Max(periodo.OrdiniVendita, periodo.Previsioni));
            }
            CalcolaProduzioneRadice(distintaBase.Albero, FabbisognoComp);
            foreach(Componente sottoNodo in distintaBase.Albero.SottoNodi)
            {
                CalcolaProduzioneSottonodi(distintaBase.Albero, sottoNodo);
            }
        }

        /// <summary>
        /// Calcola la produzione del componente in input (deve essere la radice della distintaBase).
        /// </summary>
        public void CalcolaProduzioneRadice(Componente comp, List<int> FabbisognoComp)
        {
            for (int i=0; i<FabbisognoComp.Count; i++)
            {
                comp.Produzione[i].FabbisognoLordo = FabbisognoComp[i] * comp.CoefficenteUtilizzo;
            }
            avviaProduzioneComponente(comp);
        }

        /// <summary>
        /// Calcola la produzione del componente in input (deve essere un sottocomp della distintaBase).
        /// </summary>
        public void CalcolaProduzioneSottonodi(Componente padre, Componente comp)
        {
            int TempoProduzioneComp = comp.LeadTime + comp.LeadTimeSicurezza;
            int[] FabbisognoComp = new int[comp.Produzione.Count];
            for (int i = 0; i < comp.Produzione.Count - TempoProduzioneComp+1; i++)
            {
                FabbisognoComp[i] = padre.Produzione[i + TempoProduzioneComp-1].Versamenti;
                comp.Produzione[i].FabbisognoLordo = FabbisognoComp[i] * comp.CoefficenteUtilizzo;
            }
            avviaProduzioneComponente(comp);
            if(comp.PeriodoDiCopertura>1)
            {
                for(int i=0; i<comp.Produzione.Count-comp.PeriodoDiCopertura+1; i++)
                {
                    if(comp.Produzione[i].Versamenti!=0)
                    {
                        ImplementazionePeriodoCompertura(comp, i);
                    }
                }
            }
            foreach (Componente sottoComp in comp.SottoNodi)
            {
                CalcolaProduzioneSottonodi(comp,sottoComp);
            }
        }

        /// <summary>
        /// Per ogni periodo chiama il metodo calcolaPeriodoComponente (calcoli svolti sul componente in input).
        /// </summary>
        /// <param name="comp"></param>
        public void avviaProduzioneComponente(Componente comp)//tutti i periodi
        {
            int tempoProduzione = comp.LeadTime + comp.LeadTimeSicurezza;
            for (int i = 1; i < NumPeriodi+1; i++) //devo fare anche il periodo 0 ?????!!!!
            {
                calcolaPeriodoComponente(comp, tempoProduzione, i);
            }
        }

        /// <summary>
        /// Svolge i vari calcoli sul componente dato in input al periodo dato in input (periodoAdesso)
        /// </summary>
        public void calcolaPeriodoComponente(Componente comp, int TempoProduzioneTotale, int periodoAdesso)//di 1 periodo
        {
            int fabbisognoNetto = (comp.Produzione[periodoAdesso].FabbisognoLordo+comp.ScortaSicurezza) - comp.Produzione[periodoAdesso - 1].Giacenza;
            comp.Produzione[periodoAdesso].Giacenza = comp.Produzione[periodoAdesso - 1].Giacenza - comp.Produzione[periodoAdesso].FabbisognoLordo;
            

            if (fabbisognoNetto < 0) fabbisognoNetto = 0;

            while (comp.Produzione[periodoAdesso-1].Versamenti < fabbisognoNetto)
            {
                comp.Produzione[periodoAdesso-1].Versamenti += comp.Lotto;
            }
            
            if (comp.Produzione[periodoAdesso-1].Versamenti == 0) return;//non devo produrre

            if (periodoAdesso - TempoProduzioneTotale >= 0)
            {
                comp.Produzione[periodoAdesso].Giacenza = (comp.Produzione[periodoAdesso-1].Versamenti + comp.Produzione[periodoAdesso - 1].Giacenza) - comp.Produzione[periodoAdesso].FabbisognoLordo;

                comp.Produzione[periodoAdesso - TempoProduzioneTotale].OrdiniProduzione = comp.Produzione[periodoAdesso-1].Versamenti;
            }
            else
            {
                comp.Produzione[periodoAdesso].Giacenza = (comp.Produzione[periodoAdesso-1].Versamenti + comp.Produzione[periodoAdesso - 1].Giacenza) - comp.Produzione[periodoAdesso].FabbisognoLordo; 

                int periodiNecessari = Math.Abs(periodoAdesso - TempoProduzioneTotale + 1);
                if (periodiNecessari > PeriodiNegativi)
                {
                    PeriodiNegativi = periodiNecessari;
                }
            }
            //comp.Produzione[periodoAdesso].FabbisognoLordo = giacenzaFinale;
        }

        /// <summary>
        /// Calcola e svolge i vari calcoli per "accorpare" i fabbisogni di più periodi di un componente se questo ha PeriodoDiCopertura>1 .
        /// </summary>
        public void ImplementazionePeriodoCompertura(Componente comp,int periodoInizio)
        {
            for(int i=1; i<comp.PeriodoDiCopertura; i++)
            {
                comp.Produzione[periodoInizio].Giacenza += comp.Produzione[periodoInizio + i].Giacenza;
                comp.Produzione[periodoInizio].Versamenti += comp.Produzione[periodoInizio + i].Versamenti;
                comp.Produzione[periodoInizio + i].Versamenti = 0;
                if(periodoInizio-(comp.LeadTime + comp.LeadTimeSicurezza) + 1 > 0)
                {
                    comp.Produzione[periodoInizio - (comp.LeadTime + comp.LeadTimeSicurezza) + 1].OrdiniProduzione += comp.Produzione[periodoInizio - (comp.LeadTime + comp.LeadTimeSicurezza) + 1 + i].OrdiniProduzione;
                    comp.Produzione[periodoInizio - (comp.LeadTime + comp.LeadTimeSicurezza) + 1 + i].OrdiniProduzione = 0;
                }
            }
        }
    }
}
