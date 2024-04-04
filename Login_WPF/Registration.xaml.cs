using System.Windows;
using System.Text.RegularExpressions;

namespace Login_WPF
{
    /// <summary>
    /// логика для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
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
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                if (passwordBox1.Password.Length == 0)
                {
                    errormessage.Text = "Введите пароль.";
                    passwordBox1.Focus();
                }
                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    errormessage.Text = "Повторите ввод пароля.";
                    passwordBoxConfirm.Focus();
                }
                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    errormessage.Text = "Пароль для подтверждения должен совпадать с паролем.";
                    passwordBoxConfirm.Focus();
                }
                else
                {
                    //-----------------------------EF-------------------------------
                    errormessage.Text = "";
                    string address = textBoxAddress.Text;
                    // добавление данных
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        //db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                        // создаем два объекта User
                        User user1 = new User { FirstName = firstname, LastName = lastname, Email = email, Password = password, Address = address };
                        
                        // добавляем их в бд
                        db.Users.AddRange(user1);
                        db.SaveChanges();
                        errormessage.Text = "Данные успешно сохранены.";
                        Reset();
                        Login login = new Login();
                        login.Show();
                        Close();
                    }
                    //------------------------------------------------------------
                }
            }
        }
    }
}
