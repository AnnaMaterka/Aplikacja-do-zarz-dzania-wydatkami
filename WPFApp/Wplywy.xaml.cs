using Aplikacja_do_zarzadzania_wydatkami;
using ProjektSQL;
using System;
using System.Collections.Generic;
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
using static WPFApp.DodajWplyw;

namespace WPFApp
{
    /// <summary>
    /// Logika interakcji dla klasy Wplywy.xaml
    /// </summary>
    public partial class Wplywy : Window
    {
        public Uzytkownik zalogowanyUzytkownik;
        private Sesja aktualnaSesja;
        private UzytkownikDbContext dc;


        public Wplywy(Uzytkownik zalogowanyUzytkownik)
        {
            this.zalogowanyUzytkownik = zalogowanyUzytkownik; // Przekazanie zalogowanego użytkownika
            dc = new UzytkownikDbContext();
            InitializeComponent();
            WyswietlWplywy();
        }

        private void DodajWplyw_Click(object sender, RoutedEventArgs e)
        {
            DodajWplyw okno = new DodajWplyw(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
        }

        private void WyswietlWplywy()
        {
            if (zalogowanyUzytkownik != null)
            {
                // Pobierz wszystkie wpływy z kont użytkownika
                var wplywyUzytkownika = zalogowanyUzytkownik.ListaKont
                    .SelectMany(konto => konto.Wplywy)
                    .ToList();

                listBoxWplywy.ItemsSource = wplywyUzytkownika;
            }
        }
    }
}
