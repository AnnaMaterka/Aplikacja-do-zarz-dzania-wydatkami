using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace WPFApp
{
    /// <summary>
    /// Logika interakcji dla klasy DodajWplyw.xaml
    /// </summary>
    public partial class DodajWplyw : Window
    {
        private UzytkownikDbContext db;
        public DodajWplywViewModel ViewModel { get; set; }
        public DodajWplyw(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();
            db = new UzytkownikDbContext();
            // Inicjalizacja ViewModel i przypisanie zalogowanego użytkownika
            ViewModel = new DodajWplywViewModel { Uzytkownik = zalogowanyUzytkownik };
            DataContext = ViewModel;

            // Pobierz listę kont użytkownika
            var listaKont = zalogowanyUzytkownik.ListaKont;

            // Przypisz listę kont do ListBox
            cbKonta.ItemsSource = listaKont;
            cbKategorie.ItemsSource = db.Kategorie.ToList();
        }
        private void DodajWplyw_Click(object sender, RoutedEventArgs e)
        {
            // Dodaj logikę sprawdzającą poprawność danych
            if (ViewModel.IsValid())
            {
                // Pobierz informacje o nowym koncie z pól wejściowych w oknie
                Kategoria kategoria = (Kategoria)cbKategorie.SelectedItem;
                DateTime dataWplywu = ViewModel.Data;
                decimal kwota = ViewModel.Kwota;
                Konto konto = (Konto)cbKonta.SelectedItem;

                konto.NowyWplywKonto(kwota, dataWplywu, kategoria);
                
                // Zamknij okno
                this.Close();
            }
            else
            {
                // Wyświetl komunikat o błędzie walidacji
                MessageBox.Show("Formularz zawiera błędy. Sprawdź poprawność wprowadzonych danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public class DodajWplywViewModel
        {
            [Required(ErrorMessage = "Pole 'Kwota' jest wymagane.")]
            [Range(0, double.MaxValue, ErrorMessage = "Kwota musi być liczbą nieujemną.")]
            public decimal Kwota { get; set; }

            [Required(ErrorMessage = "Pole 'Data' jest wymagane.")]
            public DateTime Data { get; set; }

            [Required(ErrorMessage = "Pole 'Kategoria' jest wymagane.")]
            public Kategoria Kategoria { get; set; }

            public Uzytkownik Uzytkownik { get; set; }

            public bool IsValid()
            {
                // Wykorzystaj Validator.TryValidateObject do walidacji obiektu
                return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
            }
        }
        public List<string> PobierzNazwyKategorii()
        {
            // Pobierz wszystkie kategorie z bazy danych
            List<Kategoria> wszystkieKategorie = db.Kategorie.ToList();

            // Utwórz listę nazw kategorii
            List<string> nazwyKategorii = wszystkieKategorie.Select(k => k.NazwaKategorii).ToList();

            return nazwyKategorii;
        }
    }
}

