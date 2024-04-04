using System.Windows;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Controls;

namespace Login_WPF
{
    /// <summary>
    /// Логика для MainWindow.xaml
    /// </summary>

    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        Registration registration = new Registration();
        Welcome welcome = new Welcome();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Ведите email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Не верно введен email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {                
                //------------------------EF--------------------------
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                // получение данных
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        // получаем объекты из бд и выводим на консоль
                        var users = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Email='" + email + "' AND Password='" + password + "'").ToList(); ;
                        if (users.Count > 0)
                        {
                            foreach (User u in users)
                            {
                                string username = u.FirstName + " " + u.LastName;
                                welcome.TextBlockName.Text = username;
                                welcome.Show();
                                Close();
                            }
                        }
                        else
                        {
                            errormessage.Text = "Пожалуйста, введите правильные данные электронную почту/пароль.";
                        }
                    }
                    catch (Exception x)
                    {
                        errormessage.Text = x.ToString();
                    }
                }
                //--------------------------------------------------
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            Close();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                pwdTextBox.Text = passwordBox1.Password; 
                pwdTextBox.Visibility = Visibility.Visible; 
                passwordBox1.Visibility = Visibility.Hidden; 
            }
            else
            {
                passwordBox1.Password = pwdTextBox.Text; 
                pwdTextBox.Visibility = Visibility.Hidden; 
                passwordBox1.Visibility = Visibility.Visible; 
            }
        }
    }
}
