using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace WPFApp
{
    public partial class UtworzKonto : Window
    {
        public UtworzKontoViewModel ViewModel { get; set; }
        public Uzytkownik ZalogowanyUzytkownik { get; set; }

        public UtworzKonto(Konto noweKonto, Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();
            ZalogowanyUzytkownik = zalogowanyUzytkownik;
            ViewModel = new UtworzKontoViewModel { NoweKonto = noweKonto};
            DataContext = ViewModel;
        }

        private void DodajKonto_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsValid())
            {
                string nazwaKonta = ViewModel.Nazwa;
                string nazwaBanku = ViewModel.NazwaBanku;
                decimal saldoPoczatkowe = ViewModel.StanKonta;

                Konto noweKonto = new Konto(nazwaBanku, saldoPoczatkowe, ZalogowanyUzytkownik, nazwaKonta);

                ZalogowanyUzytkownik.DodajKonto(noweKonto);
                ViewModel.NoweKonto.ZapiszDoBazy();
                this.Close();
            }
            else
            {
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

        public Konto NoweKonto { get; set; }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
}
