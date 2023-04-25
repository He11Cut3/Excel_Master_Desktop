using DocumentFormat.OpenXml.Spreadsheet;
using ExcelMaster.UC_Math;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Packaging;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelMaster.AP1_4_Mon_
{
    /// <summary>
    /// Логика взаимодействия для AP4_Mon.xaml
    /// </summary>
    public partial class AP4_Mon : Window
    {
        private string _branchName;
        private ExcelMaster_dbEntities _context;
        public string BranchName { get; set; }
        private UC _uC;

        public AP4_Mon(string branchName, ExcelMaster_dbEntities excelMaster_DbEntities, UC uC)
        {
            InitializeComponent();
            _branchName = branchName;
            this._uC = uC;
            this._context = excelMaster_DbEntities;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                Excel.Application excel = new Excel.Application();

                // открываем файл
                Excel.Workbook workbook = excel.Workbooks.Open(filePath);

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {

                    // выбираем лист с данными
                    var worksheet = package.Workbook.Worksheets["Лист1"];
                    int rows = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rows; row++)
                    {

                        double smdValue = Convert.ToDouble(worksheet.Cells[row, 3].Value);
                        double rmdValue = Convert.ToDouble(worksheet.Cells[row, 4].Value);

                        int score;
                        if (rmdValue > smdValue)
                        {
                            score = 10;
                        }
                        else
                        {
                            score = 5;
                        }

                        var recordsToUpdate = _context.ExcelMaster_Educational_Monitoring.Where(x => x.ExcelMaster_Educational_Monitoring_Name == _branchName).ToList();

                        foreach (var duplicate in recordsToUpdate)
                        {
                            duplicate.ExcelMaster_Educational_Monitoring_AP4 = score;
                        };
                        _uC.Itog_Mon();
                        _context.SaveChanges();
                        this.Close();
                    }


                    // закрываем приложение Excel
                    excel.Quit();
                }
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
