using ExcelMaster.Point;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace ExcelMaster.Authorization
{
    /// <summary>
    /// Логика взаимодействия для Entry.xaml
    /// </summary>
    public partial class Entry : Window
    {
        ExcelMaster_dbEntities _context = new ExcelMaster_dbEntities();

        public string Loogin { get; set; }

        public Entry()
        {
            InitializeComponent();

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Entry_bt_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;
            var user = _context.ExcelMaster_Users.FirstOrDefault(u => u.ExcelMaster_Users_Login == login && u.ExcelMaster_Users_Password == password);
            if (user == null)
            {
                MessageBox.Show("Пользователь не найден");
            }
            else
            {
                Win_Main win_Main = new Win_Main(user);
                this.Close();
                win_Main.ShowDialog();
            }
        }

        private void Reg_bt_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;
            // Проверяем, есть ли пользователь с таким же логином
            var existingUser = _context.ExcelMaster_Users.FirstOrDefault(u => u.ExcelMaster_Users_Login == login );
            if (existingUser != null)
            {
                // Пользователь с таким же логином уже существует
                MessageBox.Show("Пользователь уже существует");
            }
            else
            {
                if (CMB.IsChecked == true)
                {
                    var newUser = new ExcelMaster_Users
                    {
                        ExcelMaster_Users_Login = login,
                        ExcelMaster_Users_Password = password,
                    };
                    _context.ExcelMaster_Users.Add(newUser);
                    _context.SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен");
                    CMB.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("Прочитайте пользовательское соглашение");
                }
                
            }

        }
      

    }
}
