using System.Data.Entity;
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

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UzytkownikDbContext dc;
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
            if (_aktualnaSesja != null && _aktualnaSesja.Zalogowany)
            {
                var zalogowanyUzytkownik = dc.Uzytkownicy.Find(_aktualnaSesja.IdUzytkownika);
                var kontaUzytkownika = dc.Konta.Where(k => k.IdUzytkownika == zalogowanyUzytkownik.IdUzytkownika).ToList();

                dgKonta.ItemsSource = kontaUzytkownika;
                txtSumaPieniedzy.Text = kontaUzytkownika.Sum(k => k.StanKonta).ToString("C");
            }
            else
            {
                dgKonta.ItemsSource = null;
                txtSumaPieniedzy.Text = string.Empty;
            }
        }

        private Sesja _aktualnaSesja;

        private void Zaloguj(string imie)
        {
            var uzytkownik = dc.Uzytkownicy.FirstOrDefault(u => u.Imie == imie);

            if (uzytkownik != null)
            {
                _aktualnaSesja = new Sesja { IdUzytkownika = uzytkownik.IdUzytkownika, Zalogowany = true };
                dc.Sesje.Add(_aktualnaSesja);
                dc.SaveChanges();
                WczytajDane();
            }
            else
            {
                MessageBox.Show("Użytkownik o podanym imieniu nie istnieje.");
            }
        }

        private void Wyloguj()
        {
            if (_aktualnaSesja != null)
            {
                _aktualnaSesja.Zalogowany = false;
                dc.SaveChanges();
                _aktualnaSesja = null;
                WczytajDane();
            }
        }

        private void ZarejestrujUzytkownika(string imie)
        {
            var istniejacyUzytkownik = dc.Uzytkownicy.Any(u => u.Imie == imie);

            if (!istniejacyUzytkownik)
            {
                var nowyUzytkownik = new Uzytkownik { Imie = imie };
                dc.Uzytkownicy.Add(nowyUzytkownik);
                dc.SaveChanges();
                MessageBox.Show($"Użytkownik {imie} został pomyślnie zarejestrowany.");
            }
            else
            {
                MessageBox.Show("Użytkownik o podanym imieniu już istnieje.");
            }
        }
        private void Zaloguj_Click(object sender, RoutedEventArgs e)
        {
            string imie = Interaction.InputBox("Podaj swoje imię:", "Logowanie");
            Zaloguj(imie);
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

    }
}