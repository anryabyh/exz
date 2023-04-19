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
    /// Логика взаимодействия для zayavka.xaml
    /// </summary>
    public partial class zayavka : Window
    {
        public zayavka()
        {
            InitializeComponent();
            AppConnect.exModel = new exEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = (from s in AppConnect.exModel.Sotrudnik
                         join k in AppConnect.exModel.Karta_ucheta on s.id equals k.id_sotrudnika
                         join p in AppConnect.exModel.Podrazdelenie on k.id_podrazdeleniya equals p.id_podrazdeleniya
                         join z in AppConnect.exModel.Zayavka on s.id equals z.sotr_id
                         where z.creation_date >= new DateTime(2022, 1, 1) && z.creation_date <= new DateTime(2023, 12, 31)
                         orderby p.id_podrazdeleniya, s.surname, s.name, s.patronymic
                         select new
                         {
                             Surname = s.surname,
                             Name = s.name,
                             Patronymic = s.patronymic,
                             Podrazdelenie = p.id_podrazdeleniya
                         }).Distinct();

            dataGrid1.ItemsSource = query.ToList();
        }
    }
}
