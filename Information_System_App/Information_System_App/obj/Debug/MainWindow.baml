   M S B A M L   `   `   `  ����Q  MInformation_System_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Information_System_AppInformation_System_App  5   	   '    !Information_System_App.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Information_System_App     5   	     � Title$0  *ИС (DNS) Окно авторизации��5   	   $	��400��6+   $	��400��68   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5       6   �� 5!      6   �� 5"      6   �� 5#      6   �� 5$      6   �� 5%      6   �� 5&      6   .+�5*   
   �� $��4��5+      $��7��6   $��26,28,26,10q�61   $��2��6F   ��5,      �� 5-       �� ImageSource$ /DNS_Logo.jpg��6   5/      �� 50   
   $��1q�51      $��2��52      $��7��6   $��2��53      $��11��6    �� CornerRadius$ 	20,20,0,0��54      ��55      �� 56       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��57      �  � Color$ 	#FFEA8819��6#   � 58      $ White��6#    � Offset$ 0.993Q�61   59      5;      �� 5<   
   $��2��5=      $��2��6   $��4��5>      $��7��6   $��Center=�5?      $��Авторизация��5@      $
��Wrap=�6    	 �� TextAlignment$	 Center=�64   $��Center=�6K   $��25(�5A      $��Bahnschrift SemiLight)�6   $
��Bold"�6>   
�������6P   �� 5B   
   $��4��5C      $��1��5D      $��7��6   $��Center=�5E      $��Логин:��5F      $
��Wrap=�6   $	 Center=�6/   $��Center=�6F   $��15(�5G      $��Bahnschrift SemiLight)�6   $
��Bold"�6>   
�������6P   �� 5H   
   -   5        
 ��Name$$
 Auth_Username_or_Email_TextBox��5I      $��4��5J      $��6��5K      $��6��6   $��Center=�5L       �� TextWrapping$
 Wrap=�5M      $��15(�5N      $��Bahnschrift SemiLight)�6   $
��Bold"�6>   
����������	#7FFFFFFF6�   �� 5O   
   $��5��5P      $��1��5Q      $��7��6   $��Center=�5R      $��Пароль:��5S      $
��Wrap=�6   $	 Center=�60   $��Center=�6G   $��15(�5T      $��Bahnschrift SemiLight)�6   $
��Bold"�6>   
�������6P   G� 5U   
   -   5         G�Name$ Auth_Password_PasswordBox��5V      $��5��5W      $��6��5X      $��6��6   $��Center=�5Y      $��15(�5Z      $��Bahnschrift SemiLight)�6   $
��Bold"�6>   
����������	#7FFFFFFF6�   �� 5[   
   -   5         ��Name$ Auth_Authorization_button��5\      $��7��5]      $	��Top=�6   $��3��5^      $��9��6   $��
Войти��5_      $��20(�5`      $��Bahnschrift SemiLight)�6   $
��Bold"�6>   
����������	#FFF1B0676z   �� 5a   
   -   5         ��Name$ 
Wrong_Auth��5b      $��3��5c      $��3��5d      $��9��6   $	 Center=�5e      $��Bottom=�5f      $��15(�5g      $7��1Вы ввели некоректно данные��5h      $��Bahnschrift SemiLight)�5i      $
��Bold"�60   
�������6B   5j      5k      