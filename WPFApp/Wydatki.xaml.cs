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

namespace WPFApp
{
    /// <summary>
    /// Logika interakcji dla klasy Wydatki.xaml
    /// </summary>
    public partial class Wydatki : Window
    {
        public Uzytkownik zalogowanyUzytkownik;
        //public Konto aktualneKonto;
        private Sesja aktualnaSesja;
        private UzytkownikDbContext dc;
        public Wydatki(Uzytkownik zalogowanyUzytkownik, UzytkownikDbContext dc)
        {
            InitializeComponent();
            this.zalogowanyUzytkownik = zalogowanyUzytkownik; // Przekazanie zalogowanego użytkownika
            this.dc = dc;
        }
        private void DodajWydatek_Click(object sender, RoutedEventArgs e)
        {
            DodajWydatek okno = new DodajWydatek(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
        }
        private void DodajWydatekStaly_Click(object sender, RoutedEventArgs e)
        {
            DodajWydatekStaly okno = new DodajWydatekStaly(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
        }
        private void Zapisz()
        {
            if (aktualnaSesja != null)
            {
                dc.SaveChanges();
            }
        }
        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            Zapisz();
        }
    }
}
