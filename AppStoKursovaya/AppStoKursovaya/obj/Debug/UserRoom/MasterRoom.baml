   M S B A M L   `   `   `  ����J  FAppStoKursovaya, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AppStoKursovaya.UserRoomAppStoKursovaya.UserRoom  5   	   )    #AppStoKursovaya.UserRoom.MasterRoom   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:AppStoKursovaya.UserRoom     5   	     � Title$  
MasterRoom��5   	   $	��340��6   $	��430��6)   .��5	      � 
��� ���.+�5
   
   ��  �� Orientation$ Vertical=�6   $��	10 0 10 0q�6,   .+�5      �� $#��Кабинет мастера��6   $��20(�6.   �� 5      $ 
Horizontal=�6   .+�5      �� $)��#фамилия сотрудника��6   $��10(�65   �� 5      -   5         ��Name$ 
nameMaster��5      $
��auto��60   $��10(�6=   
��� � �$��Center=�6^   $��10 0 0 0q�6y   5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ masterDataGrid��5      $	��200��6/      
IsReadOnly .    RowDetailsTemplate 5      �� 5      .R�5      �� $ 
Horizontal=�6%   .+�5      ��  ��Name$ butGridOrder��6%   -   5        $ ��Дополнительно��5   ;   6S   5      5      5      �� 5      -   5        $ 
masterExit��5      $��
Выход��6)   $��0 10 0 0q�69   $	��100��6K   $
��Left=�6W   
���  ��6�   5      5      5      