using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Uzytkownik
    {
        private decimal stanGotowki;
        List<Konto> listaKont;
        private List<WydatekRaz> listaWydatkowRaz;
        private List<WydatekStaly> listaWydatkowSt;
        private List<WplywRaz> listaWplywowRaz;
        private List<WplywStaly> listaWplywowSt;
        private List<Oszczednosc> listaOszczednosci;

        [Key]
        public int IdUzytkownika { get; set; }
        public virtual List<Konto> KontaUzytkownika { get; set; }


        public Uzytkownik()
        {
            ListaKont = new List<Konto>();
            ListaWydatkowRaz = new List<WydatekRaz>();
            ListaWydatkowSt = new List<WydatekStaly>();
            ListaWplywowRaz = new List<WplywRaz>();
            ListaWplywowSt = new List<WplywStaly>();
            ListaOszczednosci = new List<Oszczednosc>();
        }
        public Uzytkownik(decimal stanGotowki) :this()
        {
            StanGotowki = stanGotowki;
        }

        public decimal StanGotowki { get => stanGotowki; set => stanGotowki = value; }
        public List<Konto> ListaKont { get => listaKont; set => listaKont = value; }
        public List<WydatekRaz> ListaWydatkowRaz { get => listaWydatkowRaz; set => listaWydatkowRaz = value; }
        public List<WydatekStaly> ListaWydatkowSt { get => listaWydatkowSt; set => listaWydatkowSt = value; }
        internal List<WplywRaz> ListaWplywowRaz { get => listaWplywowRaz; set => listaWplywowRaz = value; }
        internal List<WplywStaly> ListaWplywowSt { get => listaWplywowSt; set => listaWplywowSt = value; }
        internal List<Oszczednosc> ListaOszczednosci { get => listaOszczednosci; set => listaOszczednosci = value; }

        public void ZapiszDoBazy()
        {
            using var db = new UzytkownikDbContext();
            Console.WriteLine("Zapis do pliku");
            db.Uzytkownicy.Add(this);
            db.SaveChanges();
            Console.WriteLine("Zapisano!");
        }

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
        public void NowyWydatekGotowka(decimal kwota, DateTime data, Kategoria kategoria)
        {
            this.StanGotowki -= kwota;
            WydatekRaz wydatek = new WydatekRaz(kwota, data, kategoria);
            this.ListaWydatkowRaz.Add(wydatek);
        }
        //dotyczy zakupów kartą
        public void NowyWydatekKonto(decimal kwota, DateTime data, Kategoria kategoria, Konto konto)
        {
            konto.StanKonta -= kwota;
            WydatekRaz wydatek = new WydatekRaz(kwota, data, kategoria);
            this.ListaWydatkowRaz.Add(wydatek);
        }
        
        public void NowyWydatekStaly(CyklWydatku cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime deadline, Kategoria kategoria)
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
            //KategoriaWydatkuSt k = KategoriaWydatkuSt.Inne; // Tu trzeba zmienić
            WydatekRaz nowy = new WydatekRaz(wydatek.Kwota, DateTime.Today, wydatek.Kategoria);
            this.ListaWydatkowRaz.Add(nowy);
            wydatek.OplaconyWBiezacymCyklu = true;
        
        }

        public bool ZapisXML(string nazwa)
        {
            try
            {
                using StreamWriter sw = new StreamWriter(nazwa);
                XmlSerializer xs = new(typeof(Uzytkownik));
                xs.Serialize(sw, this);
                return true;
            }
            catch { return false; }
        }

        public static Uzytkownik OdczytXML(string nazwa)
        {
            using StreamReader sr = new StreamReader(nazwa);
            XmlSerializer xs = new(typeof(Uzytkownik));
            Uzytkownik u = (Uzytkownik?)xs.Deserialize(sr)!;
            if(u != null) { return u; }
            else { throw new Exception(); }
        }

        public void RaportMiesieczny(string rokmiesiac)
        {
            string nazwaPliku = $"R.mies.{rokmiesiac}.txt";
            using StreamWriter sw = new StreamWriter(nazwaPliku, true);
            DateTime data = DateTime.ParseExact(rokmiesiac, "yyyyMM", CultureInfo.InvariantCulture);
            string miesiac = data.ToString("MMMM", new CultureInfo("pl-PL"));
            string rok = data.Year.ToString();
            sw.WriteLine($"Raport miesięczny dla miesiąca {miesiac} roku {rok}\n\n");
            /*
            foreach(string kategoria in Kategorie)
            {
                decimal suma = 0;
                foreach(WydatekRaz w in ListaWydatkowRaz)
                {
                    decimal sumakat
                    if(w.Kategoria == kategoria)
                    {
                        sumakat += w.Kwota;
                    }
                }
                if(sumakat > 0)
                {
                    sw.WriteLine($"{kategoria.PadRight(40)}{suma}");
                }
            }
            */
        }



    }
}
