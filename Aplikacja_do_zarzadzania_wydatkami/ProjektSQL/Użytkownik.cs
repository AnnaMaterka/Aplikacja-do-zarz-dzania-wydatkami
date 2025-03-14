﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using ProjektSQL;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Aplikacja_do_zarzadzania_wydatkami
{
    public class Uzytkownik : Aktualizacja
    {
        private static long login = 10000;
        private string imie;
        private decimal stanGotowki;
        private ObservableCollection<Konto> listaKont;

        public ObservableCollection<Konto> ListaKont
        {
            get => listaKont;
            set
            {
                if (listaKont != value)
                {
                    listaKont = value;
                    OnPropertyChanged();
                }
            }
        }

        [Key]
        public int IdUzytkownika { get; set; }

        //private ObservableCollection<Wydatek> listaWydatkow;
        //public ObservableCollection<Wydatek> ListaWydatkow
        //{
        //    get => listaWydatkow;
        //    set
        //    {
        //        if(listaWydatkow != value)
        //        {
        //            listaWydatkow = value; 
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        //private ObservableCollection<Wplyw> listaWplywow;
        //public ObservableCollection<Wplyw> ListaWplywow
        //{
        //    get => listaWplywow;
        //    set
        //    {
        //        if (listaWplywow != value)
        //        {
        //            listaWplywow = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        public virtual List<WydatekRaz> WydatkiGotowka { get; set; }
        public virtual List<WplywRaz> WplywyGotowka { get; set; }
        public virtual List<Oszczednosc> OszczednosciWGotowce { get; set; }

        public Uzytkownik()
        {
            StanGotowki = 0;
            Login = login;
            login++;
            Imie = "Podane imie";
            ListaKont = new ObservableCollection<Konto>();
            WplywyGotowka = new List<WplywRaz>();
            WydatkiGotowka = new List<WydatekRaz>();
            OszczednosciWGotowce = new List<Oszczednosc>();
        }
        public Uzytkownik(decimal stanGotowki) : this()
        {
            StanGotowki = stanGotowki;
        }
        public Uzytkownik(string imie) : this()
        {
            Imie = imie;
        }
        public Uzytkownik(decimal stanGotowki, string imie) : this()
        {
            StanGotowki = stanGotowki;
            Imie = imie;
        }
        public string Imie { get => imie; set => imie = value; }
        public decimal StanGotowki { get => stanGotowki; set => stanGotowki = value; }
        public long Login { get; }
        public void ZapiszDoBazy()
        {
            using var db = new UzytkownikDbContext();
            Console.WriteLine("Zapis do pliku");

            var existingEntity = db.Uzytkownicy.Find(this.IdUzytkownika);

            if (existingEntity != null)
            {
                db.Entry(existingEntity).CurrentValues.SetValues(this);
            }
            else
            {
                db.Uzytkownicy.Add(this);
            }

            db.SaveChanges();
            Console.WriteLine("Zapisano!");
        }


        public decimal SumaNaKontach()
        {
            decimal suma = 0;
            foreach (Konto k in listaKont)
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
        public void DodajWplywGotowki(WplywRaz wplyw)
        {
            this.WplywyGotowka.Add(wplyw);
            OnPropertyChanged(nameof(StanGotowki));
        }
        // dotyczy zakupów gotówką
        public void NowyWydatekGotowka(WydatekRaz wydatek)
        {
            this.WydatkiGotowka.Add(wydatek);
        }
        public void OdlozGotowke(int kwota, string cel)
        {
            this.StanGotowki -= kwota;
            DateTime data = DateTime.Today;
            Oszczednosc odlozona = new(kwota, data, cel);
            this.OszczednosciWGotowce.Add(odlozona);
            OnPropertyChanged(nameof(StanGotowki));
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
            if (u != null) { return u; }
            else { throw new Exception(); }
        }

        public void RaportMiesieczny(string rokmiesiac)
        {
            string nazwaPliku = $"{IdUzytkownika}.R.mies.{rokmiesiac}.html";
            string htmlContent = "<html><head><title>Raport miesięczny</title></head><body>";
            DateTime data = DateTime.ParseExact(rokmiesiac, "yyyyMM", CultureInfo.InvariantCulture);
            string miesiac = data.ToString("MMMM", new CultureInfo("pl-PL"));
            string rok = data.Year.ToString();
            htmlContent += $"<h1>Raport miesięczny obrotów na koncie dla miesiąca {miesiac} roku {rok}</h1>";
            decimal sumaWplywow = ListaKont.Sum(Konto => Konto.SumaWplywowMies(rokmiesiac));
            sumaWplywow += WplywyGotowka.Where(WplywRaz => WplywRaz.Data.ToString("yyyyMM") == rokmiesiac).Sum(WplywRaz => WplywRaz.Kwota);
            htmlContent += $"<h2>Suma wpływów: <b>{sumaWplywow}</b></h2>";
            decimal sumaWydatkow = ListaKont.Sum(Konto => Konto.SumaWydatkowMies(rokmiesiac));
            sumaWydatkow += WydatkiGotowka.Where(WydatekRaz => WydatekRaz.Data.ToString("yyyyMM") == rokmiesiac).Sum(WydatekRaz => WydatekRaz.Kwota);
            htmlContent += $"<h2>Suma wydatków: <b>{sumaWydatkow}</b></h2>";
            decimal sumaOszczednosci = ListaKont.Sum(Konto => Konto.SumaOszczednosciMies(rokmiesiac));
            sumaOszczednosci += OszczednosciWGotowce.Where(Oszczednosc => Oszczednosc.Data.ToString("yyyyMM") == rokmiesiac).Sum(Oszczednosc => Oszczednosc.Kwota);
            htmlContent += $"<h2>Suma środków przeznaczonych na oszczędności: <b>{sumaOszczednosci}</b></h2>";
            htmlContent += "</body></html>";

            File.WriteAllText(nazwaPliku, htmlContent);
        }

        public void RaportRoczny(string rok)
        {
            string nazwaPliku = $"{IdUzytkownika}.R.rocz.{rok}.html";
            string htmlContent = "<html><head><title>Raport roczny</title></head><body>";
            DateTime data = DateTime.ParseExact(rok, "yyyy", CultureInfo.InvariantCulture);
            htmlContent += $"<h1>Raport miesięczny obrotów na koncie dla roku {rok}</h1>";
            decimal sumaWplywow = ListaKont.Sum(Konto => Konto.SumaWplywowRok(rok));
            sumaWplywow += WplywyGotowka.Where(WplywRaz => WplywRaz.Data.ToString("yyyy") == rok).Sum(WplywRaz => WplywRaz.Kwota);
            htmlContent += $"<h2>Suma wpływów: <b>{sumaWplywow}</b></h2>";
            decimal sumaWydatkow = ListaKont.Sum(Konto => Konto.SumaWydatkowRok(rok));
            sumaWydatkow += WydatkiGotowka.Where(WydatekRaz => WydatekRaz.Data.ToString("yyyy") == rok).Sum(WydatekRaz => WydatekRaz.Kwota);
            htmlContent += $"<h2>Suma wydatków: <b>{sumaWydatkow}</b></h2>";
            decimal sumaOszczednosci = ListaKont.Sum(Konto => Konto.SumaOszczednosciRok(rok));
            sumaOszczednosci += OszczednosciWGotowce.Where(Oszczednosc => Oszczednosc.Data.ToString("yyyy") == rok).Sum(Oszczednosc => Oszczednosc.Kwota);
            htmlContent += $"<h2>Suma środków przeznaczonych na oszczędności: <b>{sumaOszczednosci}</b></h2>";
            htmlContent += "</body></html>";

            File.WriteAllText(nazwaPliku, htmlContent);
        }

        public void RaportTyg(string typ)
        {
            string nazwaPliku = $"{IdUzytkownika}.R.rocz.{typ}.html";
            string htmlContent = "<html><head><title>Raport tygodniowy</title></head><body>";
            if(typ == "TB")
            {
                htmlContent += "<h1>Raport obrotów na koncie w bieżącym tygodniu </h1>";
                htmlContent += $"<h2>Suma wpływów: <b>{SumaWplywowTyg(true)}</b></h2>";
                htmlContent += $"<h2>Suma wydatków: <b>{SumaWydatkowTyg(true)}</b></h2>";
                htmlContent += $"<h2>Suma środków przeznaczonych na oszczędności: <b>{SumaOszczednosciTyg(true)}</b></h2>";
            }
            else
            {
                htmlContent += "<h1>Raport obrotów na koncie w poprzednim tygodniu </h1>";
                htmlContent += $"<h2>Suma wpływów: <b>{SumaWplywowTyg(false)}</b></h2>";
                htmlContent += $"<h2>Suma wydatków: <b>{SumaWydatkowTyg(false)}</b></h2>";
                htmlContent += $"<h2>Suma środków przeznaczonych na oszczędności: <b>{SumaOszczednosciTyg(false)}</b></h2>";
            }
            htmlContent += "</body></html>";

            File.WriteAllText(nazwaPliku, htmlContent);
        }

        public decimal SumaWplywowMies(string rokmies)
        {
            decimal suma = WplywyGotowka.Where(WplywRaz => WplywRaz.Data.ToString("yyyyMM") == rokmies).Sum(WplywRaz => WplywRaz.Kwota);
            suma += ListaKont.Sum(Konto => Konto.SumaWplywowMies(rokmies));
            return suma;
        }

        public decimal SumaWydatkowMies(string rokmies)
        {
            decimal suma = WydatkiGotowka.Where(WydatekRaz => WydatekRaz.Data.ToString("yyyyMM") == rokmies).Sum(WydatekRaz => WydatekRaz.Kwota);
            suma += ListaKont.Sum(Konto => Konto.SumaWydatkowMies(rokmies));
            return suma;
        }

        public decimal SumaOszczednosciMies(string rokmies)
        {
            decimal suma = ListaKont.Sum(Konto => Konto.SumaOszczednosciMies(rokmies));
            suma += OszczednosciWGotowce.Where(Oszczednosc => Oszczednosc.Data.ToString("yyyyMM") == rokmies).Sum(Oszczednosc => Oszczednosc.Kwota);
            return suma;
        }

        public decimal SumaWplywowRok(string rok)
        {
            decimal suma = ListaKont.Sum(Konto => Konto.SumaWplywowRok(rok));
            suma += WplywyGotowka.Where(WplywRaz => WplywRaz.Data.ToString("yyyy") == rok).Sum(WplywRaz => WplywRaz.Kwota);
            return suma;
        }

        public decimal SumaWydatkowRok(string rok)
        {
            decimal suma = ListaKont.Sum(Konto => Konto.SumaWydatkowRok(rok));
            suma += WydatkiGotowka.Where(WydatekRaz => WydatekRaz.Data.ToString("yyyy") == rok).Sum(WydatekRaz => WydatekRaz.Kwota);
            return suma;
        }

        public decimal SumaOszczednosciRok(string rok)
        {
            decimal suma = ListaKont.Sum(Konto => Konto.SumaOszczednosciRok(rok));
            suma += OszczednosciWGotowce.Where(Oszczednosc => Oszczednosc.Data.ToString("yyyy") == rok).Sum(Oszczednosc => Oszczednosc.Kwota);
            return suma;
        }

        public decimal SumaWplywowTyg(bool biezacy)
        {
            decimal suma = ListaKont.Sum(Konto => Konto.SumaWplywowTyg(biezacy));
            //DateTime poniedzialekbiez = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            //DateTime poniedzialekbyly = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
            //if (biezacy)
            //{
              //  suma += WplywyGotowka.Where(WplywRaz => WplywRaz.Data >= poniedzialekbiez).Sum(WplywRaz => WplywRaz.Kwota);
            //}
            //else { suma += WplywyGotowka.Where(WplywRaz => (WplywRaz.Data >= poniedzialekbyly) & (WplywRaz.Data < poniedzialekbiez)).Sum(WplywRaz => WplywRaz.Kwota); }
            return suma;
        }

        public decimal SumaWydatkowTyg(bool biezacy)
        {
            decimal suma = ListaKont.Sum(Konto => Konto.SumaWydatkowTyg(biezacy));
            DateTime poniedzialekbiez = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime poniedzialekbyly = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
            if (biezacy)
            {
                foreach(WydatekRaz w in WydatkiGotowka)
                {
                    if(w.Data >= poniedzialekbiez)
                    {
                        suma += w.Kwota;
                    }
                }
            }
            else { suma += WplywyGotowka.Where(WydatekRaz => (WydatekRaz.Data >= poniedzialekbyly) & (WydatekRaz.Data < poniedzialekbiez)).Sum(WydatekRaz => WydatekRaz.Kwota); }
            return suma;
        }

        public decimal SumaOszczednosciTyg(bool biezacy)
        {
            decimal suma = ListaKont.Sum(Konto => Konto.SumaWydatkowTyg(biezacy));
            DateTime poniedzialekbiez = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime poniedzialekbyly = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
            if (biezacy)
            {
                suma += OszczednosciWGotowce.Where(Oszczednosc => Oszczednosc.Data >= poniedzialekbiez).Sum(Oszczednosc => Oszczednosc.Kwota);
            }
            else { suma += OszczednosciWGotowce.Where(Oszczednosc => (Oszczednosc.Data >= poniedzialekbyly) & (Oszczednosc.Data < poniedzialekbiez)).Sum(Oszczednosc => Oszczednosc.Kwota); }
            return suma;
        }
    }
}
