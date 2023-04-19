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
using WpfApp3.ApplicationData;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Ruk.xaml
    /// </summary>
    public partial class Ruk : Window
    {
        public Ruk()
        {
            InitializeComponent();
            AppConnect.exModel = new exEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                var query = from karta in AppConnect.exModel.Karta_ucheta
                            join podrazdelenie in AppConnect.exModel.Podrazdelenie on karta.id_podrazdeleniya equals podrazdelenie.id_podrazdeleniya
                            join dolzhnosti in AppConnect.exModel.Dolzhnosti on karta.id_dolzhnosti equals dolzhnosti.id_dolzhnosti
                            join stavka in AppConnect.exModel.Stavka on karta.id_stavki equals stavka.id_stavki
                            join sotrudnik in AppConnect.exModel.Sotrudnik on karta.id_sotrudnika equals sotrudnik.id
                            select new { Dolzhnost = dolzhnosti.nazvanie, Stavka = stavka.naimeovanie, Surname = sotrudnik.surname, karta.data_priema, karta.data_uvolneniya };

            dataGrid1.ItemsSource = query.ToList();
           
        }
    }
}
