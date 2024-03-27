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
    public partial class Сashier_Page : Window
    {
        public Сashier_Page()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Open_Cash_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Cashier_Cash_Page();
        }

        private void Open_Historys_of_Receipts_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Cashier_Receipts_Page();
        }
    }
}
