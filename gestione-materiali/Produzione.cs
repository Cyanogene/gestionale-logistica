using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestione_materiali
{
    class Produzione
    {
        public List<Periodo> Periodi { get; set; }

        public Produzione(List<Periodo> input)
        {
            Periodi = input;
        }

        public void CalcolaProgrammazioneProduzione()
        {
            for (int i = 0; i < Periodi.Count; i++)
            {
                CalcolaDati(i);
            }
        }

        public void CalcolaDati(int periodo)
        {
            CalcolaGiacenza(periodo);
            if (Periodi[periodo].Giacenza < SS) // SS = Scorta di Sicurezza
            {
                CalcolaVersamenti(periodo);
                CalcolaOrdiniDiProduzione(periodo);
                Periodi[periodo].Giacenza += Periodi[periodo].Versamenti;
            }

            else
            {
                Periodi[periodo].Versamenti = 0;
                Periodi[periodo].OrdiniProduzione = 0;
            }
        }

        // Giacenza(periodo) = Giacenza(periodo-1) - Max(Previsioni, OrdiniVendita)
        public void CalcolaGiacenza(int periodo)
        {
            Periodi[periodo].Giacenza =
               Periodi[periodo - 1].Giacenza - Math.Max(Periodi[periodo].Previsioni, Periodi[periodo].OrdiniVendita);
        }

        public void CalcolaVersamenti(int periodo)
        {
            if (Periodi[periodo].Versamenti == -1)
            {
                int TempLotto = Lotto;
                while (TempLotto < SS + Math.Abs(Periodi[periodo].Giacenza))
                {
                    TempLotto += Lotto;
                }
                Periodi[periodo].Versamenti = TempLotto;
            }
        }

        public void CalcolaOrdiniDiProduzione(int periodo)
        {
            if (LT == 1)    //LT = Lead Time
            {
                Periodi[periodo].OrdiniProduzione = Periodi[periodo].Versamenti;
            }
            if (LT > 1)
            {
                Periodi[periodo - LT + 1].OrdiniProduzione = Periodi[periodo].Versamenti;
            }
        }
    }
}
