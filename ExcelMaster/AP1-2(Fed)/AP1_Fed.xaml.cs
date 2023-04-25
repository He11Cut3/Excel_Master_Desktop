using ExcelMaster.UC_Math;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ExcelMaster.AP1_2_Fed_
{
    /// <summary>
    /// Логика взаимодействия для AP1_Fed.xaml
    /// </summary>
    public partial class AP1_Fed : Window
    {
        private string _branchName;
        private ExcelMaster_dbEntities _context;
        public string BranchName { get; set; }
        private UC _uC;

        public AP1_Fed(string branchName, ExcelMaster_dbEntities excelMaster_DbEntities, UC uC)
        {
            InitializeComponent();
            _branchName = branchName;
            this._uC = uC;
            this._context = excelMaster_DbEntities;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                int totalStudents = Convert.ToInt32(totalStudentsTextBox.Text);
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets["Лист1"];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int excelRowCount = xlRange.Rows.Count - 1; // Вычитаем 1, чтобы не учитывать заголовок
                double percentageExcelRows = (double)excelRowCount / totalStudents;

                int score = 0; // Баллы

                int countAbove70 = 0; // Количество студентов с оценкой > 70

                // Подсчет количества студентов с оценкой > 70
                for (int i = 2; i <= xlRange.Rows.Count; i++)
                {
                    int studentScore = Convert.ToInt32(xlRange.Cells[i, 3].Value2); // Оценка студента
                    if (studentScore >= 70)
                    {
                        countAbove70++;
                    }
                }

                // Проверка, что все студенты с оценкой выше 70% и количество принявших участие больше или равно 70% от всех студентов
                if (percentageExcelRows >= 0.7)
                {

                    double ap3 = (double)countAbove70 / excelRowCount;
                    // Расчет баллов в зависимости от процента студентов с оценкой > 70
                    if (ap3 < 0.50)
                    {
                        score = 0;
                    }
                    else if (ap3 >= 0.50 && ap3 <= 0.64)
                    {
                        score = 10;
                    }
                    else if (ap3 >= 0.65)
                    {
                        score = 20;
                    }
                }
                else
                {
                    MessageBox.Show("Кол-во студентов в оценивании меньше 70% ");
                }

                xlWorkbook.Close();
                xlApp.Quit();

                MessageBox.Show("Кол-во баллов: " + score);

                var recordsToUpdate = _context.ExcelMaster_State_Сontrol.Where(x => x.ExcelMaster_State_Сontrol_Name == _branchName).ToList();

                foreach (var duplicate in recordsToUpdate)
                {
                    duplicate.ExcelMaster_State_Сontrol_AP1 = score;
                }
                _uC.Itog_Fed();
                _context.SaveChanges();
                this.Close();

            }
        }
    }
}
