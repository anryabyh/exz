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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Otd_Kadr.xaml
    /// </summary>
    public partial class Otd_Kadr : Window
    {
        public Otd_Kadr()
        {
            InitializeComponent();
        }

        private void otch_obr_Click(object sender, RoutedEventArgs e)
        {
            var otch_obr= new Otch_obr();
            otch_obr.Show();

        }

        private void zayavka_Click(object sender, RoutedEventArgs e)
        {
            var zaya = new zayavka();
            zaya.Show();
        }

        private void povysh_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
