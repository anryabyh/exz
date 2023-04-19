using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using Excel = Microsoft.Office.Interop.Excel;
namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Otch_obr.xaml
    /// </summary>
    public partial class Otch_obr : System.Windows.Window
    {
        public Otch_obr()
        {
            InitializeComponent();
            AppConnect.exModel = new exEntities();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query = from pod in AppConnect.exModel.Podrazdelenie
                        join karta in AppConnect.exModel.Karta_ucheta on pod.id_podrazdeleniya equals karta.id_podrazdeleniya
                        join sotr in AppConnect.exModel.Sotrudnik on karta.id_sotrudnika equals sotr.id
                        join dol in AppConnect.exModel.Dolzhnosti on karta.id_dolzhnosti equals dol.id_dolzhnosti
                        join doc in AppConnect.exModel.Doc_obr on sotr.doc_obr_id equals doc.id
                        select new
                        {
                            Podrazdelenie_id = pod.id_podrazdeleniya,
                            Surname = sotr.surname,
                            Name = sotr.name,
                            Patronymic = sotr.patronymic,
                            age = (int)DbFunctions.DiffYears(sotr.birth_date, DateTime.Now),
                            education = doc.description.Replace("высшее", ""),
                            Dolzhnosti_name = dol.nazvanie
                        };

            dataGrid1.ItemsSource = query.ToList();


        }


        private void b_otch_Click(object sender, RoutedEventArgs e)
        {
            // Создаем новый экземпляр приложения Excel
            Excel.Application excelApp = new Excel.Application();

            // Делаем приложение Excel видимым
            excelApp.Visible = true;

            // Создаем новую книгу Excel
            Excel.Workbook workbook = excelApp.Workbooks.Add();

            // Получаем первый лист в книге
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            // Задаем заголовки для столбцов
            worksheet.Cells[1, 1] = "ID подразделения";
            worksheet.Cells[1, 2] = "Фамилия";
            worksheet.Cells[1, 3] = "Имя";
            worksheet.Cells[1, 4] = "Отчество";
            worksheet.Cells[1, 5] = "Возраст";
            worksheet.Cells[1, 6] = "Образование";
            worksheet.Cells[1, 7] = "Должность";

            // Получаем данные из запроса и заполняем таблицу в Excel
            var query = from pod in AppConnect.exModel.Podrazdelenie
                        join karta in AppConnect.exModel.Karta_ucheta on pod.id_podrazdeleniya equals karta.id_podrazdeleniya
                        join sotr in AppConnect.exModel.Sotrudnik on karta.id_sotrudnika equals sotr.id
                        join dol in AppConnect.exModel.Dolzhnosti on karta.id_dolzhnosti equals dol.id_dolzhnosti
                        join doc in AppConnect.exModel.Doc_obr on sotr.doc_obr_id equals doc.id
                        select new
                        {
                            Podrazdelenie_id = pod.id_podrazdeleniya,
                            Surname = sotr.surname,
                            Name = sotr.name,
                            Patronymic = sotr.patronymic,
                            age = (int)DbFunctions.DiffYears(sotr.birth_date, DateTime.Now),
                            education = doc.description.Replace("высшее", ""),
                            Dolzhnosti_name = dol.nazvanie
                        };

            int row = 2;

            foreach (var item in query)
            {
                worksheet.Cells[row, 1] = item.Podrazdelenie_id;
                worksheet.Cells[row, 2] = item.Surname;
                worksheet.Cells[row, 3] = item.Name;
                worksheet.Cells[row, 4] = item.Patronymic;
                worksheet.Cells[row, 5] = item.age;
                worksheet.Cells[row, 6] = item.education;
                worksheet.Cells[row, 7] = item.Dolzhnosti_name;

                row++;
            }

        }
    }
}