using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestione_materiali
{
    class Produzione
    {
        public List<Periodo> ciao { get; set; } 

        public Produzione(List<Periodo> input)
        {
            ciao = input;
        }
        //public void CalcolaPrevisioni()
        //{

        //}

        //public void CalcolaOrdiniDiVendita()
        //{

        //}
        public void ProgrammazioneProduzione()
        {
            for (int i = 0; i < 7; i++)
            {

            }
        }


        public int CalcolaGiacenza(int periodo,int disponibilitàMesePrecedente)
        {
            return disponibilitàMesePrecedente - Math.Max()
        }

        public void CalcolaVersamenti()
        {

        }

        public void CalcolaOrdiniDiProduzione()
        {

        }
    }
}
