using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Użytkownik
    {
        private decimal stanGotowki;
        private List<Konto> listaKont;

        public Użytkownik(decimal stanGotowki, List<Konto> listaKont)
        {
            this.stanGotowki = stanGotowki;
            this.listaKont = listaKont;
        }

        public decimal StanGotowki { get => stanGotowki; set => stanGotowki = value; }
        public List<Konto> ListaKont { get => listaKont; set => listaKont = value; }

        public decimal SumaNaKontach()
        {
            decimal suma = 0;
            foreach(Konto k in listaKont)
            {
                suma += k.StanKonta;
            }
            return suma;
        }

        public decimal SumaSrodkow()
        {
            return StanGotowki + SumaNaKontach();
        }

        public void DodajKonto(Konto konto)
        {
            ListaKont.Add(konto);
        }

        public void UsunKonto(Konto konto)
        {
            ListaKont.Remove(konto);
        }

        public void WyplaczKonta(Konto konto, decimal kwota)
        {
            konto.StanKonta -= kwota;
            StanGotowki += kwota;
        }

        public void WplacnaKonto(Konto konto, decimal kwota)
        {
            konto.StanKonta += kwota;
            StanGotowki -= kwota;
        }


    }
}
