using Information_System_App.Information_System_Of_DNS_DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Information_System_App
{
    public partial class Cashier_Cash_Page : Page
    {
        public StockTableAdapter stock = new StockTableAdapter();
        public Products_CategoriesTableAdapter categorys = new Products_CategoriesTableAdapter();
        public ProductsTableAdapter products = new ProductsTableAdapter();
        public Datas_for_ReceiptTableAdapter receipt = new Datas_for_ReceiptTableAdapter();
        public Receipts_and_ProductsTableAdapter receipts_and_products = new Receipts_and_ProductsTableAdapter();
        public Cashier_Cash_Page()
        {
            InitializeComponent();

            Update_Fields();
            // 0 3 4 6 7
            Table_Products_On_Stock_Datagrid.Columns[0].Visibility = Visibility.Collapsed;
            Table_Products_On_Stock_Datagrid.Columns[3].Visibility = Visibility.Collapsed;
            Table_Products_On_Stock_Datagrid.Columns[4].Visibility = Visibility.Collapsed;
            Table_Products_On_Stock_Datagrid.Columns[6].Visibility = Visibility.Collapsed;
            Table_Products_On_Stock_Datagrid.Columns[7].Visibility = Visibility.Collapsed;

            // 0 1
            Table_Products_On_Receipt_Datagrid.Columns[0].Visibility = Visibility.Collapsed;
            Table_Products_On_Receipt_Datagrid.Columns[2].Visibility = Visibility.Collapsed;
        }

        // Stock + Products
        public int Product_ID;
        public string Product_Name;
        public float Product_Price;
        public int Stock_Quantity;
        public string Product_Category_Name;

        // Receipts + Products
        public int ID_Receipt;
        // public string Product_Name; Объявлена выше

        // Data_for_receipt
        public float Receipt_Full_Price = 0;
        public float Receipt_Buyer_Amount = 0;
        public float Receipt_Change = 0;
        public DateTime Receipt_Date = DateTime.Now;

        public int Receipts_and_Products_ID;
        //public string ID_Receipt;
        public int ID_Product;
        public int Quantity_from_Stock;

        public int MaxID;


        private void UpdateReceiptSumTextBlock()
        {
            float totalSum = 0;
            foreach (DataRowView row in Table_Products_On_Receipt_Datagrid.Items)
            {
                float productPrice = (float)row["Product_Price"];
                int quantity = (int)row["Quantity_from_Stock"];
                totalSum += productPrice * quantity; // Умножаем цену на количество
            }
            Receipt_And_Sum_textblock.Text = "Сумма для оплаты покупки: " + totalSum.ToString("C2");
        }



        private void Table_Products_On_Stock_Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Table_Products_On_Stock_Datagrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Table_Products_On_Stock_Datagrid.SelectedItem;
                Product_ID = ((int)row["Product_ID"]);
                Product_Name = (row["Product_Name"].ToString());
                Product_Price = ((float)row["Product_Price"]);
                Stock_Quantity = ((int)row["Stock_Quantity"]);
                Product_Category_Name = (row["Product_Category_Name"].ToString());
            }
        }

        private void Add_Product_to_Receipt_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Product_Name))
            {
                MessageBox.Show("Вы не выбрали товар, чтобы его добавить!\nПопробуйте снова.", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (Table_Products_On_Receipt_Datagrid.Items.Count == 0)
                {
                    receipt.InsertQuery(0, 0, 0, DateTime.Now);
                }

                foreach (DataRowView row in Table_Products_On_Receipt_Datagrid.Items)
                {

                    int Product_ID_In_Receipt = (int)row["ID_Product"];
                    int MaxQuantity = (int)stock.ScalarQuery(Product_ID);
                    if (Product_ID == Product_ID_In_Receipt)
                    {
                        if (MaxQuantity == 0)
                        {
                            MessageBox.Show("Данный товар закончился!", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        receipts_and_products.UpdateQuantityAdd(Product_ID, (int)receipt.ScalarQuery());
                        stock.UpdateQueryAfterAddToReceipt(Product_ID);

                        MaxID = (int)receipts_and_products.ScalarQuery();
                        Table_Products_On_Receipt_Datagrid.ItemsSource = receipts_and_products.GetDataByReceipt_ID(MaxID);
                        Table_Products_On_Stock_Datagrid.ItemsSource = products.GetFullData();
                        UpdateReceiptSumTextBlock();
                        return;
                    }
                }

                // Добавляем товар в чек, если его там нет
                receipts_and_products.InsertQuery((int)receipt.ScalarQuery(), Product_ID, 1);
                stock.UpdateQueryAfterAddToReceipt(Product_ID);

                MaxID = (int)receipts_and_products.ScalarQuery();
                Table_Products_On_Receipt_Datagrid.ItemsSource = receipts_and_products.GetDataByReceipt_ID(MaxID);
                Table_Products_On_Stock_Datagrid.ItemsSource = products.GetFullData();

                UpdateReceiptSumTextBlock();
            }
        }



        /// Удаление товара из чека ///
        private void Table_Products_On_Receipt_Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Table_Products_On_Receipt_Datagrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)Table_Products_On_Receipt_Datagrid.SelectedItem;
                ID_Product = (int)row["ID_Product"];
                Receipts_and_Products_ID = (int)row["Receipts_and_Products_ID"];
                ID_Receipt = (int)row["ID_Receipt"];
                Quantity_from_Stock = ((int)row["Quantity_from_Stock"]);
                Product_Name = row["Product_Name"].ToString();
            }
        }
        private void Delete_Product_from_Receipt_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Product_Name))
            {
                MessageBox.Show("Вы не выбрали товар, чтобы его удалить!\nПопробуйте снова.", "Ошибка при удалении", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Добавляем оператор return, чтобы прервать выполнение метода
            }

            foreach (DataRowView row in Table_Products_On_Receipt_Datagrid.Items)
            {
                int Product_ID_In_Receipt = (int)row["ID_Product"];
                if (ID_Product == Product_ID_In_Receipt)
                {
                    if ((int)row["Quantity_from_Stock"] == 1)
                    {
                        MaxID = (int)receipts_and_products.ScalarQuery();
                        receipts_and_products.DeleteQuery(ID_Product);
                    }
                    else
                    {
                        receipts_and_products.UpdateQuantityDelete(ID_Product);
                    }

                    stock.UpdateQueryAfterDeleteToReceipt(ID_Product);


                    Table_Products_On_Receipt_Datagrid.ItemsSource = receipts_and_products.GetDataByReceipt_ID(MaxID);
                    Table_Products_On_Stock_Datagrid.ItemsSource = products.GetFullData();

                    UpdateReceiptSumTextBlock();
                    return; // Добавляем оператор return, чтобы прервать выполнение цикла после удаления товара
                }
            }
        }











        private void Buy_button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Clients_Money_TextBox.Text))
            {
                MessageBox.Show("Покупатель не заплатил за заказ!", "Ошибка при добавлении", MessageBoxButton.OK, MessageBoxImage.Error);
                // Очистка полей
                Clear_Fields();
            }
            else
            {
                // Получение данных чека
                var receiptData = receipts_and_products.GetDataForReceiptProductTable();

                // Получение данных о продуктах из таблицы Products
                var productsData = products.GetFullData(); // Предполагается, что у вас есть метод GetData для извлечения данных из таблицы Products

                // Формирование строки для текстового файла
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("\t\t\t\tСпасибо за покупку в DNS!\n\t\t\t\t   Приходите к нам ещё!");
                sb.AppendLine("\t\t\t\t     Кассовый чек №" + receipt.ScalarQuery() + "\n");

                // Создайте переменную для хранения общей суммы
                float totalAmount = 0;


                // Получите максимальный идентификатор чека
                MaxID = (int)receipts_and_products.ScalarQuery();

                // Извлеките данные о продуктах только для чека с максимальным идентификатором
                var productsDataForMaxReceipt = receiptData.Where(p => p.ID_Receipt == MaxID);

                // Посчитайте общую стоимость заказа для чека с максимальным идентификатором
                float totalAmountForMaxReceipt = 0;
                foreach (var product in productsDataForMaxReceipt)
                {
                    var productInfo = productsData.FirstOrDefault(p => p.Product_ID == product.ID_Product);
                    if (productInfo != null)
                    {
                        // Добавьте цену, умноженную на количество, к общей сумме для чека с максимальным идентификатором
                        float totalPrice = productInfo.Product_Price * product.Quantity_from_Stock;
                        totalAmountForMaxReceipt += totalPrice;
                        sb.AppendLine("\t\t\t" + productInfo.Product_Name + " - " + product.Quantity_from_Stock + " шт. - " + productInfo.Product_Price + " руб. (Итого: " + totalPrice + " руб.)");
                    }
                }



                // Расчет сдачи
                if (float.TryParse(Clients_Money_TextBox.Text, out float amountPaid))
                {
                    if (amountPaid < totalAmountForMaxReceipt)
                    {
                        MessageBox.Show("Недостаточно средств для оплаты!", "Ошибка при оплате", MessageBoxButton.OK, MessageBoxImage.Error);
                        // Очистка полей
                        Clear_Fields();
                    }
                    else
                    {
                        float change = amountPaid - totalAmountForMaxReceipt;  // Расчет сдачи для чека с максимальным идентификатором

                        // Посчитайте общую сумму заказа
                        totalAmount = totalAmountForMaxReceipt;

                        // Добавьте строку с итоговой суммой заказа в чек
                        sb.AppendLine("\nИтого к оплате: " + totalAmount + " руб.");

                        // Добавление строки с внесенной суммой в чек
                        sb.AppendLine("Внесено: " + amountPaid);

                        // Добавление строки с расчетом сдачи в чек
                        sb.AppendLine("Сдача: " + change);


                        // Получение пути к рабочему столу
                        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

                        receipt.UpdateQuery(totalAmount, change, amountPaid);

                        // Создание текстового файла и запись в него
                        string fileName = "Чек №" + receipt.ScalarQuery() + ".txt";
                        string filePath = System.IO.Path.Combine(desktopPath, fileName);
                        System.IO.File.WriteAllText(filePath, sb.ToString());

                        // Очистка полей
                        Clear_Fields();

                        Receipt_And_Sum_textblock.Text = "Сумма для оплаты покупки: ";

                        Table_Products_On_Receipt_Datagrid.ItemsSource = null;
                    }
                }

                else
                {
                    MessageBox.Show("Введена некорректная сумма!", "Ошибка при оплате", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Очистка полей
                    Clear_Fields();
                }

            }
        }



        /////////////////////////////////////////////////////////////////////////
        ///////////// Методы для обновления полей и валидации ///////////////////
        /////////////////////////////////////////////////////////////////////////
        private void Update_Fields()
        {
            Table_Products_On_Stock_Datagrid.ItemsSource = products.GetFullData();
        }
        private void Clear_Fields()
        {
            Clients_Money_TextBox.Text = string.Empty;
        }

        // Валидация //
        private void Clients_Money_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Clients_Money_TextBox.Text, "^[0-9 ,]+$"))
            {
                Clients_Money_TextBox.Text = string.Empty;
            }
        }
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
    }
}
