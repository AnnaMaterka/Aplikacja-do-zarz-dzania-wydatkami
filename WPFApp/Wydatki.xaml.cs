using Aplikacja_do_zarzadzania_wydatkami;
using ProjektSQL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WPFApp
{
    /// <summary>
    /// Logika interakcji dla klasy Wydatki.xaml
    /// </summary>
    public partial class Wydatki : Window
    {
        public Uzytkownik zalogowanyUzytkownik;
        private Sesja aktualnaSesja;
        private UzytkownikDbContext dc;
        public Wydatki(Uzytkownik zalogowanyUzytkownik, UzytkownikDbContext dc)
        {
            InitializeComponent();
            this.zalogowanyUzytkownik = zalogowanyUzytkownik; 
            this.dc = dc;
            WyswietlWydatki();
        }
        private void DodajWydatek_Click(object sender, RoutedEventArgs e)
        {
            DodajWydatek okno = new DodajWydatek(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
            dc.SaveChanges();
            WyswietlWydatki();
        }
        private void DodajWydatekStaly_Click(object sender, RoutedEventArgs e)
        {
            DodajWydatekStaly okno = new DodajWydatekStaly(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
            dc.SaveChanges();
            WyswietlWydatki();
        }
        private void WyswietlWydatki()
        {
            if (zalogowanyUzytkownik != null)
            {
                var kontaUzytkownika = dc.Konta.Where(k => k.Uzytkownik.IdUzytkownika == zalogowanyUzytkownik.IdUzytkownika).ToList();
                BindingList<Wydatek> wszystkieWydatki = new BindingList<Wydatek>();
                //ObservableCollection<Wydatek> wszystkieWydatki = new ObservableCollection<Wydatek>();
                foreach (var konto in kontaUzytkownika)
                {
                    var wydatki = dc.WydatkiRaz.Where(w => w.IdKonta == konto.IdKonta).ToList();
                    foreach (var wydatek in wydatki)
                    {
                        wszystkieWydatki.Add(wydatek);
                    }
                    var wydatkiStale = dc.WydatkiStale.Where(w => w.IdKonta == konto.IdKonta).ToList();
                    foreach (var wydatek in wydatkiStale)
                    {
                        wszystkieWydatki.Add(wydatek);
                    }
                }
                //wszystkieWydatki = new ObservableCollection<Wydatek>(wszystkieWydatki.OrderBy(w => w.Data));
                WydatkiDataGrid.ItemsSource = new BindingList<Wydatek>(wszystkieWydatki);
                //WydatkiDataGrid.ItemsSource = wszystkieWydatki;
            }
        }
    }
}
