using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information_System_App
{
    public class Receipts_and_Products
    {
        public int Receipts_and_Products_ID { get; set; }
        public int ID_Receipt { get; set; }
        public int ID_Product { get; set; }
        public int Quantity_from_Stock { get; set; }
        // Добавьте свойства для других полей, если это необходимо

        // Конструктор по умолчанию
        public Receipts_and_Products() { }

        // Конструктор, который принимает все поля
        public Receipts_and_Products(int id, int receiptId, int productId, int quantity)
        {
            Receipts_and_Products_ID = id;
            ID_Receipt = receiptId;
            ID_Product = productId;
            Quantity_from_Stock = quantity;
        }
    }

}
