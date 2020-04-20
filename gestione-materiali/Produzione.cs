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
        public Componente comp;

        public Produzione(List<Periodo> input, Componente c)
        {
            Periodi = input;
            comp = c;
        }

        public List<Periodo> CalcolaProgrammazioneProduzione()
        {
            for (int i = 1; i < Periodi.Count; i++)
            {
                CalcolaDati(i);
            }
            return Periodi;
        }

        public void CalcolaDati(int periodo)
        {
            CalcolaGiacenza(periodo);
            if (Periodi[periodo].Giacenza < comp.ScortaSicurezza)
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
                int TempLotto = comp.Lotto;
                while (TempLotto < comp.ScortaSicurezza + Math.Abs(Periodi[periodo].Giacenza))
                {
                    TempLotto += comp.Lotto;
                }
                Periodi[periodo].Versamenti = TempLotto;
            }
        }

        public void CalcolaOrdiniDiProduzione(int periodo)
        {
            Periodi[periodo - comp.LeadTime + 1].OrdiniProduzione = Periodi[periodo].Versamenti;
        }
    }
}
