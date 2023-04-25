using ExcelMaster.UC_Math;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ExcelMaster.AP1_4_Mon_
{
    /// <summary>
    /// Логика взаимодействия для AP2_Mon.xaml
    /// </summary>
    public partial class AP2_Mon : Window
    {
        private string _branchName;
        private ExcelMaster_dbEntities _context;
        public string BranchName { get; set; }
        private UC _uC;

        public AP2_Mon(string branchName, ExcelMaster_dbEntities excelMaster_DbEntities, UC uC)
        {
            InitializeComponent();
            _branchName = branchName;
            this._uC = uC;
            this._context = excelMaster_DbEntities;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                var file = new FileInfo(filePath);
                using (var package = new ExcelPackage(file))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var worksheet = package.Workbook.Worksheets["Лист1"];
                    var dt = new DataTable();

                    for (int i = worksheet.Dimension.Start.Column; i <= worksheet.Dimension.End.Column; i++)
                    {
                        dt.Columns.Add(new DataColumn(worksheet.Cells[1, i].Value.ToString()));
                    }

                    for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                    {
                        var wsRow = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                        var row = dt.NewRow();

                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Value;
                        }

                        dt.Rows.Add(row);
                    }

                    dataGrid.ItemsSource = dt.DefaultView;

                    int totalStudents = dataGrid.Items.Count;
                    int employedStudents = 0;

                    foreach (var item in dataGrid.Items)
                    {
                        if (item is DataRowView)
                        {
                            DataRowView row = item as DataRowView;
                            string employmentStatus = row.Row["Место работы"].ToString();

                            if (employmentStatus != "ВСРФ" && employmentStatus != "Учится" && employmentStatus != "-" && string.IsNullOrEmpty(employmentStatus) == false)
                            {
                                employedStudents++;
                            }
                        }
                    }

                    double employedPercentage = (double)employedStudents / totalStudents * 100;

                    MessageBox.Show($"Процент трудоустроившихся студентов: {employedPercentage:F2}%");
                    int points = 0;

                    if (employedPercentage >= 51)
                    {
                        points = 20;
                    }
                    else if (employedPercentage >= 31)
                    {
                        points = 10;
                    }
                    else
                    {
                        points = 0;
                    }

                    var recordsToUpdate = _context.ExcelMaster_Educational_Monitoring.Where(x => x.ExcelMaster_Educational_Monitoring_Name == _branchName).ToList();

                    foreach (var duplicate in recordsToUpdate)
                    {
                        duplicate.ExcelMaster_Educational_Monitoring_AP2 = points;
                    }
                    _uC.Itog_Mon();
                    _context.SaveChanges();
                    MessageBox.Show($"Баллы: {points}");
                    this.Close();

                   
                }
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            
        }
    }
}
