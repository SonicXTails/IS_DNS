   M S B A M L   `   `   `  ����Q  MInformation_System_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Information_System_AppInformation_System_App  5      2    ,Information_System_App.Cashier_Receipts_Page   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:Information_System_App     5        N� Title$  Cashier_Receipts_Page��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5       6   �� 5!      6   �� 5"      6   �� 5#      6   �� 5$      6   �� 5%      6   �� 5&      6   �� 5'      6   .+�5*   
   �� -   5         ��Name$ 
BackGround��5+      $��15��5,      $��1��5-      $��9��6   
�������6+   &   System.Windows.Controls.DataGrid  5/   
   -   5          Name$ Table_Receipts_Datagrid��50      $��1��51      $��9��6   $��1��52      $��8��53         CanUserAddRows .     CanUserDeleteRows .  
�������   CanUserReorderColumns .     AutoGenerateColumns .  $��Bahnschrift SemiLight)�59      $
��Bold"�60   	   Columns	 5:      0  *System.Windows.Controls.DataGridTextColumn  5;      ,  &System.Windows.Controls.DataGridColumn
   
IsReadOnly
 .    Header$ 
Номер��67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *Receipts_and_Products_ID+  5<      
 . $ Номер чека��67    6K   �� *
ID_Receipt+  5=      
 . $! Номер продукта��67    6O   �� *
ID_Product+  5>      
 . $ Количество��67    6K   �� *Quantity_from_Stock+  5?      
 . $ Название��67    6I   �� *Product_Name+  5A      
 . $ 
Номер��67    6F   �� *
Receipt_ID+  5B      
 . $( "Сумма покупки (руб)��67    6T   �� *Receipt_Full_Price+  5C      
 . $. (Сумма покупателя (руб)��67    6W   �� *Receipt_Buyer_Amount+  5D      
 . $ Сдача (руб)��67    6L   �� *Receipt_Change+  5E      
 . $ Дата покупки��67    6M   �� *Receipt_Date+5G      �� 5I   
   -   5         ��Name$ Find_by_button��5J      $��10��5K      $��5��6   $��5��5L      $��15(�5M      $+��%Найти по номеру чека��5N      $��Center=�5O      $��Center=�5P      $��Bahnschrift SemiLight)�5Q      $
��Bold"�60   
�������$��5,0,5,0q�6U   6f   �� 5S   
   -   5        $ Delete_by_button��5T      $��10��5U      $��5��6   $��3��5V      $��15(�5W      $��Удалить чек��5X      $��Center=�5Y      $��Center=�5Z      $��Bahnschrift SemiLight)�5[      $
��Bold"�60   
�������$��5,0,5,0q�6U   6f   �� 5]   
   -   5         ��Name$ Receipt_Number_TextBox��5^      $��10��5_      $��5��6   $��4��5`      $��Center=�5a      $��Center=�5b      $��Bahnschrift SemiLight)�5c      $
��Bold"�60   
��� ���$��15(�6V   $��5,0,5,0q�6d   6u   �� 5e   
   -   5        $ Export_Receipt_button��5f      $��15��5g      $��9��5h      $��15(�5i      $(��"Выгрузить этот чек��5j      $��Center=�5k      $��Center=�5l      $��Bahnschrift SemiLight)�5m      $
��Bold"�60   
�������$��0,0,5,0q�6U   6f   5o      5p      