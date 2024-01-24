using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public virtual Uzytkownik Uzytkownik { get; set; }
        [Key]
        public int IdKonta { get; set; }

        private ObservableCollection<WydatekRaz> wydatki;
        private ObservableCollection<WydatekStaly> wydatkiStale;

        private ObservableCollection<WplywRaz> wplywy; 
        private ObservableCollection<WplywStaly> wplywyStale;
        public virtual List<Oszczednosc> Oszczednosci { get; set; }

        public ObservableCollection<WplywRaz> Wplywy
        {
            get => wplywy;
            set
            {
                if (wplywy != value)
                {
                    wplywy = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<WplywStaly> WplywyStale
        {
            get => wplywyStale;
            set
            {
                if (wplywyStale != value)
                {
                    wplywyStale = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<WydatekRaz> Wydatki
        {
            get => wydatki;
            set
            {
                if (wydatki != value)
                {
                    wydatki = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<WydatekStaly> WydatkiStale
        {
            get => wydatkiStale;
            set
            {
                if (wydatkiStale != value)
                {
                    wydatkiStale = value;
                    OnPropertyChanged();
                }
            }
        }

        public Konto()
        {
            Wplywy = new ObservableCollection<WplywRaz>();
            WplywyStale = new ObservableCollection<WplywStaly>();
            Wydatki = new ObservableCollection<WydatekRaz>();
            WydatkiStale = new ObservableCollection<WydatekStaly>();
            Oszczednosci = new List<Oszczednosc>();
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
        public string Nazwa { get => nazwa; set => nazwa = value; }


        //dotyczy zakupów kartą
        public void NowyWydatekKonto(WydatekRaz wydatek)
        {
            this.Wydatki.Add(wydatek);
        }
        public void NowyWplywKonto(WplywRaz wplyw)
        {
            Wplywy.Add(wplyw);
        }
        public void NowyWplywStaly(WplywStaly staly)
        {
            WplywyStale.Add(staly);
        }
        public void NowyWydatekStaly(WydatekStaly wydatekStaly)
        {
            WydatkiStale.Add(wydatekStaly);
        }
        public void ZapiszDoBazy()
        {
            using var db = new UzytkownikDbContext();
            Console.WriteLine("Zapis do pliku");

            var existingEntity = db.Konta.Find(this.IdKonta);

            if (existingEntity != null)
            {
                db.Entry(existingEntity).CurrentValues.SetValues(this);
            }
            else
            {
                //db.Konta.Add(this);
            }

            db.SaveChanges();
            Console.WriteLine("Zapisano!");
        }
        public void NowyWydatekStaly(Cykl cyklWydatku, bool oplaconyWBiezacymCyklu, bool stalaKwota, decimal kwota, DateTime deadline, string kategoria)
        {
            WydatekStaly wydatek = new WydatekStaly(cyklWydatku, oplaconyWBiezacymCyklu, stalaKwota, kwota, deadline, kategoria);
            //this.ListaWydatkowSt.Add(wydatek);
            this.WydatkiStale.Add(wydatek);
            OnPropertyChanged(nameof(StanKonta));
        }

        public void OplacWydatekStaly(WydatekStaly wydatek)
        {
            if (StanKonta < wydatek.Kwota)
            {
                throw new BrakSrodkow($"Nie można dokonać wypłaty, ponieważ obecny stan środków wynosi:{StanKonta}");
            }
            StanKonta -= wydatek.Kwota;
            WydatekRaz nowy = new WydatekRaz(wydatek.Kwota, DateTime.Today, wydatek.Kategoria);
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

        public decimal SumaWplywowTyg(bool biezacy)
        {
            DateTime poniedzialekbiez = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime poniedzialekbyly = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
            if (biezacy)
            {
                return Wplywy.Where(WplywRaz => WplywRaz.Data >= poniedzialekbiez).Sum(WplywRaz => WplywRaz.Kwota);
            }
            return Wplywy.Where(WplywRaz => (WplywRaz.Data >= poniedzialekbyly)&(WplywRaz.Data < poniedzialekbiez)).Sum(WplywRaz => WplywRaz.Kwota);
        }

        public decimal SumaWydatkowTyg(bool biezacy)
        {
            DateTime poniedzialekbiez = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime poniedzialekbyly = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
            if (biezacy)
            {
                return Wydatki.Where(WydatekRaz => WydatekRaz.Data >= poniedzialekbiez).Sum(WydatekRaz => WydatekRaz.Kwota);
            }
            return Wydatki.Where(WydatekRaz => (WydatekRaz.Data >= poniedzialekbyly) & (WydatekRaz.Data < poniedzialekbiez)).Sum(WydatekRaz => WydatekRaz.Kwota);
        }

        public decimal SumaOszczednosciTyg(bool biezacy)
        {
            DateTime poniedzialekbiez = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime poniedzialekbyly = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
            if (biezacy)
            {
                return Oszczednosci.Where(Oszczednosc => Oszczednosc.Data >= poniedzialekbiez).Sum(Oszczednosc => Oszczednosc.Kwota);
            }
            return Oszczednosci.Where(Oszczednosc => (Oszczednosc.Data >= poniedzialekbyly) & (Oszczednosc.Data < poniedzialekbiez)).Sum(Oszczednosc => Oszczednosc.Kwota);
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
    }
}
