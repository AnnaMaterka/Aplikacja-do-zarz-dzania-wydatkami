using Aplikacja_do_zarzadzania_wydatkami;
using ProjektSQL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WPFApp.DodajWplyw;

namespace WPFApp
{
    /// <summary>
    /// Logika interakcji dla klasy Wplywy.xaml
    /// </summary>
    public partial class Wplywy : Window
    {
        public Uzytkownik zalogowanyUzytkownik;
        //public Konto aktualneKonto;
        private Sesja aktualnaSesja;
        private UzytkownikDbContext dc;

        public Wplywy(Uzytkownik zalogowanyUzytkownik, UzytkownikDbContext dc)
        {
            InitializeComponent();
            this.zalogowanyUzytkownik = zalogowanyUzytkownik; // Przekazanie zalogowanego użytkownika
            this.dc = dc;
            
            WyswietlWplywy();
        }

        private void DodajWplyw_Click(object sender, RoutedEventArgs e)
        {
            DodajWplyw okno = new DodajWplyw(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
        }
        private void DodajWplywStaly_Click(object sender, RoutedEventArgs e)
        {
            DodajWplywStaly okno = new DodajWplywStaly(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
        }
            private void Zapisz()
        {
            if (aktualnaSesja != null)
            {
                dc.SaveChanges();
            }
        }
        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            Zapisz();
        }
        private void WyswietlWplywy()
        {
            if (zalogowanyUzytkownik != null)
            {
                var kontaUzytkownika = dc.Konta.Where(k => k.Uzytkownik.IdUzytkownika == zalogowanyUzytkownik.IdUzytkownika).ToList();
                ObservableCollection<Wplyw> wszystkieWplywy = new ObservableCollection<Wplyw>();
                foreach (var konto in kontaUzytkownika)
                {
                    var wplywyDlaKonta = dc.Wplywy.Where(w => w.IdKonta == konto.IdKonta).ToList();
                    foreach (var wplyw in wplywyDlaKonta)
                    {
                        wszystkieWplywy.Add(wplyw);
                    }
                    var wplywySDlaKonta = dc.WplywyStale.Where(w => w.IdKonta == konto.IdKonta).ToList();
                    foreach (var wplyw in wplywySDlaKonta)
                    {
                        wszystkieWplywy.Add(wplyw);
                    }
                }
                wszystkieWplywy = new ObservableCollection<Wplyw>(wszystkieWplywy.OrderBy(w => w.Data));

                WplywyDataGrid.ItemsSource = wszystkieWplywy;
            }
            //if(zalogowanyUzytkownik != null)
            //{
            //    //var wplywy = dc.Konta.SelectMany(k => k.Wplywy).ToList();
            //    var wplywy = dc.Wplywy.Where(w => dc.Konta.Any(k => k.IdKonta == w.IdKonta && k.Uzytkownik.IdUzytkownika == zalogowanyUzytkownik.IdUzytkownika)).ToList();
            //    listBoxWplywy.ItemsSource = wplywy;
            //}
            // Pobierz wszystkie wpływy z kont użytkownika
            //var wplywyUzytkownika = zalogowanyUzytkownik.ListaKont
            //    .SelectMany(konto => konto.Wplywy)
            //    .ToList();

            //listBoxWplywy.ItemsSource = wplywyUzytkownika;

        }

    }
}
