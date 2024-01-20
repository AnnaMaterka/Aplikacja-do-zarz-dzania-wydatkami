using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    /// Logika interakcji dla klasy UtworzKonto.xaml
    /// </summary>
    public partial class UtworzKonto : Window
    {
        public UtworzKonto()
        {
            InitializeComponent();
            // Inicjalizacja ViewModel
            var viewModel = new UtworzKontoViewModel();
            DataContext = viewModel;

            // Przykład ustawienia obecnie zalogowanego użytkownika (do dostosowania)
            viewModel.Uzytkownik = ZalogowanyUzytkownik; // Ustaw swojego zalogowanego użytkownika
        }

        // Przykładowy zalogowany użytkownik (do dostosowania)
        private Uzytkownik ZalogowanyUzytkownik => new Uzytkownik { IdUzytkownika = 0000, Imie = "ZalogowanyUzytkownik" };
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
    }

}
