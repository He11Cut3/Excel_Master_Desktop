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
    /// Логика взаимодействия для New_Depart.xaml
    /// </summary>
    public partial class New_Depart : Window
    {
        private ExcelMaster_dbEntities _context;
        private Win_Main _wm;

        public New_Depart(ExcelMaster_dbEntities excelMaster_DbEntities, Win_Main win_Main)
        {
            InitializeComponent();
            this._context = excelMaster_DbEntities;
            this._wm = win_Main; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Вы уверены, что хотите добавить отдел?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                var branchName = Dep.Text;
                var branch = _context.ExcelMaster_Branch.FirstOrDefault(b => b.ExcelMaster_Branch_Name == branchName);

                if (branch != null)
                {
                    MessageBox.Show("Данный отдел уже существует");
                }
                else
                {
                    _context.ExcelMaster_Branch.Add(new ExcelMaster_Branch()
                    {
                        ExcelMaster_Branch_Name = Dep.Text,
                    });
                    

                    _context.SaveChanges();

                    _context.ExcelMaster_Educational_Аctivities.Add(new ExcelMaster_Educational_Аctivities{
                        ExcelMaster_Educational_Аctivities_Name = Dep.Text,
                    });
                    _context.ExcelMaster_Educational_Monitoring.Add(new ExcelMaster_Educational_Monitoring
                    {
                        ExcelMaster_Educational_Monitoring_Name = Dep.Text,
                    });
                    _context.ExcelMaster_State_Сontrol.Add(new ExcelMaster_State_Сontrol
                    {
                        ExcelMaster_State_Сontrol_Name = Dep.Text,
                    });

                    _context.SaveChanges();

                    var recordsToUpdate = _context.ExcelMaster_Educational_Аctivities.Where(x => x.ExcelMaster_Educational_Аctivities_Name == branchName).ToList();

                    

                    foreach (var duplicate in recordsToUpdate)
                    {
                        duplicate.ExcelMaster_Educational_Аctivities_AP1 = 0;
                        duplicate.ExcelMaster_Educational_Аctivities_AP2 = 0;
                        duplicate.ExcelMaster_Educational_Аctivities_AP3 = 0;
                        duplicate.ExcelMaster_Educational_Аctivities_AP4 = 0;
                        duplicate.ExcelMaster_Educational_Аctivities_Total = 0;
                    };

                    var recordsToUpdate1 = _context.ExcelMaster_Educational_Monitoring.Where(x => x.ExcelMaster_Educational_Monitoring_Name == branchName).ToList();



                    foreach (var duplicate1 in recordsToUpdate1)
                    {
                        duplicate1.ExcelMaster_Educational_Monitoring_AP1 = 0;
                        duplicate1.ExcelMaster_Educational_Monitoring_AP2 = 0;
                        duplicate1.ExcelMaster_Educational_Monitoring_AP3 = 0;
                        duplicate1.ExcelMaster_Educational_Monitoring_AP4 = 0;
                        duplicate1.ExcelMaster_Educational_Monitoring_Total = 0;
                    }

                    var recordsToUpdate2 = _context.ExcelMaster_State_Сontrol.Where(x => x.ExcelMaster_State_Сontrol_Name == branchName).ToList();



                    foreach (var duplicate2 in recordsToUpdate2)
                    {
                        duplicate2.ExcelMaster_State_Сontrol_AP1 = 0;
                        duplicate2.ExcelMaster_State_Сontrol_AP2 = 0;
                        duplicate2.ExcelMaster_State_Сontrol_Total = 0;
                    }
                    _context.SaveChanges();
                    _wm.Button_Refr(branchName);
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
