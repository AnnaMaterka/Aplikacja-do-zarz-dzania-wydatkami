using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Logika interakcji dla klasy Raport.xaml
    /// </summary>
    public partial class Raport : Window
    {
        public Raport(Uzytkownik zalogowanyUzytkownik, string typ, string okres)
        {
            InitializeComponent();
            switch (typ)
            {
                case "R": 
                    lblTytul.Content = "Raport roczny";
                    lblDotyczy.Content = $"Dotyczy roku {okres}";
                    txtSumaWplywow.Text = zalogowanyUzytkownik.SumaWplywowRok(okres).ToString();
                    txtSumaWydatkow.Text = zalogowanyUzytkownik.SumaWydatkowRok(okres).ToString();
                    txtSumaOszczednosci.Text = zalogowanyUzytkownik.SumaOszczednosciRok(okres).ToString();
                    break;
                case "M":
                    lblTytul.Content = "Raport miesięczny";
                    DateTime data = DateTime.ParseExact(okres, "yyyyMM", CultureInfo.InvariantCulture);
                    string miesiac = data.ToString("MMMM", new CultureInfo("pl-PL"));
                    string rok = data.Year.ToString();
                    lblDotyczy.Content = $"Dotyczy miesiąca {miesiac} roku {rok}";
                    txtSumaWplywow.Text = zalogowanyUzytkownik.SumaWplywowMies(okres).ToString();
                    txtSumaWydatkow.Text = zalogowanyUzytkownik.SumaWydatkowMies(okres).ToString();
                    txtSumaOszczednosci.Text = zalogowanyUzytkownik.SumaOszczednosciMies(okres).ToString();
                    break;
                case "TB":
                    lblTytul.Content = "Raport tygodniowy";
                    lblDotyczy.Content = "Dotyczy bieżącego tygodnia";
                    break;
                case "TP":
                    lblTytul.Content = "Raport tygodniowy";
                    lblDotyczy.Content = "Dotyczy poprzedniego tygodnia";
                    break;

            }
        }
    }
}
