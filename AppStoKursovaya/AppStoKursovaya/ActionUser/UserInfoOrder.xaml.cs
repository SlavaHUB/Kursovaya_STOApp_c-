using AppStoKursovaya.Items;
using AppStoKursovaya.MyRepository;
using System;
using System.Windows;
using System.Windows.Input;

namespace AppStoKursovaya.ActionUser
{
    public partial class UserInfoOrder : Window
    {
        public int curIdOrder;
        public UserInfoOrder()
        {
            InitializeComponent();
        }

        private void butClientPay_Click(object sender, RoutedEventArgs e)
        {
            Orders order = new Orders();
            order.Payment = 1;
            order.Id_order = curIdOrder;
            MyRepository.RepOrder.UpdatePayment(order);
            MessageBox.Show("Заказ оплачен!");
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientIdOrder.Text = curIdOrder.ToString();
            clientIdOrder.Text = curIdOrder.ToString();
        }

        private void clientCard_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                MessageBox.Show("Неправильно введены данные");
            }
        }
    }
}
