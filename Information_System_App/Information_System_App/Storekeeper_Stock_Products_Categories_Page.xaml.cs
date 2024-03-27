using Information_System_App.Information_System_Of_DNS_DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Information_System_App
{
    public partial class Storekeeper_Stock_Products_Categories_Page : Page
    {
        public SupplysTableAdapter supplys = new SupplysTableAdapter();
        public SuppliersTableAdapter suppliers = new SuppliersTableAdapter();
        public Histories_of_Changes_In_Prices_of_ProductsTableAdapter history = new Histories_of_Changes_In_Prices_of_ProductsTableAdapter();
        public StockTableAdapter stock = new StockTableAdapter();
        public Products_CategoriesTableAdapter categorys = new Products_CategoriesTableAdapter();
        public ProductsTableAdapter products = new ProductsTableAdapter();
        public Storekeeper_Stock_Products_Categories_Page()
        {
            InitializeComponent();
            Full_Table_Datagrid.ItemsSource = stock.GetDataStock_Products_Category();

            Full_Table_Datagrid.Columns[0].Visibility = Visibility.Collapsed;
            Full_Table_Datagrid.Columns[2].Visibility = Visibility.Collapsed;
            Full_Table_Datagrid.Columns[5].Visibility = Visibility.Collapsed;
            Full_Table_Datagrid.Columns[6].Visibility = Visibility.Collapsed;
            Full_Table_Datagrid.Columns[8].Visibility = Visibility.Collapsed;
            Full_Table_Datagrid.Columns[9].Visibility = Visibility.Collapsed;
            Full_Table_Datagrid.Columns[10].Visibility = Visibility.Collapsed;
            Full_Table_Datagrid.Columns[11].Visibility = Visibility.Collapsed;

            Categorys_DataGrid.ItemsSource = categorys.GetCategories();
            Categorys_DataGrid.Columns[0].Visibility = Visibility.Collapsed;

            Update_Fields();
        }

        public string QP;
        public string Price;
        public string Product_Name_Update;
        public string Suppliers_Products_Cahnged;
        private void Full_Table_Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Full_Table_Datagrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Full_Table_Datagrid.SelectedItem;
                Category_Products_Combobox.Text = row["Product_Category_Name"].ToString();
                ID_Product_Products_combobox.Text = row["ID_Product"].ToString();
                Product_Name_Update = Product_Name_Products_textbox.Text = row["Product_Name"].ToString();
                Price = Price_Products_textbox.Text = row["Product_Price"].ToString();
                QP = Quantity_Products_textbox.Text = row["Stock_Quantity"].ToString();
                Suppliers_Products_Cahnged = Suppliers_Products_combobox.Text = row["Supplier_Name"].ToString();
            }
        }
        private void Delete_Products_button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение поля
            if (ID_Product_Products_combobox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали продукт для удаления!\nПопробуйте снова.", "Ошибка при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DataRowView _ID_to_Delete = (DataRowView)ID_Product_Products_combobox.SelectedItem;
                int ID_to_Delete = Convert.ToInt32(_ID_to_Delete["Product_ID"]);
                stock.DeleteProduct(ID_to_Delete);
                supplys.DeleteProduct(ID_to_Delete);
                history.DeleteProduct(ID_to_Delete);
                products.DeleteProduct(ID_to_Delete);

                // Обновление полей
                Update_Fields();
            }  
        }
        private void Add_Products_button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на заполнение поля
            if (string.IsNullOrEmpty(Category_Products_Combobox.Text) ||
                string.IsNullOrEmpty(Product_Name_Products_textbox.Text) ||
                string.IsNullOrEmpty(Price_Products_textbox.Text) ||
                string.IsNullOrEmpty(Quantity_Products_textbox.Text) ||
                string.IsNullOrEmpty(Suppliers_Products_combobox.Text))
            {
                MessageBox.Show("Вы не заполнили поле!\nПопробуйте снова.", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Отчистка полей
                Clear_Fields();
            }
            else
            {
                // Функция добавления продукта (продукт)
                float price = 0;
                float.TryParse(Price_Products_textbox.Text, out price);
                DataRowView Category_Name_Update = (DataRowView)Category_Products_Combobox.SelectedItem;
                int Category_ID = ((int)Category_Name_Update["Product_Category_ID"]);
                products.InsertQueryProducts(Product_Name_Products_textbox.Text, price, Category_ID);


                int Quantity = 0;
                int.TryParse(Quantity_Products_textbox.Text, out Quantity);
                stock.UpdateQueryForUpdateAfterInsertProduct(Quantity);

                DataRowView Supplier_Name_Update = (DataRowView)Suppliers_Products_combobox.SelectedItem;
                int Supplier_ID = ((int)Supplier_Name_Update["Supplier_ID"]);
                supplys.UpdateQueryForUpdateAfterInsertProduct(Quantity, Supplier_ID);


                //Отчистка полей
                Clear_Fields();

                // Обновление полей
                Update_Fields();
            }
        }
        private void Update_Products_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Category_Products_Combobox.Text) ||
                string.IsNullOrEmpty(Product_Name_Products_textbox.Text) ||
                string.IsNullOrEmpty(Price_Products_textbox.Text) ||
                string.IsNullOrEmpty(Quantity_Products_textbox.Text) ||
                string.IsNullOrEmpty(Suppliers_Products_combobox.Text))
            {
                MessageBox.Show("Вы не заполнили поле!\nПопробуйте снова.", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Отчистка полей
                Clear_Fields();
            }
            else
            {
                DataRowView Category_Name_Update = (DataRowView)Category_Products_Combobox.SelectedItem;
                int Category_ID = ((int)Category_Name_Update["Product_Category_ID"]);

                DataRowView _ID_to_Update = (DataRowView)ID_Product_Products_combobox.SelectedItem;
                int ID_to_Update = Convert.ToInt32(_ID_to_Update["Product_ID"]);

                float price = 0;
                float.TryParse(Price_Products_textbox.Text, out price);

                DataRowView Supplier_Name_Update = (DataRowView)Suppliers_Products_combobox.SelectedItem;
                string Supplier_ID = Supplier_Name_Update["Supplier_Name"].ToString();
                if (Suppliers_Products_Cahnged != Supplier_ID)
                {
                    MessageBox.Show("Вы пытаетесь изменить поставщика!\nПопробуйте произвести обновление снова.", "Логическое несаглосование для обновления", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                int Quantity = 0;
                int.TryParse(Quantity_Products_textbox.Text, out Quantity);
                if (Quantity_Products_textbox.Text !=  QP)
                {
                    MessageBox.Show("Нельзя изменить количество продукта на складе, так как есть данные о количестве в поставке!\nПопробуйте произвести обновление снова.", "Логическое несаглосование для обновления", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (Product_Name_Products_textbox.Text != Product_Name_Update)
                {
                    MessageBox.Show("Вы не можете изменить имя продукта, так как данный продукт уже был поставлен в магазин!\nПопробуйте произвести обновление снова.", "Логическое несаглосование для обновления", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                if (Price_Products_textbox.Text != Price)
                {
                    MessageBox.Show("Вы не можете обновить цену, так как не было поставок!\nПопробуйте произвести обновление снова.", "Логическое несаглосование для обновления", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                products.UpdateQuery(Category_ID, ID_to_Update); 
                
                

                //Отчистка полей
                Clear_Fields();

                // Обновление полей
                Update_Fields();
            }
        }



        // Категории
        public int Category_ID;
        public string Category_Name_Old;
        private void Categorys_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Categorys_DataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Categorys_DataGrid.SelectedItem;
                Category_Name_Old = Name_Category_textbox.Text = row["Product_Category_Name"].ToString();
                Category_ID = (int)row["Product_Category_ID"];
            }

        }
        private void Delete_Category_button_Click(object sender, RoutedEventArgs e)
        {
            // Получение количества пользователей, использующих эту роль
            int userCount = (int)products.ScalarQuery(Category_ID);

            if (string.IsNullOrEmpty(Name_Category_textbox.Text))
            {
                MessageBox.Show("Вы не выбрали категорию для удаления!\nПопробуйте снова.", "Ошибка при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Fields();
            }
            // Если есть пользователи, использующие эту роль, выдать ошибку
            else if (userCount > 0)
            {
                MessageBox.Show("Невозможно удалить эту категорию, так как в ней есть информация о продуктах!", "Ошибка внешних ключей при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Отчистка полей
                Clear_Fields();
            }
            else
            {
                categorys.DeleteQuery(Name_Category_textbox.Text);
                Update_Fields();
                Clear_Fields();
            }   
        }
        private void Add_Category_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name_Category_textbox.Text))
            {
                MessageBox.Show("Вы не ввели название категории для добавления!\nПопробуйте снова.", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Fields();
            }
            else
            {
                categorys.InsertQuery(Name_Category_textbox.Text);
                Update_Fields();
                Clear_Fields();
            }
        }
        private void Update_Category_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name_Category_textbox.Text))
            {
                MessageBox.Show("Вы не ввели новое название категории для обновления!\nПопробуйте снова.", "Ошибка при обновлении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Fields();
            }
            else
            {
                categorys.UpdateQuery(Name_Category_textbox.Text, Category_Name_Old);
                Update_Fields();
                Clear_Fields();
            }
        }
        private void Import_button_Click(object sender, RoutedEventArgs e)
        {
            List<categoryModel> ImportCategorys = Converter.DeserializeObject<List<categoryModel>>();
            foreach (var item in ImportCategorys)
            {
                categorys.InsertQuery(item.Product_Category_Name);
            }
            Import_button.IsEnabled = false;
            Update_Fields();
        }


        /////////////////////////////////////////////////////////////////////////
        ///////////// Методы для обновления полей и валидации ///////////////////
        /////////////////////////////////////////////////////////////////////////
        private void Update_Fields()
        {
            Full_Table_Datagrid.ItemsSource = stock.GetDataStock_Products_Category();

            Category_Products_Combobox.ItemsSource = categorys.GetData();
            Category_Products_Combobox.DisplayMemberPath = "Product_Category_Name";

            Suppliers_Products_combobox.ItemsSource = suppliers.GetData();
            Suppliers_Products_combobox.DisplayMemberPath = "Supplier_Name";

            ID_Product_Products_combobox.ItemsSource = products.GetData();
            ID_Product_Products_combobox.DisplayMemberPath = "Product_ID";

            Categorys_DataGrid.ItemsSource = categorys.GetCategories();
        }
        private void Clear_Fields()
        {
            Category_Products_Combobox.Text = string.Empty;
            Product_Name_Products_textbox.Text = string.Empty;
            Price_Products_textbox.Text = string.Empty;
            Quantity_Products_textbox.Text = string.Empty;
            Suppliers_Products_combobox.Text = string.Empty;
            Name_Category_textbox.Text = string.Empty;
        }

                                                   // Валидация //
        private void Price_Products_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Price_Products_textbox.Text, "^[0-9]+$"))
            {
                Price_Products_textbox.Text = string.Empty;
            }
        }

        private void Quantity_Products_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Quantity_Products_textbox.Text, "^[0-9]+$"))
            {
                Quantity_Products_textbox.Text = string.Empty;
            }
        }
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
    }
}
