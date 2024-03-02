using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Design;
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
            Uri iconUri = new Uri("pack://application:,,,/WPFApp;component/Assets/portfel.ico", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            WczytajDane();
        }

        private void WczytajDane()
        {
            if (aktualnaSesja != null && aktualnaSesja.Zalogowany)
            {
                var zalogowanyUzytkownik = dc.Uzytkownicy.Find(aktualnaSesja.IdUzytkownika);
                var kontaUzytkownika = dc.Konta.Where(k => k.Uzytkownik.IdUzytkownika == zalogowanyUzytkownik.IdUzytkownika).ToList();

                dgKont.ItemsSource = kontaUzytkownika;
                txtSumaPieniedzy.Text = kontaUzytkownika.Sum(k => k.StanKonta).ToString("C");
            }
            else
            {
                dgKont.ItemsSource = null;
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
                WidokKont.Visibility = Visibility.Collapsed;
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
            Konto noweKonto = new Konto();
            UtworzKonto okno = new UtworzKonto(noweKonto, zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                zalogowanyUzytkownik.DodajKonto(noweKonto);
                dgKont.ItemsSource = new ObservableCollection<Konto>(zalogowanyUzytkownik.ListaKont);
            }
            okno.DataContext = new UtworzKontoViewModel { NoweKonto = noweKonto };
        }

        private void WyswietlanieKont_Click(object sender, RoutedEventArgs e)
        {
            InitialView.Visibility = Visibility.Collapsed;
            LoggedInView.Visibility = Visibility.Collapsed;
            WidokKont.Visibility = Visibility.Visible;
            WyswietlanieKont();
        }
        private void WyswietlanieKont()
        {
            if (zalogowanyUzytkownik != null)
            {
                var kontaUzytkownika = dc.Konta.Where(k => k.Uzytkownik.IdUzytkownika == zalogowanyUzytkownik.IdUzytkownika).ToList();
                dgKont.ItemsSource = kontaUzytkownika;
            }
        }
        private void Zamknij_Click(object sender, RoutedEventArgs e)
        {
            InitialView.Visibility = Visibility.Collapsed;
            LoggedInView.Visibility = Visibility.Visible;
            WidokKont.Visibility = Visibility.Collapsed;
        }
        private void Wplywy_Click(object sender, RoutedEventArgs e)
        {
            Wplywy wplywyWindow = new Wplywy(zalogowanyUzytkownik, dc);
            wplywyWindow.Show();
        }
        
        private void Wydatki_Click(object sender, RoutedEventArgs e)
        {
            Wydatki wydatkiWindow = new Wydatki(zalogowanyUzytkownik, dc);
            wydatkiWindow.Show();
        }
        private void RaportRoczny_Click(object sender, RoutedEventArgs e)
        {
            string roks = Interaction.InputBox("Wybierz rok", "Wybór roku");
            int.TryParse(roks, out int rok);
            if (rok < 1 | rok > DateTime.Today.Year) { MessageBox.Show("Nieprawidłowy rok!"); return; }
            Raport raport = new Raport(zalogowanyUzytkownik, "R", roks);
            bool? result = raport.ShowDialog();
        }

        private void RaportMiesieczny_Click(object sender, RoutedEventArgs e)
        {
            WyborMiesiacaiRoku w = new WyborMiesiacaiRoku();
            bool? result = w.ShowDialog();
            if (result == true)
            {
                Raport raport = new Raport(zalogowanyUzytkownik, "M", w.rokmies);
                bool? result1 = raport.ShowDialog();
            }
        }

        private void RaportTygodniowy_Click(object sender, RoutedEventArgs e)
        { 
            WyborTygodnia w = new WyborTygodnia();
            bool? result = w.ShowDialog();
            if(result == true)
            {
                if (w.biezacy)
                {
                    Raport raport = new Raport(zalogowanyUzytkownik, "TB", null);
                    raport.ShowDialog();
                }
                else
                {
                    Raport raport = new Raport(zalogowanyUzytkownik, "TP", null);
                    raport.ShowDialog();
                }
            }

        }
        private void Aktualizuj_Click(object sender, RoutedEventArgs e)
        {
            dgKont.ItemsSource = new ObservableCollection<Konto>(zalogowanyUzytkownik.ListaKont);
        }
    }
}