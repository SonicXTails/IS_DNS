using Information_System_App.Information_System_Of_DNS_DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
using ControlzEx.Standard;
using static Information_System_App.Hash_Class;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.UI.WebControls;
using static MaterialDesignThemes.Wpf.Theme;
using System.Text.RegularExpressions;

namespace Information_System_App
{
    public partial class Admin_Page : Window
    {
        public UsersTableAdapter users = new UsersTableAdapter();
        public RolesTableAdapter roles = new RolesTableAdapter();
        public Users_DatasTableAdapter users_datas = new Users_DatasTableAdapter();
        public Histories_of_Changes_In_Prices_of_ProductsTableAdapter history = new Histories_of_Changes_In_Prices_of_ProductsTableAdapter();
        public ProductsTableAdapter products = new ProductsTableAdapter();
        public StockTableAdapter stock = new StockTableAdapter();
        public Admin_Page()
        {
            InitializeComponent();
            Initialisation_MainWindow_for_Admin_Hidden();
        }
        // Меню //
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }



        // Пользователи //
        int User_ID_from_BD;
        int User_Data_ID_from_BD;
        private void Open_Table_Users_Click(object sender, RoutedEventArgs e)
        {
            Initialisation_MainWindow_for_Admin_Hidden();
            Open_Table_Users_Visible();
            Update_ComboBoxes_DataGrids();

            // 0 4 5 10 (Clear)
            Users_DataGrid.Columns[0].Visibility = Visibility.Collapsed;
            Users_DataGrid.Columns[4].Visibility = Visibility.Collapsed;
            Users_DataGrid.Columns[5].Visibility = Visibility.Collapsed;
            Users_DataGrid.Columns[6].Visibility = Visibility.Collapsed;
            Users_DataGrid.Columns[10].Visibility = Visibility.Collapsed;
        }
        // Методы для валидации полей
        private void Update_Name_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = Update_Name_textbox.Text;
            string nonNumericInput = new string(input.Where(c => !char.IsDigit(c)).ToArray());

