using Aplikacja_do_zarzadzania_wydatkami;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Logika interakcji dla klasy DodajWplyw.xaml
    /// </summary>
    public partial class DodajWplyw : Window
    {
        private UzytkownikDbContext db;
        public Uzytkownik ZalogowanyUzytkownik { get; set; }

        public decimal Kwota { get; set; }
        public DateTime Data { get; set; }
        public string WpisanaKategoria { get; set; }
        public Konto WybraneKonto { get; set; }
        public WplywRaz NowyWplyw { get; set; }

        public DodajWplyw(Uzytkownik zalogowanyUzytkownik)
        {
            InitializeComponent();
            db = new UzytkownikDbContext();
            this.ZalogowanyUzytkownik = zalogowanyUzytkownik;

            var listaKont = ZalogowanyUzytkownik.ListaKont;
            cbKonta.ItemsSource = listaKont;
        }

        private void DodajWplyw_Click(object sender, RoutedEventArgs e)
        {
            // Dodaj logikę sprawdzającą poprawność danych
            if (IsValid())
            {
                decimal kwota = 0;
                decimal.TryParse(txtKwota.Text, out kwota);
                Kwota = kwota;
                Konto selectedKonto = (Konto)cbKonta.SelectedItem;
                selectedKonto.StanKonta += Kwota;

                WpisanaKategoria = txtKategoria.Text;
                WybraneKonto = selectedKonto;
                Data = (DateTime)datePickerData.SelectedDate;
                NowyWplyw = new WplywRaz(Kwota, Data, WpisanaKategoria, ZalogowanyUzytkownik, WybraneKonto);
                WybraneKonto.NowyWplywKonto(NowyWplyw);
                WybraneKonto.ZapiszDoBazy();
                db.SaveChanges();
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
