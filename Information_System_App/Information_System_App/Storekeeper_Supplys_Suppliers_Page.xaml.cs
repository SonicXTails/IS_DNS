using Information_System_App.Information_System_Of_DNS_DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class Storekeeper_Supplys_Suppliers_Page : Page
    {
        private SuppliersTableAdapter suppliers = new SuppliersTableAdapter();
        private SupplysTableAdapter supplys = new SupplysTableAdapter();
        private ProductsTableAdapter products = new ProductsTableAdapter();
        private StockTableAdapter stock = new StockTableAdapter();
        private Histories_of_Changes_In_Prices_of_ProductsTableAdapter history = new Histories_of_Changes_In_Prices_of_ProductsTableAdapter();
        public Storekeeper_Supplys_Suppliers_Page()
        {
            InitializeComponent();

            Update_Fields();

            Table_Suppliers_Datagrid.Columns[0].Visibility = Visibility.Collapsed;
            Table_Supplys_Datagrid.Columns[0].Visibility = Visibility.Collapsed;
            Table_Supplys_Datagrid.Columns[3].Visibility = Visibility.Collapsed;
            Table_Supplys_Datagrid.Columns[4].Visibility = Visibility.Collapsed;
        }

        public int ID_Supplier;
        public string Supplier_Name;
        private void Table_Suppliers_Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Table_Suppliers_Datagrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Table_Suppliers_Datagrid.SelectedItem;
                Supplier_Name = Name_Supplier_textbox.Text = row["Supplier_Name"].ToString();
                Info_Supplier_textbox.Text = row["Supplier_Info"].ToString();
                Address_Supplier_textbox.Text = row["Supplier_Address"].ToString();

                ID_Supplier = (int)row["Supplier_ID"];
            }
        }
        private void Delete_Supply_button_Click(object sender, RoutedEventArgs e)
        {
            int Suppliers_ID_With_Selected_Supply = (int)supplys.ScalarQuery(ID_Supplier);

            if (string.IsNullOrEmpty(Name_Supplier_textbox.Text))
            {
                MessageBox.Show("Вы не заполнили поле!\nПопробуйте снова.", "Ошибка при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Fields();
            }
            if (Suppliers_ID_With_Selected_Supply > 0)
            {
                MessageBox.Show("Невозможно удалить этого поставщика, так как его данные используются в других таблицах!", "Ошибка внешних ключей при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Fields();
            }
            else
            {
                string Supply_Name = Name_Supplier_textbox.Text;
                suppliers.DeleteQuery(Name_Supplier_textbox.Text);

                Update_Fields();
                Clear_Fields();
            }
        }
        private void Add_Supplier_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name_Supplier_textbox.Text) ||
                string.IsNullOrEmpty(Info_Supplier_textbox.Text) ||
                string.IsNullOrEmpty(Address_Supplier_textbox.Text))
            {
                MessageBox.Show("Вы не заполнили все необходимые поля!\nПопробуйте снова.", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Fields();
            }
            else
            {
                suppliers.InsertQuery(Name_Supplier_textbox.Text, Info_Supplier_textbox.Text, Address_Supplier_textbox.Text);

                Update_Fields();
                Clear_Fields();
            }
        }
        private void Update_Supplier_button_Click(object sender, RoutedEventArgs e)
        {
            int Suppliers_ID_With_Selected_Supply = (int)supplys.ScalarQuery(ID_Supplier);

            if (string.IsNullOrEmpty(Name_Supplier_textbox.Text) ||
                string.IsNullOrEmpty(Info_Supplier_textbox.Text) ||
                string.IsNullOrEmpty(Address_Supplier_textbox.Text))
            {
                MessageBox.Show("Вы не заполнили поле!\nПопробуйте снова.", "Ошибка при обновлении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Fields();
            }
            else
            {
                suppliers.UpdateQuery(Name_Supplier_textbox.Text, Info_Supplier_textbox.Text, Address_Supplier_textbox.Text, Supplier_Name);
                Clear_Fields();
                Update_Fields();
            }
        }




        public int Quantity_Supply;
        public DateTime Date_Supply;
        public int ID_Product;
        private void Table_Supplys_Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Table_Supplys_Datagrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Table_Supplys_Datagrid.SelectedItem;
                Quantity_Supply = (int)row["Supply_Quantity"];
                Quantity_Supply_textbox.Text = row["Supply_Quantity"].ToString();

                Date_Supply = (DateTime)row["Supply_Date"];
                Date_Supply_textbox.Text = Date_Supply.ToString();

                ID_Supplier = (int)row["ID_Supplier"];

                ID_Product = (int)row["ID_Product"];
                ID_Product_Products_combobox.Text = ID_Product.ToString();

            }
        }

        private void Delete_Supply_button_Click_1(object sender, RoutedEventArgs e)
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
                history.DeleteProduct(ID_to_Delete);
                supplys.DeleteProduct(ID_to_Delete);
                products.DeleteProduct(ID_to_Delete);

                // Обновление полей
                Update_Fields();
            }
        }

        private void Update_Supply_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Quantity_Supply_textbox.Text) ||
                string.IsNullOrEmpty(Date_Supply_textbox.Text) ||
                string.IsNullOrEmpty(ID_Product_Products_combobox.Text))
            {
                MessageBox.Show("Вы не выбрали поле для обновления!\nПопробуйте снова.", "Ошибка при обновлении", MessageBoxButton.OK, MessageBoxImage.Error);
                Clear_Fields();
            }
            else
            {
                DataRowView _ID_to_Update = (DataRowView)ID_Product_Products_combobox.SelectedItem;
                int ID_to_Update = Convert.ToInt32(_ID_to_Update["Product_ID"]);

                int Quantity = 0;
                int.TryParse(Quantity_Supply_textbox.Text, out Quantity);

                supplys.UpdateQuery(Quantity, ID_Supplier, ID_Product);
                stock.UpdateQuery(Quantity, ID_Product);

                //Отчистка полей
                Clear_Fields();

                // Обновление полей
                Update_Fields();
            }
        }


        private void Import_button_Click(object sender, RoutedEventArgs e)
        {
            List<SuppliersModel> ImportCategorys = Converter.DeserializeObject<List<SuppliersModel>>();
            foreach (var item in ImportCategorys)
            {
                suppliers.InsertQuery(item.Supplier_Name, item.Supplier_Info, item.Supplier_Address);
            }

            Update_Fields();
        }
        /////////////////////////////////////////////////////////////////////////
        ///////////// Методы для обновления полей и валидации ///////////////////
        /////////////////////////////////////////////////////////////////////////
        private void Update_Fields()
        {
            Table_Suppliers_Datagrid.ItemsSource = suppliers.GetDataSuppliers();
            Table_Supplys_Datagrid.ItemsSource = supplys.GetDataSupplys();

            ID_Product_Products_combobox.ItemsSource = products.GetData();
            ID_Product_Products_combobox.DisplayMemberPath = "Product_ID";
        }
        private void Clear_Fields()
        {
            Name_Supplier_textbox.Text = string.Empty;
            Info_Supplier_textbox.Text = string.Empty;
            Address_Supplier_textbox.Text= string.Empty;
        }

        private void Quantity_Supply_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Quantity_Supply_textbox.Text, "^[0-9]+$"))
            {
                Quantity_Supply_textbox.Text = string.Empty;
            }
        }
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
    }
}