            if (input != nonNumericInput)
            {
                Update_Name_textbox.Text = nonNumericInput;
                Update_Name_textbox.Select(Update_Name_textbox.Text.Length, 0);
            }
        }
        private void Update_Surname_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = Update_Surname_textbox.Text;
            string nonNumericInput = new string(input.Where(c => !char.IsDigit(c)).ToArray());

            if (input != nonNumericInput)
            {
                Update_Surname_textbox.Text = nonNumericInput;
                Update_Surname_textbox.Select(Update_Surname_textbox.Text.Length, 0);
            }
        }
        private void Update_SecondName_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string input = Update_SecondName_textbox.Text;
            string nonNumericInput = new string(input.Where(c => !char.IsDigit(c)).ToArray());

            if (input != nonNumericInput)
            {
                Update_SecondName_textbox.Text = nonNumericInput;
                Update_SecondName_textbox.Select(Update_SecondName_textbox.Text.Length, 0);
            }
        }
        // Функции
        private void Delete_users_button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение поля
            if (Select_users_delete_combobox.SelectedItem == null)
            {
                MessageBox.Show("Вы не заполнили поле!\nПопробуйте снова.", "Ошибка при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Отчистка полей
                Clear_Add_Fields();
            }
            else
            {
                // Функция удаления
                DataRowView Name_to_del_in_users = (DataRowView)Select_users_delete_combobox.SelectedItem;
                string Username = Convert.ToString(Name_to_del_in_users["User_Username"]);
                users.DeleteQueryByUsernameInUsers(Username);

                // Обновление полей
                Update_ComboBoxes_DataGrids();
            }
        }
        private void Add_Users_button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение данных
            if (string.IsNullOrEmpty(Add_Write_Username_textbox.Text) || 
                string.IsNullOrEmpty(Add_Write_Password_PasswordBox.Password) || 
                Add_Select_Role_Combobox.SelectedItem == null)
            {
                MessageBox.Show("Вы не заполнили все поля!\nПопробуйте снова.", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Add_Fields();
            }
            else
            {
                string hashedPassword = HashPassword(Add_Write_Password_PasswordBox.Password);

                DataRowView Role_to_Add_in_Users = (DataRowView)Add_Select_Role_Combobox.SelectedItem;
                int Role = Convert.ToInt32(Role_to_Add_in_Users["Role_ID"]);

                // Функция добавления пустых данных в users_Data
                users_datas.InsertQueryInUsers_Datas_Name_Surname();
                int maxUsersDatas = users_datas.GetData().Max(row => row.User_Data_ID);

                // Функция добавления только в Users
                users.InsertQueryInUsers_Role_Username_Password_maxUserData(Add_Write_Username_textbox.Text.ToString(), "@Почта", hashedPassword, Role, maxUsersDatas);

                Clear_Add_Fields();

                // Обновление полей
                Update_ComboBoxes_DataGrids();
            }
        }
        private void Users_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Users_DataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Users_DataGrid.SelectedItem;
                Update_Username_textbox.Text = row["User_Username"].ToString();
                Update_Email_textbox.Text = row["User_Email"].ToString();
                Update_Name_textbox.Text = row["User_Data_Name"].ToString();
                Update_Surname_textbox.Text = row["User_Data_Surname"].ToString();
                Update_SecondName_textbox.Text = row["User_Data_Second_Name"].ToString();
                Update_RoleName_Combobox.Text = row["Role_Name"].ToString();

                Update_RoleName_Combobox.IsEnabled = true;

                ID_User_from_BD.Text = "Имя (старое)" + row["User_Data_Name"].ToString();

                User_ID_from_BD = (int)row["User_ID1"];
                User_Data_ID_from_BD = (int)row["User_Data_ID"];
            }
        }
        private void Update_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Update_Username_textbox.Text) ||
                string.IsNullOrEmpty(Update_Email_textbox.Text) ||
                string.IsNullOrEmpty(Update_Password_textbox.Password) ||
                string.IsNullOrEmpty(Update_Name_textbox.Text) ||
                string.IsNullOrEmpty(Update_Surname_textbox.Text) ||
                string.IsNullOrEmpty(Update_RoleName_Combobox.Text))
            {
                MessageBox.Show("Вы заполнили не все поля!", "Ошибка обновления данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (!Update_Email_textbox.Text.Contains("@"))
                {
                    // Если введенный текст не содержит знак @, вы можете отобразить сообщение об ошибке
                    MessageBox.Show("Адрес электронной почты должен содержать символ @", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string Username = Update_Username_textbox.Text;
                    string Email = Update_Email_textbox.Text;
                    string Password = HashPassword(Update_Password_textbox.Password);
                    string Name = Update_Name_textbox.Text;
                    string Surname = Update_Surname_textbox.Text;
                    string SecondName = Update_SecondName_textbox.Text;
                    DataRowView Role_Name_Update = (DataRowView)Update_RoleName_Combobox.SelectedItem;

                    int Role_ID = ((int)Role_Name_Update["Role_ID"]);

                    // Обновление данных
                    users_datas.UpdateQueryNSS(Name, Surname, SecondName, User_Data_ID_from_BD);
                    users.UpdateQueryDatas_in_Users(Username, Email, Password, Role_ID, User_ID_from_BD);

                    // После успешного обновления можно вывести сообщение об успешном обновлении
                    MessageBox.Show("Данные пользователя успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Обновление полей
                    Update_ComboBoxes_DataGrids();

                    // Отчистка полей
                    Clear_Add_Fields();
                }
            }
        }




        // Роли //
        public int Role_ID;
        private void Roles_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Roles_DataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Roles_DataGrid.SelectedItem;
                Role_Name_textbox.Text = row["Role_Name"].ToString();
                Role_ID = (int)row["Role_ID"];
            }
        }

        private void Delete_Role_button_Click(object sender, RoutedEventArgs e)
        {
            // Обновление полей
            Update_ComboBoxes_DataGrids();

            // Получение количества пользователей, использующих эту роль
            int userCount = (int)users.ScalarQueryFindUsersRole(Role_ID);

            // Проверка на заполнение поля
            if (string.IsNullOrEmpty(Role_Name_textbox.Text))
            {
                MessageBox.Show("Вы не заполнили поле!\nПопробуйте снова.", "Ошибка при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Отчистка полей
                Clear_Add_Fields();
            }
            // Если есть пользователи, использующие эту роль, выдать ошибку
            if (userCount > 0)
            {
                MessageBox.Show("Невозможно удалить эту роль, так как она используется пользователями!", "Ошибка внешних ключей при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Отчистка полей
                Clear_Add_Fields();
            }
            else
            {
                // Функция удаления роли
                string Role_Name = Role_Name_textbox.Text;
                roles.DeleteQueryRole_From_Roles(Role_ID, Role_Name);

                // Обновления
                Update_ComboBoxes_DataGrids();

                // Отчистка полей
                Clear_Add_Fields();
            }
        }

        private void Add_Role_button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение поля
            if (string.IsNullOrEmpty(Role_Name_textbox.Text))
            {
                MessageBox.Show("Вы не заполнили поле!\nПопробуйте снова.", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Отчистка полей
                Clear_Add_Fields();
            }
            else
            {
                // Функция добавления только в Roles
                roles.InsertQueryRole_Name(Role_Name_textbox.Text.ToString());

                //Отчистка полей
                Clear_Add_Fields();

                // Обновление полей
                Update_ComboBoxes_DataGrids();
            }
        }

        private void Update_Role_button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение поля
            if (string.IsNullOrEmpty(Role_Name_textbox.Text))
            {
                MessageBox.Show("Вы не заполнили поле!\nПопробуйте снова.", "Ошибка при обновлении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Отчистка полей
                Clear_Add_Fields();
            }
            else
            {
                roles.UpdateQueryRole_Name(Role_Name_textbox.Text, Role_ID);

                // Обновление полей
                Update_ComboBoxes_DataGrids();
            }
        }



        // История покупок //
        private void Open_Table_HoCPoP_Click(object sender, RoutedEventArgs e)
        {
            Initialisation_MainWindow_for_Admin_Hidden();
            Open_Table_History_Products_Visible();
            Update_ComboBoxes_DataGrids();

            Histories_DataGrid.Columns[5].Visibility = Visibility.Collapsed;
        }
        // Заполнение полей
        public float New_for_Old = 0;
        public string New_for_Old_i;
        private void Histories_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Histories_DataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Histories_DataGrid.SelectedItem;
                Product_Combobox.Text = row["ChangesP__of_Products_ID"].ToString();
                Old_textbox.Text = row["ChangesP__of_Products_Old"].ToString();
                New_textbox.Text = row["ChangesP__of_Products_New"].ToString();
                    // Дата выставляется автоматически после обновления//
                Reason_textbox.Text = row["ChangesP__of_Products_Reason"].ToString();


                this.New_for_Old_i = row["ChangesP__of_Products_New"].ToString();
                float.TryParse(New_for_Old_i, out New_for_Old);
            }
        }
        // Валидация поля
        private void New_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(New_textbox.Text, "^[0-9]+$"))
            {
                New_textbox.Text = string.Empty;
            }
        }
        // Обновление
        private void Update_History_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Product_Combobox.Text) ||
                string.IsNullOrEmpty(Old_textbox.Text) ||
                string.IsNullOrEmpty(New_textbox.Text) ||
                string.IsNullOrEmpty(Reason_textbox.Text))
            {
                MessageBox.Show("Вы заполнили не все поля!", "Ошибка обновления данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int ID = 0;
                int.TryParse(Product_Combobox.Text, out ID);

                float Old = 0;
                float.TryParse(Old_textbox.Text, out Old);
                
                float New = 0;
                float.TryParse(New_textbox.Text, out New);
                
                DateTime Date = DateTime.Now;

                string Reason = Reason_textbox.Text;

                DataRowView Role_Name_Update = (DataRowView)Update_RoleName_Combobox.SelectedItem;

                if (New_for_Old == 0)
                {
                    New_for_Old = Old;
                }

                if (Reason_textbox.Text == "Нет изменений")
                {
                    MessageBox.Show("Вы не указали причину изменения!", "Ошибка при обновлении", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Обновление данных
                    history.UpdateQueryNewValues(New_for_Old, New, Date, Reason, ID);

                    // После успешного обновления можно вывести сообщение об успешном обновлении
                    MessageBox.Show("Данные товара успешно обновлены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Обновление полей
                    Update_ComboBoxes_DataGrids();

                    // Отчистка полей
                    Clear_Add_Fields();
                }
            }
        }
        // Поиск
        private void Find_in_History_button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Write_to_Find_in_History_textbox.Text))
            {
                var results = history.FindByReasonAndPName(Write_to_Find_in_History_textbox.Text);

                if (results.Any())
                {
                    Histories_DataGrid.ItemsSource = results;
                }
                else
                {
                    MessageBox.Show("Данные по вашему запросу не найдены!\nПопробуйте снова.", "Отсутсвие данных в базе данных", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear_Add_Fields();
                }
            }
            else
            {
                Update_ComboBoxes_DataGrids();
            }
        }



        /////////////////////////////////////////////////////////////////////////
        ////////////////////// Методы для сокрытия элементов ////////////////////
        /////////////////////////////////////////////////////////////////////////
        /// Visible
        private void Open_Table_Users_Visible()
        {
            //DataGrid
            Histories_of_Changes_Prices_of_Products_DataGrid.Visibility = Visibility.Hidden;
            Users_DataGrid.Visibility = Visibility.Visible;
            Roles_DataGrid.Visibility = Visibility.Visible;

            //Delete Users
            Delete_Users_textblock.Visibility = Visibility.Visible;
            Select_users_delete_combobox.Visibility = Visibility.Visible;
            Delete_users_button.Visibility = Visibility.Visible;

            //Add Users
            Add_textblock.Visibility = Visibility.Visible;
            Add_Role_textblock.Visibility = Visibility.Visible;
            Add_username_textblock.Visibility = Visibility.Visible;
            Add_Password_textblock.Visibility = Visibility.Visible;

            Add_Select_Role_Combobox.Visibility = Visibility.Visible;
            Add_Write_Username_textbox.Visibility = Visibility.Visible;
            Add_Write_Password_PasswordBox.Visibility = Visibility.Visible;

            Add_Users_button.Visibility = Visibility.Visible;

            // Update Users
            Update_textblock.Visibility = Visibility.Visible;

            Update_Username_textblock.Visibility = Visibility.Visible;
            Update_Username_textbox.Visibility = Visibility.Visible;

            Update_Email_textblock.Visibility = Visibility.Visible;
            Update_Email_textbox.Visibility = Visibility.Visible;

            Update_Password_textblock.Visibility = Visibility.Visible;
            Update_Password_textbox.Visibility = Visibility.Visible;

            Update_Name_textblock.Visibility = Visibility.Visible;
            Update_Name_textbox.Visibility = Visibility.Visible;

            Update_Surname_textblock.Visibility = Visibility.Visible;
            Update_Surname_textbox.Visibility = Visibility.Visible;

            Update_SecondName_textblock.Visibility = Visibility.Visible;
            Update_SecondName_textbox.Visibility = Visibility.Visible;

            Update_RoleName_textblock.Visibility = Visibility.Visible;
            Update_RoleName_Combobox.Visibility = Visibility.Visible;

            Update_textblock.Visibility = Visibility.Visible;
            Update_button.Visibility = Visibility.Visible;

            ID_User_from_BD.Visibility = Visibility.Visible;

            // Roles
            Role_Name_textblock.Visibility = Visibility.Visible;
            Role_Name_textbox.Visibility = Visibility.Visible;
            Delete_Role_button.Visibility = Visibility.Visible;
            Add_Role_button.Visibility = Visibility.Visible;
            Update_Role_button.Visibility = Visibility.Visible;
        }
        private void Open_Table_History_Products_Visible()
        {
            // Таблица изменения цен //
            Histories_DataGrid.Visibility = Visibility.Visible;
            Product_Combobox.Visibility = Visibility.Visible;
            Product_textblock.Visibility = Visibility.Visible;

            Old_textblock.Visibility = Visibility.Visible;
            Old_textbox.Visibility = Visibility.Visible;

            New_textblock.Visibility = Visibility.Visible;
            New_textbox.Visibility = Visibility.Visible;

            Reason_textblock.Visibility = Visibility.Visible;
            Reason_textbox.Visibility = Visibility.Visible;

            Update_History_button.Visibility = Visibility.Visible;
            Update_History_textblock.Visibility = Visibility.Visible;

            Find_in_History_button.Visibility = Visibility.Visible;
            Write_to_Find_in_History_textbox.Visibility = Visibility.Visible;
        }

        ///Hidden
        private void Initialisation_MainWindow_for_Admin_Hidden()
        {
            // DataGrids
            Users_DataGrid.Visibility = Visibility.Hidden;
            Histories_of_Changes_Prices_of_Products_DataGrid.Visibility = Visibility.Hidden;
            Roles_DataGrid.Visibility = Visibility.Hidden;

            //Delete Users
            Delete_Users_textblock.Visibility = Visibility.Hidden;
            Select_users_delete_combobox.Visibility = Visibility.Hidden;
            Delete_users_button.Visibility = Visibility.Hidden;

            //Add Users
            Add_textblock.Visibility = Visibility.Hidden;
            Add_Role_textblock.Visibility = Visibility.Hidden;
            Add_username_textblock.Visibility = Visibility.Hidden;
            Add_Password_textblock.Visibility = Visibility.Hidden;

            Add_Select_Role_Combobox.Visibility = Visibility.Hidden;
            Add_Write_Username_textbox.Visibility = Visibility.Hidden;
            Add_Write_Password_PasswordBox.Visibility = Visibility.Hidden;

            Add_Users_button.Visibility = Visibility.Hidden;

            // Update Users
            Update_textblock.Visibility = Visibility.Hidden;

            Update_Username_textblock.Visibility = Visibility.Hidden;
            Update_Username_textbox.Visibility = Visibility.Hidden;

            Update_Email_textblock.Visibility = Visibility.Hidden;
            Update_Email_textbox.Visibility = Visibility.Hidden;

            Update_Password_textblock.Visibility = Visibility.Hidden;
            Update_Password_textbox.Visibility = Visibility.Hidden;

            Update_Name_textblock.Visibility = Visibility.Hidden;
            Update_Name_textbox.Visibility = Visibility.Hidden;

            Update_Surname_textblock.Visibility = Visibility.Hidden;
            Update_Surname_textbox.Visibility = Visibility.Hidden;

            Update_SecondName_textblock.Visibility = Visibility.Hidden;
            Update_SecondName_textbox.Visibility = Visibility.Hidden;

            Update_RoleName_textblock.Visibility = Visibility.Hidden;
            Update_RoleName_Combobox.Visibility = Visibility.Hidden;

            Update_textblock.Visibility = Visibility.Hidden;
            Update_button.Visibility = Visibility.Hidden;

            ID_User_from_BD.Visibility = Visibility.Hidden;

            // Roles
            Role_Name_textblock.Visibility = Visibility.Hidden;
            Role_Name_textbox.Visibility = Visibility.Hidden;
            Delete_Role_button.Visibility = Visibility.Hidden;
            Add_Role_button.Visibility = Visibility.Hidden;
            Update_Role_button.Visibility = Visibility.Hidden;

            // Таблица изменения цен //
            Histories_DataGrid.Visibility = Visibility.Hidden;
            Product_Combobox.Visibility = Visibility.Hidden;
            Product_textblock.Visibility = Visibility.Hidden;

            Old_textblock.Visibility = Visibility.Hidden;
            Old_textbox.Visibility = Visibility.Hidden;

            New_textblock.Visibility = Visibility.Hidden;
            New_textbox.Visibility = Visibility.Hidden;

            Reason_textblock.Visibility = Visibility.Hidden;
            Reason_textbox.Visibility = Visibility.Hidden;

            Update_History_button.Visibility = Visibility.Hidden;
            Update_History_textblock.Visibility = Visibility.Hidden;

            // Найти
            Find_in_History_button.Visibility = Visibility.Hidden;
            Write_to_Find_in_History_textbox.Visibility = Visibility.Hidden;
        }

        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////
        ///////////////// Методы для отчистки и заполнении полей ////////////////
        /////////////////////////////////////////////////////////////////////////
        private void Update_ComboBoxes_DataGrids()
        {
            Users_DataGrid.ItemsSource = users.GetFullData_U_R_DU();
            Roles_DataGrid.ItemsSource = roles.GetData();

            Select_users_delete_combobox.ItemsSource = users.GetData();
            Select_users_delete_combobox.DisplayMemberPath = "User_Username";

            Add_Select_Role_Combobox.ItemsSource = roles.GetData();
            Add_Select_Role_Combobox.DisplayMemberPath = "Role_Name";

            Update_RoleName_Combobox.ItemsSource = roles.GetData();
            Update_RoleName_Combobox.DisplayMemberPath = "Role_Name";

            Histories_DataGrid.ItemsSource = history.GetDataFromDB();

            Product_Combobox.ItemsSource = history.GetDataFromDB();
            Product_Combobox.DisplayMemberPath = "ChangesP__of_Products_ID";
        }

        private void Clear_Add_Fields()
        {
            // Отчистка полей для добавления
            Add_Select_Role_Combobox.Text = string.Empty;
            Add_Write_Username_textbox.Text = string.Empty;
            Add_Write_Password_PasswordBox.Password = string.Empty;

            // Отчистка полей Роли
            Role_Name_textbox.Text = string.Empty;

            // Очтистка полей для добавления
            Update_Username_textbox.Text = string.Empty;
            Update_Email_textbox.Text = string.Empty;
            Update_Password_textbox.Password = string.Empty;
            Update_Name_textbox.Text = string.Empty;
            Update_Surname_textbox.Text = string.Empty;
            Update_SecondName_textbox.Text = string.Empty;
            Update_RoleName_Combobox.Text = string.Empty;

            ID_User_from_BD.Text = "Имя (старое)";

            // Отчистка истории
            Old_textbox.Text = string.Empty;
            New_textbox.Text = string.Empty;
            Reason_textbox.Text = string.Empty;
            Product_Combobox.SelectedItem = null;

            // Отчистка функций поиска
            Write_to_Find_in_History_textbox.Text = string.Empty;
        }
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
    }
}
