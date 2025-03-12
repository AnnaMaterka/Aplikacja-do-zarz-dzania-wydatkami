using Aplikacja_do_zarzadzania_wydatkami;
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
    /// Logika interakcji dla klasy DodajWydatekStaly.xaml
    /// </summary>
    public partial class DodajWydatekStaly : Window
    {
        private UzytkownikDbContext db;
        public Uzytkownik ZalogowanyUzytkownik { get; set; }

        public decimal Kwota { get; set; }
        public DateTime Data { get; set; }
        public string WpisanaKategoria { get; set; }
        public Konto WybraneKonto { get; set; }
        public Cykl WybranyCykl { get; set; }


        public DodajWydatekStaly(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();
            db = new UzytkownikDbContext();
            this.ZalogowanyUzytkownik = zalogowanyUzytkownik;
            var listaKont = ZalogowanyUzytkownik.ListaKont;
            cbKonta.ItemsSource = listaKont;
            cbCykl.ItemsSource = Enum.GetValues(typeof(Cykl));
        }

        private void DodajWydatekStaly_Click(object sender, RoutedEventArgs e)
        {
            decimal kwota = 0;
            decimal.TryParse(txtKwota.Text, out kwota);
            Kwota = kwota;
            WybraneKonto = (Konto)cbKonta.SelectedItem;
            WybraneKonto.StanKonta += Kwota;
            WybranyCykl = (Cykl)cbCykl.SelectedItem;
            WpisanaKategoria = txtKategoria.Text;
            Data = (DateTime)datePickerData.SelectedDate;
            WydatekStaly wydatek = new WydatekStaly(Kwota, Data, WpisanaKategoria, ZalogowanyUzytkownik, WybraneKonto, WybranyCykl);
            WybraneKonto.NowyWydatekStaly(wydatek);
            WybraneKonto.ZapiszDoBazy();
            this.Close();
        }
    }
}
