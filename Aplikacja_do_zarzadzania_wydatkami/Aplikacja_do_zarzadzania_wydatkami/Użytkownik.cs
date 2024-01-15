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
        private List<WydatekRaz> listaWydatkowRaz;
        private List<WydatekStaly> listaWydatkowSt;

        public Użytkownik(decimal stanGotowki, List<Konto> listaKont)
        {
            this.stanGotowki = stanGotowki;
            this.listaKont = listaKont;
            this.ListaWydatkowRaz = new List<WydatekRaz>();
            this.ListaWydatkowSt = new List<WydatekStaly>();
        }

        public decimal StanGotowki { get => stanGotowki; set => stanGotowki = value; }
        public List<Konto> ListaKont { get => listaKont; set => listaKont = value; }
        public List<WydatekRaz> ListaWydatkowRaz { get => listaWydatkowRaz; set => listaWydatkowRaz = value; }
        public List<WydatekStaly> ListaWydatkowSt { get => listaWydatkowSt; set => listaWydatkowSt = value; }

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
            StanGotowki += konto.StanKonta;
            ListaKont.Remove(konto);
        }

        public void WyplaczKonta(Konto konto, decimal kwota)
        {
            if(konto.StanKonta < kwota)
            {
                throw new BrakSrodkow($"Nie można dokonać wypłaty, ponieważ obecny stan środków wynosi:{konto.StanKonta}");
            }
            konto.StanKonta -= kwota;
            StanGotowki += kwota;
        }

        public void WplacnaKonto(Konto konto, decimal kwota)
        {
            konto.StanKonta += kwota;
            StanGotowki -= kwota;
        }

        //public void NowyWydatek()


    }
}
