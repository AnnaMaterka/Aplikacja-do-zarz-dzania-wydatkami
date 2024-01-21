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
    /// Logika interakcji dla klasy DodajWplyw.xaml
    /// </summary>
    public partial class DodajWplyw : Window
    {
        public DodajWplywViewModel ViewModel { get; set; }
        public DodajWplyw(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();
            // Inicjalizacja ViewModel i przypisanie zalogowanego użytkownika
            ViewModel = new DodajWplywViewModel { Uzytkownik = zalogowanyUzytkownik };
            DataContext = ViewModel;

            // Pobierz listę kont użytkownika
            var listaKont = zalogowanyUzytkownik.ListaKont;

            // Przypisz listę kont do ListBox
            listBoxKonta.ItemsSource = listaKont;
        }
        private void DodajWplyw_Click(object sender, RoutedEventArgs e)
        {
            // Dodaj logikę sprawdzającą poprawność danych
            if (ViewModel.IsValid())
            {
                // Pobierz informacje o nowym koncie z pól wejściowych w oknie
                string kategoria = ViewModel.Kategoria;
                DateTime dataWplywu = ViewModel.Data;
                decimal kwota = ViewModel.Kwota;
                Konto konto = (Konto)listBoxKonta.SelectedItem;

                // Utwórz nowe konto
                WplywRaz wplyw = new WplywRaz(kwota, dataWplywu, kategoria, konto);

                // Dodaj konto do listy kont użytkownika
                ViewModel.Uzytkownik.DodajWplywGotowki(wplyw);

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
            public string Kategoria { get; set; }

            public Uzytkownik Uzytkownik { get; set; }

            public bool IsValid()
            {
                // Wykorzystaj Validator.TryValidateObject do walidacji obiektu
                return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
            }
        }

    }
}

