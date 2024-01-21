using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace WPFApp
{
    public partial class UtworzKonto : Window
    {
        // Inicjalizacja ViewModel
        public UtworzKontoViewModel ViewModel { get; set; }

        //private Uzytkownik zalogowanyUzytkownik; 

        // Przykładowy zalogowany użytkownik (do dostosowania)
        //private Uzytkownik zalogowanyUzytkownik => new Uzytkownik { IdUzytkownika = 0000, Imie = "ZalogowanyUzytkownik" };
        //public UtworzKonto()
        //{
        //    InitializeComponent();

        //    // Inicjalizacja ViewModel i przypisanie zalogowanego użytkownika
        //    ViewModel = new UtworzKontoViewModel { Uzytkownik = ZalogowanyUzytkownik };
        //    DataContext = ViewModel;

        //    // Przykład ustawienia obecnie zalogowanego użytkownika (do dostosowania)
        //    ViewModel.Uzytkownik = ZalogowanyUzytkownik; // Ustaw swojego zalogowanego użytkownika
        //}

        public UtworzKonto(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();

            // Inicjalizacja ViewModel i przypisanie zalogowanego użytkownika
            ViewModel = new UtworzKontoViewModel { Uzytkownik = zalogowanyUzytkownik };
            DataContext = ViewModel;

            // do usunięcia ?? Przykład ustawienia obecnie zalogowanego użytkownika (do dostosowania)
            // do usunięcia ?? ViewModel.Uzytkownik = zalogowanyUzytkownik; // Ustaw swojego zalogowanego użytkownika
        }

        private void DodajKonto_Click(object sender, RoutedEventArgs e)
        {
            // Dodaj logikę sprawdzającą poprawność danych
            if (ViewModel.IsValid())
            {
                // Pobierz informacje o nowym koncie z pól wejściowych w oknie
                string nazwaKonta = ViewModel.Nazwa;
                string nazwaBanku = ViewModel.NazwaBanku;
                decimal saldoPoczatkowe = ViewModel.StanKonta;

                // Utwórz nowe konto
                Konto noweKonto = new Konto(nazwaBanku, saldoPoczatkowe, ViewModel.Uzytkownik, nazwaKonta);

                // Dodaj konto do listy kont użytkownika
                ViewModel.Uzytkownik.DodajKonto(noweKonto);

                // Zapisz zmiany w bazie danych
                ViewModel.Uzytkownik.ZapiszDoBazy();

                // Zamknij okno
                this.Close();
            }
            else
            {
                // Wyświetl komunikat o błędzie walidacji
                MessageBox.Show("Formularz zawiera błędy. Sprawdź poprawność wprowadzonych danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class UtworzKontoViewModel
    {
        [Required(ErrorMessage = "Pole 'Nazwa' jest wymagane.")]
        public string Nazwa { get; set; }

        [Required(ErrorMessage = "Pole 'NazwaBanku' jest wymagane.")]
        public string NazwaBanku { get; set; }

        [Required(ErrorMessage = "Pole 'StanKonta' jest wymagane.")]
        [Range(0, double.MaxValue, ErrorMessage = "Stan konta musi być liczbą nieujemną.")]
        public decimal StanKonta { get; set; }

        public Uzytkownik Uzytkownik { get; set; }

        public bool IsValid()
        {
            // Wykorzystaj Validator.TryValidateObject do walidacji obiektu
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
