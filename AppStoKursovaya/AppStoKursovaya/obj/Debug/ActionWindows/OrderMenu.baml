   M S B A M L   `   `   `  ����J  FAppStoKursovaya, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:AppStoKursovaya.ActionWindowsAppStoKursovaya.ActionWindows  5   	   -    'AppStoKursovaya.ActionWindows.OrderMenu   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   =local+clr-namespace:AppStoKursovaya.ActionWindows     5   	   -   5          �Name$  orderWindow��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � Title$ PersonalRoom��5	   	   $	��515��6   $	��480��6+   $	��480��68   $	��480��6G   $	��515��6V   $	��515��6f   .��5
      � ���#FFCF40.+�5   
   �� $	��Top=�6   $	��600��6-   .+�5      �� $'��!Оформления заказа��6   $��25(�61   $
��Bold"�6?   $	��Top=�6Q   �� 5       �� Orientation$ 
Horizontal=�6   $��0 10 0 0q�62   .+�5      �� $��ФИО*��6   $��14(�6'   $	��160��65   �� 5      -   5         ��Name$ orderUserSurname��5      $��14(�64   
����� �$	��300��6T   5      �� 5      $ 
Horizontal=�6   $��0 10 0 0q�62   .+�5      �� $"��Номер телефона*��6   $��14(�62   $	��160��6@   �� 5      -   5        $ orderTelNom��5      $��14(�6/   
����� �$	��300��6O   5      �� 5      $ 
Horizontal=�6   $��0 10 0 0q�62   .+�5      �� $!��Неисправность*��6   $��14(�61   $	��160��6?   �� 5      -   5         ��Name$ orderProblem��5      $	��300��61   6=   5      �� 5      $ 
Horizontal=�6   $��0 10 0 0q�63   .+�5      �� $&�� Марка автомобиля*��6   $��14(�64   $	��160��6B   �� 5      -   5        $ 
orderMarka��5      $	��300��6/   $��Center=�6;   6Z   5      �� 5      $ 
Horizontal=�6   $��0 10 0 0q�62   .+�5      �� $��Год выпуска*��6   $��14(�6/   $	��160��6=   �� 5      -   5        $ 	orderYear��5      $	��300��6.   5       �� 5!      $ 
Horizontal=�6   $��0 10 0 0q�62   .+�5"      �� $ ��Тип двигателя*��6   $��14(�61   $	��160��6@   �� 5#      -   5        $ orderTypeEngine��5#      $	��300��64   5$      �� 5%      $ 
Horizontal=�6   $��0 10 0 0q�62   .+�5&      �� $��Госномер*��6   $��14(�6,   $	��160��6;   �� 5'      -   5        $ 
orderNomer��5'      $��14(�6.   
����� �$	��300��6N   5(      �� 5)      $ 
Horizontal=�6   $��0 10 0 0q�62   .+�5*      �� $B��<*-поля, обязательные к заполнению��6   5+      �� 5,      $ 
Horizontal=�6   .+�5-      �� $%��Стоимость заказа��6   $��14(�63   �� 5.      -	   5         ��Name$ 	orderCost��5.      $��14(�6/   $��0��6=   $��Center=�6F   5/      �� 50      $ 
Horizontal=�6   $��5 15 0 0q�62   $��26��6D   .+�51      �� -
   5         ��Name$ orderReg��51      $'��!Подтвердить заказ��6+   $	��455��6G   
��� � �6g   52      53      54      55      