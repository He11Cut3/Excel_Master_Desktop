using ExcelMaster.UC_Math;
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

namespace ExcelMaster.AP1_4_Mon_
{
    /// <summary>
    /// Логика взаимодействия для AP3_Mon.xaml
    /// </summary>
    public partial class AP3_Mon : Window
    {
        private string _branchName;
        private ExcelMaster_dbEntities _context;
        public string BranchName { get; set; }
        private UC _uC;

        public AP3_Mon(string branchName, ExcelMaster_dbEntities excelMaster_DbEntities, UC uC)
        {
            InitializeComponent();
            _branchName = branchName;
            this._uC = uC;
            this._context = excelMaster_DbEntities;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Вы уверены, что хотите произвести рассчёт?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {

                double A = Convert.ToInt32(AP_A.Text);
                double B = Convert.ToInt32(AP_B.Text);
                double result = (A / B) * 100;
                int score = 0;
                // Вычисляем процент посещения
                double percent = (double)A / (double)B * 100;

                // Если процент посещения не менее 80%, присваиваем 10 баллов
                if (percent >= 80)
                {
                    score = 10;
                }

                var recordsToUpdate = _context.ExcelMaster_Educational_Monitoring.Where(x => x.ExcelMaster_Educational_Monitoring_Name == _branchName).ToList();

                foreach (var duplicate in recordsToUpdate)
                {
                    duplicate.ExcelMaster_Educational_Monitoring_AP3 = score;
                };
                _uC.Itog_Mon();
                _context.SaveChanges();
                this.Close();


            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
