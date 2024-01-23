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
    /// Logika interakcji dla klasy WyborTygodnia.xaml
    /// </summary>
    public partial class WyborTygodnia : Window
    {
        public bool biezacy;
        public WyborTygodnia()
        {
            InitializeComponent();
            cbTydzien.Items.Add("Bieżący tydzień");
            cbTydzien.Items.Add("Poprzedni tydzień");
        }

        private void Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if(cbTydzien.SelectedIndex < 0) { MessageBox.Show("Wybierz opcję!"); return; }
            if(cbTydzien.SelectedIndex == 0) { biezacy = true; }
            else {  biezacy = false; }
            DialogResult = true;
        }
    }
}
