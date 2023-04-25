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

namespace ExcelMaster.AP1_4__Gos_
{
    /// <summary>
    /// Логика взаимодействия для AP4_Gos.xaml
    /// </summary>
    public partial class AP4_Gos : Window
    {
        private string _branchName;
        private ExcelMaster_dbEntities _context;
        public string BranchName { get; set; }
        private UC _uC;

        public AP4_Gos(string branchName, ExcelMaster_dbEntities excelMaster_DbEntities, UC uC)
        {
            InitializeComponent();
            _branchName = branchName;
            this._uC = uC;
            this._context = excelMaster_DbEntities;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            if (CB1.IsChecked == true)
            {
                count++;
            }
            if (CB2.IsChecked == true)
            {
                count++;
            }
            if (count == 2)
            {
                if ((MessageBox.Show("Вы уверены, что хотите рассчитать информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {

                    var recordsToUpdate = _context.ExcelMaster_Educational_Аctivities.Where(x => x.ExcelMaster_Educational_Аctivities_Name == _branchName).ToList();

                    foreach (var duplicate in recordsToUpdate)
                    {
                        duplicate.ExcelMaster_Educational_Аctivities_AP4 = 10;
                    }
                    _uC.Itog_Gos();
                    _context.SaveChanges();
                    this.Close();
                }
            }
            else
            {
                if ((MessageBox.Show("Вы уверены, что хотите рассчитать информацию?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
                {

                    var recordsToUpdate = _context.ExcelMaster_Educational_Аctivities.Where(x => x.ExcelMaster_Educational_Аctivities_Name == _branchName).ToList();

                    foreach (var duplicate in recordsToUpdate)
                    {
                        duplicate.ExcelMaster_Educational_Аctivities_AP4 = 0;
                    }
                    _uC.Itog_Gos();
                    _context.SaveChanges();
                    this.Close();
                }
            }
        }
    }
}
