using Aplikacja_do_zarzadzania_wydatkami;
using ProjektSQL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
using static WPFApp.DodajWplyw;

namespace WPFApp
{
    /// <summary>
    /// Logika interakcji dla klasy Wplywy.xaml
    /// </summary>
    /// 
    public partial class Wplywy : Window, INotifyPropertyChanged
    {
        public Uzytkownik zalogowanyUzytkownik;
        //public Konto aktualneKonto;
        //private Sesja aktualnaSesja;
        private UzytkownikDbContext dc;
        private ObservableCollection<Wplyw> wszystkieWplywy = new ObservableCollection<Wplyw>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Wplyw> WszystkieWplywy 
        { 
            get => wszystkieWplywy;
            set 
            { 
                if(wszystkieWplywy != value)
                {
                    wszystkieWplywy = value;
                    OnPropertyChanged(nameof(WszystkieWplywy));
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Wplywy(Uzytkownik zalogowanyUzytkownik, UzytkownikDbContext dc)
        {
            InitializeComponent();
            this.zalogowanyUzytkownik = zalogowanyUzytkownik;
            this.dc = dc;
            WyswietlWplywy();
        }

        private void DodajWplyw_Click(object sender, RoutedEventArgs e)
        {
            WplywRaz nowyWplyw = new WplywRaz();
            DodajWplyw okno = new DodajWplyw(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                dc.SaveChanges();
                WyswietlWplywy();
            }
        }
        private void DodajWplywStaly_Click(object sender, RoutedEventArgs e)
        {
            DodajWplywStaly okno = new DodajWplywStaly(zalogowanyUzytkownik);
            bool? result = okno.ShowDialog();
            if (result == true)
            {
                dc.SaveChanges();
                WyswietlWplywy();
            }
        }
        private void WyswietlWplywy()
        {
            WplywyDataGrid.ItemsSource = null;
            if (zalogowanyUzytkownik != null)
            {
                var kontaUzytkownika = dc.Konta.Where(k => k.Uzytkownik.IdUzytkownika == zalogowanyUzytkownik.IdUzytkownika).ToList();
                foreach (var konto in kontaUzytkownika)
                {
                    var wplywyDlaKonta = dc.WplywyRaz.Where(w => w.IdKonta == konto.IdKonta).ToList();
                    foreach (var wplyw in wplywyDlaKonta)
                    {
                        WszystkieWplywy.Add(wplyw);
                    }
                    var wplywySDlaKonta = dc.WplywyStale.Where(w => w.IdKonta == konto.IdKonta).ToList();
                    foreach (var wplyw in wplywySDlaKonta)
                    {
                        WszystkieWplywy.Add(wplyw);
                    }
                }
                WplywyDataGrid.ItemsSource = new ObservableCollection<Wplyw>(WszystkieWplywy.OrderBy(w => w.Data));

            }

        }
    }
}
