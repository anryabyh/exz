using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.ApplicationData;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.exModel = new exEntities();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = AppConnect.exModel.Sotrudnik.FirstOrDefault(x => x.login == UsernameTextBox.Text && x.password == PasswordBox.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (userObj.role)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте, сотрудник отдела кадров " + userObj.name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            var otdKadrWindow = new Otd_Kadr();
                            otdKadrWindow.Show();
                            Close();
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте, руководитель " + userObj.name + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            var rukWindow = new Ruk();
                            rukWindow.Show();
                            Close();
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка" + Ex.Message.ToString(), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
