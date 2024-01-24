using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy DodajKategorie.xaml
    /// </summary>
    public partial class DodajKategorie : Window
    {
        public DodajKategorie()
        {
            InitializeComponent();
        }
        private void DodajKategorie_Button(object sender, RoutedEventArgs e)
        {
            string nazwaKategorii = txtNazwaKategorii.Text;

            if (!string.IsNullOrEmpty(nazwaKategorii))
            {
                // Tworzenie nowej kategorii i dodanie jej do bazy danych lub innej struktury danych
                Kategoria nowaKategoria = new Kategoria(nazwaKategorii);
                DodajKategorieDoBazyDanych(nowaKategoria);

                // Możesz także zaktualizować interfejs użytkownika lub wyświetlić potwierdzenie
                MessageBox.Show("Kategoria dodana pomyślnie!");
            }
            else
            {
                MessageBox.Show("Wprowadź nazwę kategorii.");
            }
        }
        private void DodajKategorieDoBazyDanych(Kategoria kategoria)
        {
            using (var context = new UzytkownikDbContext())
            {
                // Sprawdź, czy kategoria o tej nazwie już istnieje
                var istniejacaKategoria = context.Kategorie.FirstOrDefault(k => k.NazwaKategorii == kategoria.NazwaKategorii);

                if (istniejacaKategoria == null)
                {
                    // Kategoria nie istnieje, więc możemy ją dodać
                    context.Kategorie.Add(kategoria);
                }
                else
                {
                    // Kategoria już istnieje, możesz obsłużyć to w dowolny sposób, na przykład zaktualizować
                    // nazwę kategorii lub podjąć inne działania
                    istniejacaKategoria.NazwaKategorii = kategoria.NazwaKategorii;
                    context.Entry(istniejacaKategoria).State = EntityState.Modified;
                }

                // Zapisz zmiany w bazie danych
                context.SaveChanges();
            }

        }
    }
}
