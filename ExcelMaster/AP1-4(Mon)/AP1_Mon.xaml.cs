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
    /// Логика взаимодействия для AP1_Mon.xaml
    /// </summary>
    public partial class AP1_Mon : Window
    {
        private string _branchName;
        private ExcelMaster_dbEntities _context;
        private UC _uC;
        public string BranchName { get; set; }

        public AP1_Mon(string branchName, ExcelMaster_dbEntities excelMaster_DbEntities, UC uC)
        {
            InitializeComponent();
            _branchName = branchName;
            this._context = excelMaster_DbEntities;
            this._uC = uC;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string _BranchName = this._branchName;

            if ((MessageBox.Show("Вы уверены, что хотите добавить информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                double A = Convert.ToInt32(AP_A.Text);
                double B = Convert.ToInt32(AP_B.Text);
                double result = (A / B) * 100;

                if (result >= 25 && result <= 100)
                {
                    var recordsToUpdate = _context.ExcelMaster_Educational_Monitoring.Where(x => x.ExcelMaster_Educational_Monitoring_Name == _BranchName).ToList();

                    foreach (var duplicate in recordsToUpdate)
                    {
                        duplicate.ExcelMaster_Educational_Monitoring_AP1 = 10;
                    };
                    _uC.Itog_Mon();
                    _context.SaveChanges();
                    this.Close();
                }
                else
                {
                    var recordsToUpdate = _context.ExcelMaster_Educational_Monitoring.Where(x => x.ExcelMaster_Educational_Monitoring_Name == _BranchName).ToList();

                    foreach (var duplicate in recordsToUpdate)
                    {
                        duplicate.ExcelMaster_Educational_Monitoring_AP1 = 0;
                    };
                    _uC.Itog_Mon();
                    _context.SaveChanges();
                    this.Close();
                }
            }

        }
    }
}
