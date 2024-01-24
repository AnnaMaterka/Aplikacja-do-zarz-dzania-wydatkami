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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Logika interakcji dla klasy DodajWplywStaly.xaml
    /// </summary>
    /// 
    public partial class DodajWplywStaly : Window
    {
        private UzytkownikDbContext db;
        public Uzytkownik ZalogowanyUzytkownik { get; set; }

        public decimal Kwota { get; set; }
        public DateTime Data { get; set; }
        //public Kategoria WybranaKategoria { get; set; }
        public string WpisanaKategoria { get; set; }
        public Konto WybraneKonto { get; set; }
        public Cykl WybranyCykl { get; set; }


        public DodajWplywStaly(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();
            db = new UzytkownikDbContext();
            var listaKont = zalogowanyUzytkownik.ListaKont;

            // Przypisz listę kont do ListBox
            cbKonta.ItemsSource = listaKont;
        }

        private void DodajWplywStaly_Click(object sender, RoutedEventArgs e)
        {
            decimal kwota = 0;
            decimal.TryParse(txtKwota.Text, out kwota);
            Kwota = kwota;
            Konto selectedKonto = (Konto)cbKonta.SelectedItem;
            selectedKonto.StanKonta += Kwota;

            //Kategoria selectedKategoria = (Kategoria)cbKategorie.SelectedItem;
            WpisanaKategoria = txtKategoria.Text;
            WybraneKonto = selectedKonto;
            Data = (DateTime)datePickerData.SelectedDate;
            WplywStaly wplyw = new WplywStaly(Kwota, Data, WpisanaKategoria, ZalogowanyUzytkownik, WybraneKonto, WybranyCykl);
            //WybraneKonto.NowyWplywKonto(wplyw);
            WybraneKonto.ZapiszDoBazy();
            this.Close();
        }
    }
}
