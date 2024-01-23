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
    /// Logika interakcji dla klasy WyborMiesiacaiRoku.xaml
    /// </summary>
    public partial class WyborMiesiacaiRoku : Window
    {
        public string rokmies { get; private set; }
        public WyborMiesiacaiRoku()
        {
            InitializeComponent();
            for (int i = 1; i <= 12; i++)
            {
                cbMiesiac.Items.Add(i);
            }


        }

        private void Zatwierdz_Click(object sender, RoutedEventArgs e)
        {
            int mies = (int)cbMiesiac.SelectedItem;
            int.TryParse(txtRok.Text, out int rok);
            if (rok < 1 | rok > DateTime.Now.Year) { MessageBox.Show("Nieprawidłowy rok!"); return; }
            rokmies = $"{rok}{mies:D2}";
            DialogResult = true;
            Close();
        }
    }
}
