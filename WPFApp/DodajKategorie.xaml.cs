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
        }
            
        
    }
}
