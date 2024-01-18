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
        List<Konto> listaKont;
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

        // WyplaczKonta służy do wypłacania, np w bankomacie
        //pieniądze na koncie są zamieniane na gotówkę
        public void WyplaczKonta(Konto konto, decimal kwota, DateTime data, string kategoria)
        {
            if (konto.StanKonta < kwota)
            {
                throw new BrakSrodkow($"Nie można dokonać wypłaty, ponieważ obecny stan środków wynosi:{konto.StanKonta}");
            }
            konto.StanKonta -= kwota;
            StanGotowki += kwota;
        }

        // WplacnaKonto służy do wpłacania, np w bankomacie
        // gotówkę zamieniamy na środki na koncie
        public void WplacnaKonto(Konto konto, decimal kwota, DateTime data, string kategoria)
        {
            konto.StanKonta += kwota;
            StanGotowki -= kwota;
        }

        public void WplywGotowki(decimal kwota)
        {
            StanGotowki += kwota;
        }
        // dotyczy zakupów gotówką
        public void NowyWydatekGotowka(decimal kwota, DateTime data, string kategoria)
        {
            this.StanGotowki -= kwota;
            WydatekRaz wydatek = new WydatekRaz(kwota, data, kategoria);
            this.ListaWydatkowRaz.Add(wydatek);
        }
        //dotyczy zakupów kartą
        public void NowyWydatekKonto(decimal kwota, DateTime data, string kategoria, Konto konto)
        {
            konto.StanKonta -= kwota;
            WydatekRaz wydatek = new WydatekRaz(kwota, data, kategoria);
            this.ListaWydatkowRaz.Add(wydatek);
        }
        
        public void NowyWydatekStaly(CyklWydatku cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime deadline, KategoriaWydatkuSt kategoria)
        {
            WydatekStaly wydatek = new WydatekStaly(cyklWydatku, oplaconyWBiezacymCyklu, stalaKwota, kwota, deadline, kategoria);
            this.ListaWydatkowSt.Add(wydatek);
        }

        public void OplacWydatekStaly(WydatekStaly wydatek, Konto konto)
        {
            if (konto.StanKonta < wydatek.Kwota)
            {
                throw new BrakSrodkow($"Nie można dokonać wypłaty, ponieważ obecny stan środków wynosi:{konto.StanKonta}");
            }
            konto.StanKonta -= wydatek.Kwota;
            KategoriaWydatkuSt k = KategoriaWydatkuSt.Inne; // Tu trzeba zmienić
            WydatekRaz nowy = new WydatekRaz(wydatek.Kwota, DateTime.Today, k.ToString());
            this.ListaWydatkowRaz.Add(nowy);
            wydatek.OplaconyWBiezacymCyklu = true;
        
        }

    }
}
