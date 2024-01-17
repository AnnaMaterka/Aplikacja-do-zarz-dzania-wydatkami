using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Uzytkownik
    {
        private decimal stanGotowki;
        private List<Konto> listaKont;
        private List<WydatekRaz> listaWydatkowRaz;
        private List<WydatekStaly> listaWydatkowSt;
        private List<WplywRaz> listaWplywow;
        
        public Uzytkownik()
        {
            ListaKont = new List<Konto>();
            ListaWydatkowRaz = new List<WydatekRaz>();
            ListaWydatkowSt = new List<WydatekStaly>();
            ListaWplywow = new List<WplywRaz>();
        }
        public Uzytkownik(decimal stanGotowki) :this()
        {
            StanGotowki = stanGotowki;
        }

        public decimal StanGotowki { get => stanGotowki; set => stanGotowki = value; }
        public List<Konto> ListaKont { get => listaKont; set => listaKont = value; }
        public List<WydatekRaz> ListaWydatkowRaz { get => listaWydatkowRaz; set => listaWydatkowRaz = value; }
        public List<WydatekStaly> ListaWydatkowSt { get => listaWydatkowSt; set => listaWydatkowSt = value; }
        internal List<WplywRaz> ListaWplywow { get => listaWplywow; set => listaWplywow = value; }

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

        public void WyplaczKonta(Konto konto, decimal kwota, DateTime data, string kategoria)
        {
            if (konto.StanKonta < kwota)
            {
                throw new BrakSrodkow($"Nie można dokonać wypłaty, ponieważ obecny stan środków wynosi:{konto.StanKonta}");
            }
            konto.StanKonta -= kwota;
            StanGotowki += kwota;
            WydatekRaz w = new(kwota, data, kategoria);
            ListaWydatkowRaz.Add(w);
        }

        public void WplacnaKonto(Konto konto, decimal kwota, DateTime data, string kategoria)
        {
            konto.StanKonta += kwota;
            StanGotowki -= kwota;
            WplywRaz w = new(kwota, data, kategoria);
            listaWplywow.Add(w);
        }

        public void WplywGotowki(decimal kwota)
        {
            StanGotowki += kwota;
        }
        /*
        public void NowyWydatekGotowka(decimal kwota, DateTime data, KategoriaWydatku kategoria)
        {
            this.StanGotowki -= kwota;
            WydatekRaz wydatek = new WydatekRaz(kwota, data, kategoria);
            this.ListaWydatkowRaz.Add(wydatek);
        }

        public void NowyWydatekKonto(decimal kwota, DateTime data, KategoriaWydatku kategoria, Konto konto)
        {
            konto.StanKonta -= kwota;
            WydatekRaz wydatek = new WydatekRaz(kwota, data, kategoria);
            this.ListaWydatkowRaz.Add(wydatek);
        }
        */
        public void NowyWydatekStaly(CyklWydatku cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime deadline, KategoriaWydatkuSt kategoria)
        {
            WydatekStaly wydatek = new WydatekStaly(cyklWydatku, oplaconyWBiezacymCyklu, stalaKwota, kwota, deadline, kategoria);
            this.ListaWydatkowSt.Add(wydatek);
        }

        /*public void OplacWydatekStaly(WydatekStaly wydatek, bool PlatnoscZKonta, Konto konto)
        {
            if (PlatnoscZKonta)
            {
                konto.StanKonta -= wydatek.Kwota;
            }
            else
            {
                this.StanGotowki -= wydatek.Kwota;
            }
            KategoriaWydatku k = KategoriaWydatku.Inne; // Tu trzeba zmienić
            WydatekRaz nowy = new WydatekRaz(wydatek.Kwota, DateTime.Today, k);
            this.ListaWydatkowRaz.Add(nowy);
            wydatek.OplaconyWBiezacymCyklu = true;
        
        }*/

    }
}
