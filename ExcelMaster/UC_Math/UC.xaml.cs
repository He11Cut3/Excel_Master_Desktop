using ExcelMaster.AP1_2_Fed_;
using ExcelMaster.AP1_4__Gos_;
using ExcelMaster.AP1_4_Mon_;
using MaterialDesignThemes.Wpf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExcelMaster.UC_Math
{
    /// <summary>
    /// Логика взаимодействия для UC.xaml
    /// </summary>
    public partial class UC : UserControl
    {
        public ExcelMaster_Branch Branch { get; set; }
        public string BranchName { get; set; } // новое свойство
        private string _branchName;

        ExcelMaster_dbEntities _context = new ExcelMaster_dbEntities();

        public UC(string branchName, ExcelMaster_dbEntities excelMaster_DbEntities)
        {
            InitializeComponent();
            Itog_Gos();
            _branchName = branchName;
            this._context = excelMaster_DbEntities;
            
        }

        public void Itog_Gos()
        {
            Report.Text = "";
            var branchInfo = _context.ExcelMaster_Educational_Аctivities
                                    .Where(b => b.ExcelMaster_Educational_Аctivities_Name == _branchName)
                                    .FirstOrDefault();
            if (branchInfo != null)
            {
                int? sum = branchInfo.ExcelMaster_Educational_Аctivities_AP1 + branchInfo.ExcelMaster_Educational_Аctivities_AP2 + branchInfo.ExcelMaster_Educational_Аctivities_AP3 + branchInfo.ExcelMaster_Educational_Аctivities_AP4;
                Report.Text = sum.ToString();
                var recordsToUpdate = _context.ExcelMaster_Educational_Аctivities.Where(x => x.ExcelMaster_Educational_Аctivities_Name == _branchName).ToList();
                

                foreach (var duplicate in recordsToUpdate)
                {
                        duplicate.ExcelMaster_Educational_Аctivities_Total = sum;
                        Report.Text = "Баллы: " + sum;
                }
            }
            else
            {
                Report.Text = "Баллы: " + 0.ToString();
            }
        }
        public void Itog_Mon()
        {
            Report.Text = "";
            var branchInfo = _context.ExcelMaster_Educational_Monitoring
                                    .Where(b => b.ExcelMaster_Educational_Monitoring_Name == _branchName)
                                    .FirstOrDefault();
            if (branchInfo != null)
            {
                int? sum1 = branchInfo.ExcelMaster_Educational_Monitoring_AP1 + branchInfo.ExcelMaster_Educational_Monitoring_AP2 + branchInfo.ExcelMaster_Educational_Monitoring_AP3 + branchInfo.ExcelMaster_Educational_Monitoring_AP4;
                Report.Text = sum1.ToString();
                var recordsToUpdate = _context.ExcelMaster_Educational_Monitoring.Where(x => x.ExcelMaster_Educational_Monitoring_Name == _branchName).ToList();


                foreach (var duplicate in recordsToUpdate)
                {
                    duplicate.ExcelMaster_Educational_Monitoring_Total =  sum1;
                    Report.Text = "Баллы: " + sum1;
                }
            }
            else
            {
                Report.Text = "Баллы: " + 0.ToString();
            }
        }
        public void Itog_Fed()
        {
            Report.Text = "";
            var branchInfo = _context.ExcelMaster_State_Сontrol
                                    .Where(b => b.ExcelMaster_State_Сontrol_Name == _branchName)
                                    .FirstOrDefault();
            if (branchInfo != null)
            {
                int? sum2 = branchInfo.ExcelMaster_State_Сontrol_AP1 + branchInfo.ExcelMaster_State_Сontrol_AP2;
                Report.Text = sum2.ToString();
                var recordsToUpdate = _context.ExcelMaster_State_Сontrol.Where(x => x.ExcelMaster_State_Сontrol_Name == _branchName).ToList();


                foreach (var duplicate in recordsToUpdate)
                {
                    duplicate.ExcelMaster_State_Сontrol_Total = sum2;
                    Report.Text = "Баллы: " + sum2;
                }
            }
            else
            {
                Report.Text = "Баллы: " + 0.ToString();
            }
        }

        public void SetBranchInfo(ExcelMaster_Branch branch)
        {
            BranchName = branch.ExcelMaster_Branch_Name;
        }

        public void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (Branch != null)
            {
                SetBranchInfo(Branch);
                DataContext = this;
            }
        }
        private void Gos_Click(object sender, RoutedEventArgs e)
        {

            AP1_Gos.Visibility = Visibility.Visible;
            AP2_Gos.Visibility = Visibility.Visible;
            AP3_Gos.Visibility = Visibility.Visible;
            AP4_Gos.Visibility = Visibility.Visible;

            AP1_Monitoring.Visibility = Visibility.Collapsed;
            AP2_Monitoring.Visibility = Visibility.Collapsed;
            AP3_Monitoring.Visibility = Visibility.Collapsed;
            AP4_Monitoring.Visibility = Visibility.Collapsed;

            AP1_Fed.Visibility = Visibility.Collapsed;
            AP2_Fed.Visibility = Visibility.Collapsed;

            Fed_Report.Visibility = Visibility.Collapsed;
            Mon_Report.Visibility = Visibility.Collapsed;
            Gos_Report.Visibility = Visibility.Visible;   
            

            Itog_Gos();
        }

        private void Monitoring_Click(object sender, RoutedEventArgs e)
        {
            AP1_Monitoring.Visibility = Visibility.Visible;
            AP2_Monitoring.Visibility = Visibility.Visible;
            AP3_Monitoring.Visibility = Visibility.Visible;
            AP4_Monitoring.Visibility = Visibility.Visible;
            
            AP1_Gos.Visibility = Visibility.Collapsed;
            AP2_Gos.Visibility = Visibility.Collapsed;
            AP3_Gos.Visibility = Visibility.Collapsed;
            AP4_Gos.Visibility = Visibility.Collapsed;

            AP1_Fed.Visibility = Visibility.Collapsed;
            AP2_Fed.Visibility = Visibility.Collapsed;

            Fed_Report.Visibility = Visibility.Collapsed;
            Mon_Report.Visibility = Visibility.Visible;
            Gos_Report.Visibility = Visibility.Collapsed;

            Itog_Mon();
        }

        private void Fed_Click(object sender, RoutedEventArgs e)
        {
            AP1_Fed.Visibility = Visibility.Visible;
            AP2_Fed.Visibility = Visibility.Visible;

            AP1_Monitoring.Visibility = Visibility.Collapsed;
            AP2_Monitoring.Visibility = Visibility.Collapsed;
            AP3_Monitoring.Visibility = Visibility.Collapsed;
            AP4_Monitoring.Visibility = Visibility.Collapsed;

            AP1_Gos.Visibility = Visibility.Collapsed;
            AP2_Gos.Visibility = Visibility.Collapsed;
            AP3_Gos.Visibility = Visibility.Collapsed;
            AP4_Gos.Visibility = Visibility.Collapsed;

            Fed_Report.Visibility = Visibility.Visible;
            Mon_Report.Visibility = Visibility.Collapsed;
            Gos_Report.Visibility = Visibility.Collapsed;

            Itog_Fed();
        }

        private void AP1_Gos_Click(object sender, RoutedEventArgs e)
        {
            Itog_Gos();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP1_Gos aP1_Gos = new AP1_Gos(branchName, _context, this);
            aP1_Gos.ShowDialog();
        }

        private void AP2_Gos_Click(object sender, RoutedEventArgs e)
        {
            Itog_Gos();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP2_Gos aP2_Gos = new AP2_Gos(branchName, _context, this);
            aP2_Gos.ShowDialog();
        }

        private void AP3_Gos_Click(object sender, RoutedEventArgs e)
        {
            Itog_Gos();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP3_Gos aP3_Gos = new AP3_Gos(branchName, _context, this);
            aP3_Gos.ShowDialog();
        }

        private void AP4_Gos_Click(object sender, RoutedEventArgs e)
        {
            Itog_Gos();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP4_Gos aP4_Gos = new AP4_Gos(branchName, _context, this);
            aP4_Gos.ShowDialog();
        }

        

        private void AP1_Monitoring_Click(object sender, RoutedEventArgs e)
        {
            Itog_Mon();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP1_Mon aP1_Mon = new AP1_Mon(branchName, _context, this);
            aP1_Mon.ShowDialog();
        }

        private void AP2_Monitoring_Click(object sender, RoutedEventArgs e)
        {
            Itog_Mon();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP2_Mon aP2_Mon = new AP2_Mon(branchName, _context, this);
            //branchName, _context, this
            aP2_Mon.ShowDialog();
        }

        private void AP3_Monitoring_Click(object sender, RoutedEventArgs e)
        {
            Itog_Mon();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP3_Mon aP3_Mon = new AP3_Mon(branchName, _context, this);
            //branchName, _context, this
            aP3_Mon.ShowDialog();
        }

        private void AP4_Monitoring_Click(object sender, RoutedEventArgs e)
        {
            Itog_Mon();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP4_Mon aP4_Mon = new AP4_Mon(branchName, _context, this);
            //branchName, _context, this
            aP4_Mon.ShowDialog();
        }

        private void AP1_Fed_Click(object sender, RoutedEventArgs e)
        {
            Itog_Fed();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP1_Fed aP1_Fed = new AP1_Fed(branchName, _context, this);
            //branchName, _context, this
            aP1_Fed.ShowDialog();
        }

        private void AP2_Fed_Click(object sender, RoutedEventArgs e)
        {
            Itog_Fed();
            if (_context == null)
            {
                MessageBox.Show("Не удалось инициализировать контекст базы данных.");
                return;
            }
            var branchName = Branch.ExcelMaster_Branch_Name;
            AP2_Fed aP2_Fed = new AP2_Fed(branchName, _context, this);
            //branchName, _context, this
            aP2_Fed.ShowDialog();
        }

        private void Gos_Report_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Вы уверены, что хотите сформировать отчёт?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                List<ExcelMaster_Educational_Аctivities> data = _context.ExcelMaster_Educational_Аctivities.ToList();

                // Определяем наименования столбцов
                string[] columnNames = new string[] { "Направление", "АП1", "АП2", "АП3", "АП4", "Итог" };
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Создаем новый файл Excel
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Добавляем лист
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Гос.Акр.");

                    // Записываем наименования столбцов
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columnNames[i];
                    }

                    // Записываем данные
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].ExcelMaster_Educational_Аctivities_Name;
                        worksheet.Cells[i + 2, 2].Value = data[i].ExcelMaster_Educational_Аctivities_AP1;
                        worksheet.Cells[i + 2, 3].Value = data[i].ExcelMaster_Educational_Аctivities_AP2;
                        worksheet.Cells[i + 2, 4].Value = data[i].ExcelMaster_Educational_Аctivities_AP3;
                        worksheet.Cells[i + 2, 5].Value = data[i].ExcelMaster_Educational_Аctivities_AP4;
                        worksheet.Cells[i + 2, 6].Value = data[i].ExcelMaster_Educational_Аctivities_Total;
                    }

                    // Сохраняем файл
                    File.WriteAllBytes("Гос.Акр.xlsx", package.GetAsByteArray());
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Гос.Акр.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                }
            }
        }

        private void Mon_Report_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Вы уверены, что хотите сформировать отчёт?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                List<ExcelMaster_Educational_Monitoring> data = _context.ExcelMaster_Educational_Monitoring.ToList();

                // Определяем наименования столбцов
                string[] columnNames = new string[] { "Направление", "АП1", "АП2", "АП3", "АП4", "Итог" };
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Создаем новый файл Excel
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Добавляем лист
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Аккр.мониторинг");

                    // Записываем наименования столбцов
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columnNames[i];
                    }

                    // Записываем данные
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].ExcelMaster_Educational_Monitoring_Name;
                        worksheet.Cells[i + 2, 2].Value = data[i].ExcelMaster_Educational_Monitoring_AP1;
                        worksheet.Cells[i + 2, 3].Value = data[i].ExcelMaster_Educational_Monitoring_AP2;
                        worksheet.Cells[i + 2, 4].Value = data[i].ExcelMaster_Educational_Monitoring_AP3;
                        worksheet.Cells[i + 2, 5].Value = data[i].ExcelMaster_Educational_Monitoring_AP4;
                        worksheet.Cells[i + 2, 6].Value = data[i].ExcelMaster_Educational_Monitoring_Total;
                    }

                    // Сохраняем файл
                    File.WriteAllBytes("Аккр.мониторинг.xlsx", package.GetAsByteArray());
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Аккр.мониторинг.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                }
            }
        }

        private void Fed_Report_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Вы уверены, что хотите сформировать отчёт?", "Добавление", MessageBoxButton.YesNo, MessageBoxImage.Warning)) == MessageBoxResult.Yes)
            {
                List<ExcelMaster_State_Сontrol> data = _context.ExcelMaster_State_Сontrol.ToList();

                // Определяем наименования столбцов
                string[] columnNames = new string[] { "Направление", "АП1", "АП2", "Итог" };
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                // Создаем новый файл Excel
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Добавляем лист
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Фед. гос. контроль");

                    // Записываем наименования столбцов
                    for (int i = 0; i < columnNames.Length; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = columnNames[i];
                    }

                    // Записываем данные
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = data[i].ExcelMaster_State_Сontrol_Name;
                        worksheet.Cells[i + 2, 2].Value = data[i].ExcelMaster_State_Сontrol_AP1;
                        worksheet.Cells[i + 2, 3].Value = data[i].ExcelMaster_State_Сontrol_AP2;
                        worksheet.Cells[i + 2, 4].Value = data[i].ExcelMaster_State_Сontrol_Total;
                    }

                    // Сохраняем файл
                    File.WriteAllBytes("Фед.гос.контроль.xlsx", package.GetAsByteArray());
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, "Фед.гос.контроль.xlsx");
                    File.WriteAllBytes(filePath, package.GetAsByteArray());
                }
            }
        }
    }
}
