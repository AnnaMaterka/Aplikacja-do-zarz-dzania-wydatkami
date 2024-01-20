using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Konto
    {
        private string nazwa;
        private string nazwaBanku;
        private decimal stanKonta;
        private Uzytkownik uzytkownik;
        private List<WydatekRaz> listaWydatkowRaz;
        private List<WydatekStaly> listaWydatkowSt;
        private List<WplywRaz> listaWplywowRaz;
        private List<WplywStaly> listaWplywowSt;
        private List<Oszczednosc> listaOszczednosci;

        [Key]
        public int IdKonta { get; set; }
        public int IdUzytkownika { get; set; }

        public virtual Uzytkownik Uzytkownik { get; set; }
        public virtual List<WydatekRaz> Wydatki { get; set; }
        public virtual List<WydatekStaly> WydatekStale { get; set; }
        public virtual List<WplywRaz> Wplywy { get; set; }
        public virtual List<Oszczednosc> Oszczednosci { get; set; }

        public Konto()
        {
            listaWydatkowRaz = new List<WydatekRaz>();
            listaWydatkowSt = new List<WydatekStaly>();
            listaWplywowRaz = new List<WplywRaz>();
            listaWplywowSt = new List<WplywStaly>();
            listaOszczednosci = new List<Oszczednosc>();
            Nazwa = "Nazwa konta";
        }
        public Konto(string nazwaBanku, decimal stanKonta, Uzytkownik uzytkownik) :this()
        {
            NazwaBanku = nazwaBanku;
            StanKonta = stanKonta;
            Uzytkownik1 = uzytkownik;
        }
        public Konto(string nazwaBanku, decimal stanKonta, Uzytkownik uzytkownik, string nazwa) : this()
        {
            NazwaBanku = nazwaBanku;
            StanKonta = stanKonta;
            Uzytkownik1 = uzytkownik;
            Nazwa = nazwa;
        }

        public string NazwaBanku { get => nazwaBanku; init => nazwaBanku = value; }
        public decimal StanKonta { get => stanKonta; set => stanKonta = value; }
        public Uzytkownik Uzytkownik1 { get => uzytkownik; set => uzytkownik = value; }
        public List<WydatekRaz> ListaWydatkowRaz { get => listaWydatkowRaz; set => listaWydatkowRaz = value; }
        public List<WydatekStaly> ListaWydatkowSt { get => listaWydatkowSt; set => listaWydatkowSt = value; }
        internal List<WplywRaz> ListaWplywowRaz { get => listaWplywowRaz; set => listaWplywowRaz = value; }
        internal List<WplywStaly> ListaWplywowSt { get => listaWplywowSt; set => listaWplywowSt = value; }
        internal List<Oszczednosc> ListaOszczednosci { get => listaOszczednosci; set => listaOszczednosci = value; }
        public string Nazwa { get => nazwa; set => nazwa = value; }

        //dotyczy zakupów kartą
        public void NowyWydatekKonto(decimal kwota, DateTime data, Kategoria kategoria)
        {
            StanKonta -= kwota;
            WydatekRaz wydatek = new WydatekRaz(kwota, data, kategoria);
            this.ListaWydatkowRaz.Add(wydatek);
        }

        public void NowyWydatekStaly(CyklWydatku cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime deadline, Kategoria kategoria)
        {
            WydatekStaly wydatek = new WydatekStaly(cyklWydatku, oplaconyWBiezacymCyklu, stalaKwota, kwota, deadline, kategoria);
            this.ListaWydatkowSt.Add(wydatek);
        }

        public void OplacWydatekStaly(WydatekStaly wydatek)
        {
            if (StanKonta < wydatek.Kwota)
            {
                throw new BrakSrodkow($"Nie można dokonać wypłaty, ponieważ obecny stan środków wynosi:{StanKonta}");
            }
            StanKonta -= wydatek.Kwota;
            WydatekRaz nowy = new WydatekRaz(wydatek.Kwota, DateTime.Today, wydatek.Kategoria);
            this.ListaWydatkowRaz.Add(nowy);
            wydatek.OplaconyWBiezacymCyklu = true;

        }

        public void RaportMiesieczny(string rokmiesiac)
        {
            string nazwaPliku = $"{IdKonta}.R.mies.{rokmiesiac}.html";
            string htmlContent = "<html><head><title>Raport miesięczny</title></head><body>";
            DateTime data = DateTime.ParseExact(rokmiesiac, "yyyyMM", CultureInfo.InvariantCulture);
            string miesiac = data.ToString("MMMM", new CultureInfo("pl-PL"));
            string rok = data.Year.ToString();
            htmlContent += $"<h1>Raport miesięczny obrotów na koncie dla miesiąca {miesiac} roku {rok}</h1>";

            decimal sumaWplywow = Wplywy.Where(WplywRaz => WplywRaz.Data.ToString("yyyyMM") == rokmiesiac).Sum(WplywRaz => WplywRaz.Kwota);
            htmlContent += $"<h2>Suma wpływów: <b>{sumaWplywow}</b></h2>";
            decimal sumaWydatkow = Wydatki.Where(WydatekRaz => WydatekRaz.Data.ToString("yyyyMM") == rokmiesiac).Sum(WydatekRaz => WydatekRaz.Kwota);
            htmlContent += $"<h2>Suma wydatków: <b>{sumaWydatkow}</b></h2>";
            decimal sumaOszczednosci = Wplywy.Where(Oszczednosc => Oszczednosc.Data.ToString("yyyyMM") == rokmiesiac).Sum(Oszczednosc => Oszczednosc.Kwota);
            htmlContent += $"<h2>Suma środków przeznaczonych na oszczędności: <b>{sumaOszczednosci}</b></h2>";
            htmlContent += "</body></html>";

            File.WriteAllText(nazwaPliku, htmlContent);
        }

        public void RaportRoczny(string rok)
        {
            string nazwaPliku = $"{IdKonta}.R.rocz.{rok}.html";
            string htmlContent = "<html><head><title>Raport roczny</title></head><body>";
            htmlContent += $"<h1>Raport roczny obrotów na koncie dla roku {rok}</h1>";
            decimal sumaWplywow = Wplywy.Where(WplywRaz => WplywRaz.Data.ToString("yyyy") == rok).Sum(WplywRaz => WplywRaz.Kwota);
            htmlContent += $"<h2>Suma wpływów: <b>{sumaWplywow}</b></h2>";
            decimal sumaWydatkow = Wydatki.Where(WydatekRaz => WydatekRaz.Data.ToString("yyyy") == rok).Sum(WydatekRaz => WydatekRaz.Kwota);
            htmlContent += $"<h2>Suma wydatków: <b>{sumaWydatkow}</b></h2>";
            decimal sumaOszczednosci = Wplywy.Where(Oszczednosc => Oszczednosc.Data.ToString("yyyy") == rok).Sum(Oszczednosc => Oszczednosc.Kwota);
            htmlContent += $"<h2>Suma środków przeznaczonych na oszczędności: <b>{sumaOszczednosci}</b></h2>";
            htmlContent += "</body></html>";

            File.WriteAllText(nazwaPliku, htmlContent);
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
