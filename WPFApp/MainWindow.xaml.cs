using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Aplikacja_do_zarzadzania_wydatkami;
using Microsoft.VisualBasic;
using ProjektSQL;
using static WPFApp.DodajWplyw;
using static WPFApp.UtworzKonto;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UzytkownikDbContext dc;
        public Uzytkownik zalogowanyUzytkownik;
        private Sesja aktualnaSesja;
        public MainWindow()
        {
            dc = new UzytkownikDbContext();
            InitializeComponent();
            WczytajDane();
        }
        //private void WczytajDane()
        //{
        //    dgUzytkownicy.ItemsSource = dc.Uzytkownicy.ToList();
        //    dgKonta.ItemsSource = dc.Konta.ToList();
        //}
        //private void DodajUzytkownika_Click(object sender, RoutedEventArgs e)
        //{
        //    var nowyUzytkownik = new Uzytkownik { Imie = "Nowy Użytkownik" };
        //    dc.Uzytkownicy.Add(nowyUzytkownik);
        //    dc.SaveChanges();
        //    WczytajDane();
        //}
        //private void DodajKonto_Click(object sender, RoutedEventArgs e)
        //{
        //    var noweKonto = new Konto { Nazwa = "Nowe Konto", StanKonta = 0 };
        //    dc.Konta.Add(noweKonto);
        //    dc.SaveChanges();
        //    WczytajDane();
        //}

        private void WczytajDane()
        {
            if (aktualnaSesja != null && aktualnaSesja.Zalogowany)
            {
                var zalogowanyUzytkownik = dc.Uzytkownicy.Find(aktualnaSesja.IdUzytkownika);
                var kontaUzytkownika = dc.Konta.Where(k => k.Uzytkownik.IdUzytkownika == zalogowanyUzytkownik.IdUzytkownika).ToList();

                dgKonta.ItemsSource = kontaUzytkownika;
                txtSumaPieniedzy.Text = kontaUzytkownika.Sum(k => k.StanKonta).ToString("C");
            }
            else
            {
                dgKonta.ItemsSource = null;
                txtSumaPieniedzy.Text = string.Empty;
            }
        }


        private void Zaloguj(long login)
        {
            zalogowanyUzytkownik = dc.Uzytkownicy.ToList().FirstOrDefault(u => u.Login == login);

            if (zalogowanyUzytkownik != null)
            {
                aktualnaSesja = new Sesja { IdUzytkownika = zalogowanyUzytkownik.IdUzytkownika, Zalogowany = true };
                dc.Sesje.Add(aktualnaSesja);
                dc.SaveChanges();
                //Przełączam na widok zalogowanego użytkownika
                InitialView.Visibility = Visibility.Collapsed;
                LoggedInView.Visibility = Visibility.Visible;
                WczytajDane();
                MessageBox.Show("Pomyślnie zalogowano.");
            }
            else
            {
                MessageBox.Show("Użytkownik o podanym loginie nie istnieje.");
            }
        }

        private void Wyloguj()
        {
            if (aktualnaSesja != null)
            {
                aktualnaSesja.Zalogowany = false;
                dc.SaveChanges();
                aktualnaSesja = null;
                WczytajDane();
                InitialView.Visibility = Visibility.Visible;
                LoggedInView.Visibility = Visibility.Collapsed;
            }
        }

        private void ZarejestrujUzytkownika(string imie)
        {
            //var istniejacyUzytkownik = dc.Uzytkownicy.Any(u => u.Imie == imie);

            if (!string.IsNullOrEmpty(imie))
            {
                var nowyUzytkownik = new Uzytkownik { Imie = imie };
                dc.Uzytkownicy.Add(nowyUzytkownik);
                dc.SaveChanges();
                MessageBox.Show($"Użytkownik {imie} został pomyślnie zarejestrowany.\nLogin użytkownika do logowania: {nowyUzytkownik.Login}");
            }
            else
            {
                MessageBox.Show("Nie podano imienia!");
            }
        }

        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            string login_s = Interaction.InputBox("Podaj swój login:", "Logowanie");
            long.TryParse(login_s, out long login);
            Zaloguj(login);
        }

        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            Wyloguj();
        }

        private void ZarejestrujUzytkownika_Click(object sender, RoutedEventArgs e)
        {
            string imie = Interaction.InputBox("Podaj nowe imię użytkownika:", "Rejestracja");
            ZarejestrujUzytkownika(imie);
        }
        private void DodajKonto_Click(object sender, RoutedEventArgs e)
        {
            UtworzKonto okno = new UtworzKonto(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
            okno.DataContext = new UtworzKontoViewModel { Uzytkownik = zalogowanyUzytkownik };

            //if (result == true)
            //{
            //    //dodajemy konto
            //}
        }
        //private void DodajWplyw_Click(object sender, RoutedEventArgs e)
        //{
        //    DodajWplyw okno = new DodajWplyw(zalogowanyUzytkownik);
        //    bool? result = okno.ShowDialog();
        //    okno.DataContext = new DodajWplywViewModel { Uzytkownik = zalogowanyUzytkownik };
        //}


        //Do obsługi menu - zakładka konta
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Wplywy_Click(object sender, RoutedEventArgs e)
        {
            // Tutaj otwierasz nowe okno Wplyw i przekazujesz zalogowanego użytkownika
            Wplywy wplywyWindow = new Wplywy(zalogowanyUzytkownik);
            wplywyWindow.Show();
        }


        //private void Wplywy_Click(object sender, RoutedEventArgs e)
        //{
        //    WyswietlWplywy();
        //}

        //private void WyswietlWplywy()
        //{
        //    if (zalogowanyUzytkownik != null)
        //    {
        //        // Pobierz wszystkie wpływy z kont użytkownika
        //        var wplywyUzytkownika = zalogowanyUzytkownik.ListaKont
        //            .SelectMany(konto => konto.Wplywy)
        //            .ToList();

        //        listBoxWplywy.ItemsSource = wplywyUzytkownika;
        //    }
        //}
        //private void TabControlWplywy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (tabControlWplywy.SelectedItem != null && tabControlWplywy.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Wpływy")
        //    {
        //        WyswietlWplywy();
        //    }
        //}
    }
}