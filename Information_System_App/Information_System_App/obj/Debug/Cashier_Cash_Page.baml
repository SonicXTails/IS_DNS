   M S B A M L   `   `   `  ����Q  MInformation_System_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Information_System_AppInformation_System_App  5      .    (Information_System_App.Cashier_Cash_Page   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:Information_System_App     5        N� Title$3  -Добро пожаловать Кассир!��5      $	��850��6(   $
��1600��65   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5       6   �� 5!      6   �� 5"      6   �� 5#      6   �� 5$      6   �� 5%      6   .+�5)   
   �� -   5         ��Name$ 
BackGround��5*      $��15��5+      $��1��5,      $��9��6   
�������6+   &   System.Windows.Controls.DataGrid  5.   
   -   5          Name$&  Table_Products_On_Stock_Datagrid��5/      $��2��50      $��6��6   $��1��51      $��5��52         CanUserAddRows .     CanUserDeleteRows .  
�������   CanUserReorderColumns .     AutoGenerateColumns .  $��Bahnschrift SemiLight)�58      $
��Bold"�60   	   Columns	 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn
   
IsReadOnly
 .    Header$! Номер продукта��67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6O   �� *
Product_ID+  5;      
 . $' !Название продукта��67    6R   �� *Product_Name+  5<      
 . $ Цена (руб)��67    6K   �� *Product_Price+  5=      
 . $# Номер категории��67    6P   �� *ID_Product_Category+  5>      
 . $( "Номер полки склада��67    6S   �� *
Stock_ID+  5?      
 . $ Количество��67    6K   �� *Stock_Quantity+  5@      
 . $! Номер продукта��67    6O   �� *
ID_Product+  5A      
 . $# Номер категории��67    6P   �� *Product_Category_ID+  5B      
 . $ Категория��67    6J   �� *Product_Category_Name+5D        5F   
   -   5        $( "Table_Products_On_Receipt_Datagrid��5G      $��2��5H      $��6��6   $��8��5I      $��5��5J       .   .  
������� .   .  $��Bahnschrift SemiLight)�5P      $
��Bold"�60   	 5Q        5R      
 . $ 
Номер��67    6F   �� *Receipts_and_Products_ID+  5S      
 . $ Номер чека��67    6K   �� *
ID_Receipt+  5T      
 . $! Номер продукта��67    6O   �� *
ID_Product+  5U      
 . $ Количество��67    6K   �� *Quantity_from_Stock+  5V      
 . $ Название��67    6I   �� *Product_Name+  5W      
 . $' !Цена за штуку (руб)��67    6T   �� *Product_Price+5Y      �� 5[   
   -   5         ��Name$# Add_Product_to_Receipt_button��5\      $��7��5]      $��1��6   $��3��5^      $��25(�5_      $��Center=�5`      $��Center=�5a      $��+��5b      $��Bahnschrift SemiLight)�5c      $
��Bold"�60   
�������6U   �� 5e   
   -   5        $( "Delete_Product_from_Receipt_button��5f      $��7��5g      $��1��6   $��5��5h      $��25(�5i      $��-��5j      $��Center=�5k      $��Center=�5l      $��Bahnschrift SemiLight)�5m      $
��Bold"�60   
�������6U   �� 5p   
   -   5        $ 
Buy_button��5q      $��12��5r      $��2��6   $��7��5s      $��13(�5t      $1��+Продать и выгрузить чек��5u      $��Center=�5v      $��Center=�5w      $��Bahnschrift SemiLight)�5x      $
��Bold"�60   
�������$��5,0,5,0q�6U   6f   �� 5z   
   -   5         ��Name$ Client_Gave_textblock��5{      $��8��5|      $��2��6   $��7��5}      $.��(Сумма покупателя (руб)��5~       �� TextAlignment$ Center=�5      $��Center=�5�      $��Bahnschrift SemiLight)�5�      $
��Bold"�60   
��� ���$��15(�6V   �� 5�   
   -   5         ��Name$ Clients_Money_TextBox��5�      $��10��5�      $��2��6   $��7��5�      $��Center=�5�      $��Center=�5�      $��Bahnschrift SemiLight)�5�      $
��Bold"�60   
��� ���$��15(�6V   6d   �� 5�   
   -	   5        $ Products_textblock��5�      $��3��5�      $��2��6   $��1��5�      $��Продукты��5�      $ Center=�5�      $��Center=�5�      $��Bahnschrift SemiLight)�5�      $
��Bold"�60   
��� ���$��20(�6V   �� 5�   
   -
   5        $ Receipt_And_Sum_textblock��5�      $��8��5�      $��6��6   $��1��5�      $5��/Сумма для оплаты покупки: ��5�      $ Center=�5�      $��Center=�5�      $��Bahnschrift SemiLight)�5�      $
��Bold"�60   
��� ���$��20(�6V   5�      5�      