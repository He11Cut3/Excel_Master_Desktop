using ExcelMaster.UC_Math;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ExcelMaster.Point
{
    /// <summary>
    /// Логика взаимодействия для Win_Main.xaml
    /// </summary>
    public partial class Win_Main : Window
    {
        private string _branchName = "";
        ExcelMaster_dbEntities _context = new ExcelMaster_dbEntities();
        private string branchName; // переменная для хранения наименования кнопки

        public Win_Main()
        {
            InitializeComponent();
            Button_Refr(_branchName); // передаем значение _branchName в метод Button_Refr()
        }


        public void Button_Refr(string branchName) // добавляем параметр branchName
        {
            testBut.Children.Clear();
            var buttonData = _context.ExcelMaster_Branch.ToList();
            foreach (var data in buttonData)
            {
                var button = new Button
                {
                    Name = "button_" + data.ExcelMaster_Branch_id.ToString(),
                    Content = data.ExcelMaster_Branch_Name,
                    Style = (Style)FindResource("menuButton"),
                    Tag = new UC(branchName, _context)
                };
                button.Click += Button_Click;
                testBut.Children.Add(button);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                // Получить уникальный идентификатор филиала из имени кнопки
                string buttonId = button.Name.Split('_').Last();

                // Использовать идентификатор, чтобы получить филиал из базы данных
                    int branchId = int.Parse(buttonId);
                    var branch = _context.ExcelMaster_Branch.FirstOrDefault(b => b.ExcelMaster_Branch_id == branchId);

                    // Получить наименование кнопки и сохранить его в переменную branchName
                    branchName = button.Content.ToString();

                    // Удалить предыдущий UserControl, если он был добавлен ранее
                    TestUC.Children.Clear();

                    // Создать новый экземпляр UserControl и добавить его на StackPanel
                    var userControl = new UC(branchName, _context);
                    userControl.Branch = branch;
                    userControl.BranchName = branchName; // передать значение branchName в UserControl
                    userControl.UpdateLayout(); // Обновить визуальный интерфейс пользовательского элемента управления
                    TestUC.Children.Add(userControl);
            }
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private void New_Depart_Click(object sender, RoutedEventArgs e)
        {
            New_Depart new_Depart = new New_Depart(_context, this);
            new_Depart.ShowDialog();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
           Application.Current.Shutdown();  
        }
    }
}
