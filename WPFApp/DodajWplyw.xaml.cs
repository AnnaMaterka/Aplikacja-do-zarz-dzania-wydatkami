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
using System.Windows.Markup;
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
            ViewModel = new DodajWplywViewModel
            {
                Uzytkownik = zalogowanyUzytkownik,
                Konto = zalogowanyUzytkownik.ListaKont.FirstOrDefault(),
                Kategoria = db.Kategorie.FirstOrDefault()
            };
            DataContext = ViewModel;
            // Pobierz listę kont użytkownika
            var listaKont = ViewModel.Uzytkownik.ListaKont;

            // Przypisz listę kont do ListBox
            cbKonta.ItemsSource = listaKont;
            cbKategorie.ItemsSource = db.Kategorie.ToList();
            cbKategorie.SelectedItem = ViewModel.Kategoria;
        }

        private void DodajWplyw_Click(object sender, RoutedEventArgs e)
        {
            // Dodaj logikę sprawdzającą poprawność danych
            if (ViewModel.IsValid())
            {
                Konto selectedKonto = (Konto)cbKonta.SelectedItem;
                selectedKonto.StanKonta += ViewModel.Kwota;
                Kategoria selectedKategoria = (Kategoria)cbKategorie.SelectedItem;
                WplywRaz wplyw = new WplywRaz(ViewModel.Kwota, ViewModel.Data, selectedKategoria, ViewModel.Uzytkownik, ViewModel.Konto);
                ViewModel.Konto.NowyWplywKonto(wplyw);
                ViewModel.Konto.ZapiszDoBazy();
                this.Close();
            }
            else
            {
                // Wyświetl komunikat o błędzie walidacji
                MessageBox.Show("Formularz zawiera błędy. Sprawdź poprawność wprowadzonych danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        public Kategoria? Kategoria { get; set; }
        [Required(ErrorMessage = "Pole 'Konto' jest wymagane.")]
        public Konto? Konto { get; set; }

        public Uzytkownik? Uzytkownik { get; set; }


        public bool IsValid()
        {
            // Wykorzystaj Validator.TryValidateObject do walidacji obiektu
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }
    }

}

