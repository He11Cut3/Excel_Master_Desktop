using ExcelMaster.Point;
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

namespace ExcelMaster.UC_Math
{
    /// <summary>
    /// Логика взаимодействия для Del_Depart.xaml
    /// </summary>
    public partial class Del_Depart : Window
    {
        ExcelMaster_dbEntities _context = new ExcelMaster_dbEntities();
        private Win_Main win_;
        public ExcelMaster_Branch Branch { get; set; }

        private string _branchName = "";
        public string BranchName { get; set; } // новое свойство

        public Del_Depart(Win_Main win)
        {
            InitializeComponent();
            win_ = win;
            var entities = from e in _context.ExcelMaster_Branch
                           select e;

            // Преобразуем список объектов в список строк
            List<string> items = entities.Select(e => e.ExcelMaster_Branch_Name).ToList();

            // Устанавливаем источник данных для ComboBox
            CB.ItemsSource = items;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if ((System.Windows.MessageBox.Show("Вы уверены, что хотите удалить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                // Получаем выбранную запись
                string selectedBranchName = CB.SelectedItem.ToString();
                var branchToDelete = _context.ExcelMaster_Branch.FirstOrDefault(b => b.ExcelMaster_Branch_Name == selectedBranchName);

                if (branchToDelete != null)
                {
                    // Удаляем запись из контекста базы данных
                    _context.ExcelMaster_Branch.Remove(branchToDelete);

                    // Сохраняем изменения в базе данных
                    _context.SaveChanges();
                    win_.Button_Refr(_branchName);
                    this.Close();
                }

            }

        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
