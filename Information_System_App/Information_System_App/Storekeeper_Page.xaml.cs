using Information_System_App.Information_System_Of_DNS_DataSetTableAdapters;
using System;
using System.Collections.Generic;
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

namespace Information_System_App
{
    public partial class Storekeeper_Page : Window
    {
        public Storekeeper_Page()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Open_Table_Stock_Products_Categories_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Storekeeper_Stock_Products_Categories_Page();
        }

        private void Open_Table_Supplys_Suppliers_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Storekeeper_Supplys_Suppliers_Page();
        }
    }
}
