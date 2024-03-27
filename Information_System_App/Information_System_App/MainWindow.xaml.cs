using System;
using System.Windows;
using System.Windows.Controls;
using Information_System_App;
using System.Security.Cryptography;
using System.Text;
using static Information_System_App.Hash_Class;
using Information_System_App.Information_System_Of_DNS_DataSetTableAdapters;

namespace Information_System_App
{
    public partial class MainWindow : Window
    {
        UsersTableAdapter users = new UsersTableAdapter();
        public MainWindow()
        {
            InitializeComponent();

            Wrong_Auth.Visibility = Visibility.Hidden;
        }

        private void Auth_Authorization_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Auth_Username_or_Email_TextBox.Text) || string.IsNullOrEmpty(Auth_Password_PasswordBox.Password))
            {
                // Показать сообщение об ошибке
                Wrong_Auth.Visibility = Visibility.Visible;
                Auth_Username_or_Email_TextBox.Text = String.Empty;
                Auth_Password_PasswordBox.Password = String.Empty;
            }
            else
            {
                var All_Users = users.GetData().Rows;
                string inputPasswordHash = HashPassword(Auth_Password_PasswordBox.Password);

                for (int i = 0; i < All_Users.Count; i++)
                {
                    if (string.IsNullOrEmpty(Auth_Username_or_Email_TextBox.Text) || string.IsNullOrEmpty(Auth_Password_PasswordBox.Password))
                    {
                        // Вызывается только в том случае, если пользователь ввёл некорректно данные. Иначе ошибка со стороны базы данных.
                        Wrong_Auth.Visibility = Visibility.Visible;
                        Auth_Username_or_Email_TextBox.Text = String.Empty;
                        Auth_Password_PasswordBox.Password = String.Empty;
                    }
                    if ((All_Users[i][1].ToString() == Auth_Username_or_Email_TextBox.Text || All_Users[i][2].ToString() == Auth_Username_or_Email_TextBox.Text) &&
                        All_Users[i][3].ToString() == inputPasswordHash.Substring(0, 40))
                    {
                        int Role_ID = (int)All_Users[i][4];

                        switch (Role_ID)
                        {
                            case 1:
                                Admin_Page admin_page = new Admin_Page();
                                admin_page.Show();
                                Close();
                                break;
                            case 2:
                                Сashier_Page сashier_page = new Сashier_Page();
                                сashier_page.Show();
                                Close();
                                break;
                            case 3:
                                Storekeeper_Page storekeeper_page = new Storekeeper_Page();
                                storekeeper_page.Show();
                                Close();
                                break;
                            case 4:
                                MessageBox.Show("К несуществующей роли нет пользовательского интерфейса\nОбратитесь к администратору", "Ошибка несуществующей роли");
                                break;
                        }
                        break;
                    }
                }
            }
            // Показать сообщение об ошибке
            Auth_Username_or_Email_TextBox.Text = String.Empty;
            Auth_Password_PasswordBox.Password = String.Empty;
            Wrong_Auth.Visibility = Visibility.Visible;
        }

        //Методы для скрывания элемента Wrong_Auth
        private void Auth_Username_or_Email_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Wrong_Auth.Visibility = Visibility.Hidden;
        }

        // Отчистка полей Логин и Пароль
        private void Auth_Password_TextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Wrong_Auth.Visibility = Visibility.Hidden;
        }
    }
}