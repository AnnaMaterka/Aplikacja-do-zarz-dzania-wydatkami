using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logika interakcji dla klasy DodajWydatek.xaml
    /// </summary>
    public partial class DodajWydatek : Window
    {
        private UzytkownikDbContext db;
        public Uzytkownik ZalogowanyUzytkownik { get; set; }

        public decimal Kwota { get; set; }
        public DateTime Data { get; set; }
        public string WpisanaKategoria { get; set; }
        public Konto WybraneKonto { get; set; }

        public DodajWydatek(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();

            db = new UzytkownikDbContext();
            this.ZalogowanyUzytkownik = zalogowanyUzytkownik;

            cbKonta.ItemsSource = new BindingList<Konto>(ZalogowanyUzytkownik.ListaKont);
        }
        private void DodajWydatek_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                decimal.TryParse(txtKwota.Text, out decimal kwota);
                Kwota = kwota;
                Konto selectedKonto = (Konto)cbKonta.SelectedItem;
                selectedKonto.StanKonta -= Kwota;

                WpisanaKategoria = txtKategoria.Text;
                WybraneKonto = selectedKonto;
                Data = (DateTime)datePickerData.SelectedDate;
                WydatekRaz wydatek = new WydatekRaz(Kwota, Data, WpisanaKategoria, ZalogowanyUzytkownik, WybraneKonto);
                WybraneKonto.NowyWydatekKonto(wydatek);
                WybraneKonto.ZapiszDoBazy();
                this.Close();
            }
            else
            {
                MessageBox.Show("Formularz zawiera błędy. Sprawdź poprawność wprowadzonych danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool IsValid()
        {
            if (Kwota < 0)
                return false;

            if (txtKategoria is null)
                return false;

            if (cbKonta.SelectedIndex < 0)
                return false;

            return true;
        }
    }
}
