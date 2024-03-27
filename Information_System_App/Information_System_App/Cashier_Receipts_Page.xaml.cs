using Information_System_App.Information_System_Of_DNS_DataSetTableAdapters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Information_System_App
{
    public partial class Cashier_Receipts_Page : Page
    {
        public Receipts_and_ProductsTableAdapter receipt_and_products = new Receipts_and_ProductsTableAdapter();
        public Datas_for_ReceiptTableAdapter receipt = new Datas_for_ReceiptTableAdapter();
        public Cashier_Receipts_Page()
        {
            InitializeComponent();

            Update_Fields();

            Table_Receipts_Datagrid.Columns[0].Visibility = Visibility.Collapsed;
            Table_Receipts_Datagrid.Columns[2].Visibility = Visibility.Collapsed;
            Table_Receipts_Datagrid.Columns[5].Visibility = Visibility.Collapsed;
        }

        private void Receipt_Number_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(Receipt_Number_TextBox.Text, "^[0-9]+$"))
            {
                Receipt_Number_TextBox.Text = string.Empty;
            }
        }
        private void Find_by_button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Receipt_Number_TextBox.Text))
            {
                int result = 0;
                if (int.TryParse(Receipt_Number_TextBox.Text, out result))
                {
                    Table_Receipts_Datagrid.ItemsSource = receipt_and_products.GetDataBy6(result);
                }
                if (Table_Receipts_Datagrid.ItemsSource == null)
                {
                    MessageBox.Show("Введенное значение не является корректным номером чека.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Clear_Fields();
                }
            }
            else
            {
                MessageBox.Show("Введите номер чека для поиска.", "Пустое поле", MessageBoxButton.OK, MessageBoxImage.Information);
                Clear_Fields();
            }
            Clear_Fields();
        }



        private void Export_Receipt_button_Click(object sender, RoutedEventArgs e)
        {
            if (Table_Receipts_Datagrid.SelectedItem != null)
            {
                var selectedRow = (System.Data.DataRowView)Table_Receipts_Datagrid.SelectedItem;
                var id = (int)selectedRow["Receipts_and_Products_ID"];
                var receiptId = (int)selectedRow["ID_Receipt"];
                var productId = (int)selectedRow["ID_Product"];
                var quantity = (int)selectedRow["Quantity_from_Stock"];
                var selectedReceipt = new Receipts_and_Products(id, receiptId, productId, quantity);

                try
                {
                    // Преобразование выбранного элемента в формат JSON
                    string json = JsonConvert.SerializeObject(selectedReceipt, Formatting.Indented);

                    // Сохранение JSON в файл
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string filePath = System.IO.Path.Combine(desktopPath, $"receipt_{selectedReceipt.Receipts_and_Products_ID}.json"); // Предполагается, что у вашего объекта есть свойство Receipts_and_Products_ID
                    System.IO.File.WriteAllText(filePath, json);

                    MessageBox.Show($"Данные успешно экспортированы в файл receipt_{selectedReceipt.Receipts_and_Products_ID}.json на рабочем столе.", "Экспорт завершен", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при экспорте данных в JSON: " + ex.Message, "Ошибка экспорта", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите чек для экспорта.", "Отсутствует выбранный чек", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }




        private void Delete_by_button_Click(object sender, RoutedEventArgs e)
        {
            if (Table_Receipts_Datagrid.SelectedItem != null)
            {
                var selectedRow = (System.Data.DataRowView)Table_Receipts_Datagrid.SelectedItem;
                var receiptId = (int)selectedRow["ID_Receipt"];

                try
                {
                    receipt_and_products.DeleteQueryID_Receipt(receiptId);
                    receipt.DeleteQueryID_Receipt(receiptId);
                    MessageBox.Show("Данные успешно удалены.", "Удаление завершено", MessageBoxButton.OK, MessageBoxImage.Information);
                    Update_Fields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при удалении данных: " + ex.Message, "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите чек для удаления.", "Отсутствует выбранный чек", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }




        /////////////////////////////////////////////////////////////////////////
        ///////////// Методы для обновления полей и валидации ///////////////////
        /////////////////////////////////////////////////////////////////////////
        private void Update_Fields()
        {
            Table_Receipts_Datagrid.ItemsSource = receipt_and_products.GetDataBy5();
        }
        private void Clear_Fields()
        {
            Receipt_Number_TextBox.Text = string.Empty;
        }
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////
    }
}
