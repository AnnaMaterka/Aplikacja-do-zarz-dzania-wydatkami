using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Konto :Aktualizacja
    {
        private string nazwa;
        private string nazwaBanku;
        private decimal stanKonta;
        private Uzytkownik uzytkownik;
        //private List<WydatekRaz> listaWydatkowRaz;
        //private List<WydatekStaly> listaWydatkowSt;
        //private List<WplywRaz> listaWplywowRaz;
        //private List<WplywStaly> listaWplywowSt;
        //private List<Oszczednosc> listaOszczednosci;

        public virtual Uzytkownik Uzytkownik { get; set; }
        [Key]
        public int IdKonta { get; set; }

        //[ForeignKey("Uzytkownik")]
        //public int IdUzytkownika { get; set; }

        
        public virtual List<WydatekRaz> Wydatki { get; set; }
        public virtual List<WydatekStaly> WydatekStale { get; set; }
        public virtual List<WplywRaz> Wplywy { get; set; }
        public virtual List<Oszczednosc> Oszczednosci { get; set; }

        public Konto()
        {
            //listaWydatkowRaz = new List<WydatekRaz>();
            //listaWydatkowSt = new List<WydatekStaly>();
            //listaWplywowRaz = new List<WplywRaz>();
            //listaWplywowSt = new List<WplywStaly>();
            //listaOszczednosci = new List<Oszczednosc>();
            Nazwa = "Nazwa konta";
        }
        public Konto(string nazwaBanku, decimal stanKonta, Uzytkownik uzytkownik) : this()
        {
            NazwaBanku = nazwaBanku;
            StanKonta = stanKonta;
            Uzytkownik = uzytkownik;
        }
        public Konto(string nazwaBanku, decimal stanKonta, Uzytkownik uzytkownik, string nazwa) : this()
        {
            NazwaBanku = nazwaBanku;
            StanKonta = stanKonta;
            Uzytkownik = uzytkownik;
            Nazwa = nazwa;
        }

        public string NazwaBanku { get => nazwaBanku; init => nazwaBanku = value; }
        public decimal StanKonta
        {
            get => stanKonta;
            set
            {
                if (stanKonta != value)
                {
                    stanKonta = value;
                    OnPropertyChanged();
                }
            }
        }
        //public Uzytkownik Uzytkownik1 { get => uzytkownik; set => uzytkownik = value; }
        //public List<WydatekRaz> ListaWydatkowRaz { get => listaWydatkowRaz; set => listaWydatkowRaz = value; }
        //public List<WydatekStaly> ListaWydatkowSt { get => listaWydatkowSt; set => listaWydatkowSt = value; }
        //internal List<WplywRaz> ListaWplywowRaz { get => listaWplywowRaz; set => listaWplywowRaz = value; }
        //internal List<WplywStaly> ListaWplywowSt { get => listaWplywowSt; set => listaWplywowSt = value; }
        //internal List<Oszczednosc> ListaOszczednosci { get => listaOszczednosci; set => listaOszczednosci = value; }
        public string Nazwa { get => nazwa; set => nazwa = value; }

        public int PobierzIdKategorii(string nazwaKategorii)
        {
            using (var dbContext = new UzytkownikDbContext())
            {
                // Sprawdź, czy istnieje kategoria o podanej nazwie
                var kategoria = dbContext.Kategorie.FirstOrDefault(k => k.NazwaKategorii == nazwaKategorii);

                // Jeśli kategoria istnieje, zwróć jej IdKategorii, w przeciwnym razie -1
                return kategoria?.IdKategorii ?? -1;
            }
        }

        //dotyczy zakupów kartą
        public void NowyWydatekKonto(decimal kwota, DateTime data, string kategoria)
        {
            StanKonta -= kwota;
            WydatekRaz wydatek = new WydatekRaz(kwota, data, kategoria);
            //this.ListaWydatkowRaz.Add(wydatek);
            this.Wydatki.Add(wydatek);
            OnPropertyChanged(nameof(StanKonta));
        }

        public void NowyWydatekStaly(Cykl cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime deadline, string kategoria)
        {
            WydatekStaly wydatek = new WydatekStaly(cyklWydatku, oplaconyWBiezacymCyklu, stalaKwota, kwota, deadline, kategoria);
            //this.ListaWydatkowSt.Add(wydatek);
            this.WydatekStale.Add(wydatek);
            OnPropertyChanged(nameof(StanKonta));
        }

        public void OplacWydatekStaly(WydatekStaly wydatek)
        {
            if (StanKonta < wydatek.Kwota)
            {
                throw new BrakSrodkow($"Nie można dokonać wypłaty, ponieważ obecny stan środków wynosi:{StanKonta}");
            }
            StanKonta -= wydatek.Kwota;
            WydatekRaz nowy = new WydatekRaz(wydatek.Kwota, DateTime.Today, wydatek.Kategoria.NazwaKategorii);
            //this.ListaWydatkowRaz.Add(nowy);
            this.Wydatki.Add(nowy);
            OnPropertyChanged(nameof(StanKonta));
            wydatek.OplaconyWBiezacymCyklu = true;

        }
        public delegate decimal SumaZaOkres(string okres);

        public decimal SumaWplywowMies(string rokmies)
        {
            return Wplywy.Where(WplywRaz => WplywRaz.Data.ToString("yyyyMM") == rokmies).Sum(WplywRaz => WplywRaz.Kwota);
        }

        public decimal SumaWydatkowMies(string rokmies)
        {
            return Wydatki.Where(WydatekRaz => WydatekRaz.Data.ToString("yyyyMM") == rokmies).Sum(WydatekRaz => WydatekRaz.Kwota);
        }

        public decimal SumaOszczednosciMies(string rokmies)
        {
            return Oszczednosci.Where(Oszczednosc => Oszczednosc.Data.ToString("yyyyMM") == rokmies).Sum(Oszczednosc => Oszczednosc.Kwota);
        }

        public decimal SumaWplywowRok(string rok)
        {
            return Wplywy.Where(WplywRaz => WplywRaz.Data.ToString("yyyy") == rok).Sum(WplywRaz => WplywRaz.Kwota);
        }

        public decimal SumaWydatkowRok(string rok)
        {
            return Wydatki.Where(WydatekRaz => WydatekRaz.Data.ToString("yyyy") == rok).Sum(WydatekRaz => WydatekRaz.Kwota);
        }

        public decimal SumaOszczednosciRok(string rok)
        {
            return Oszczednosci.Where(Oszczednosc => Oszczednosc.Data.ToString("yyyy") == rok).Sum(Oszczednosc => Oszczednosc.Kwota);
        }
    }
}
