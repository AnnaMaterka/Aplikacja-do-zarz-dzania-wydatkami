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
    /// Logika interakcji dla klasy DodajWplywStaly.xaml
    /// </summary>
    /// 
    public class DodajWplywStalyViewModel
    {
        public Uzytkownik ZalogowanyUzytkownik { get; set; }

        [Required(ErrorMessage = "Pole 'Kwota' jest wymagane.")]
        [Range(0, double.MaxValue, ErrorMessage = "Kwota musi być liczbą nieujemną.")]
        public decimal Kwota { get; set; }

        [Required(ErrorMessage = "Pole 'Data początkowa' jest wymagane.")]
        public DateTime DataPoczatkowa { get; set; }

        [Required(ErrorMessage = "Pole 'Kategoria' jest wymagane.")]
        public string Kategoria { get; set; }

        public bool IsValid()
        {
            // Wykorzystaj Validator.TryValidateObject do walidacji obiektu
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }
    public partial class DodajWplywStaly : Window
    {
        private UzytkownikDbContext db;
        public DodajWplywStalyViewModel ViewModel { get; set; }

        public DodajWplywStaly(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();
            db = new UzytkownikDbContext();
            ViewModel = new DodajWplywStalyViewModel { ZalogowanyUzytkownik = zalogowanyUzytkownik };
            DataContext = ViewModel;
        }

        private void DodajWplywStaly_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsValid())
            {
                // Dodaj logikę dodawania wpływu stałego do bazy danych
                // Użyj właściwości ViewModel.Kwota, ViewModel.DataPoczatkowa, ViewModel.Kategoria
                // oraz ViewModel.ZalogowanyUzytkownik do utworzenia obiektu WplywStaly
                // i dodania go do bazy danych

                MessageBox.Show("Dodano wpływ stały.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Formularz zawiera błędy. Sprawdź poprawność wprowadzonych danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
