using AppStoKursovaya.ActionUser;
using AppStoKursovaya.Items;
using AppStoKursovaya.MyRepository;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace AppStoKursovaya.UserRoom
{
    /// <summary>
    /// Логика взаимодействия для UserPrivateRoom.xaml
    /// </summary>
    public partial class UserPrivateRoom : Window
    {
        public static int SelectIdOrder { get; set; }
        public UserPrivateRoom()
        {
            InitializeComponent();
            AddDataToGrid();
        }
        private void AddDataToGrid()
        {
            if (RepClients.SelectByIdUser(Registration.RegUser.IdUser) == null)
            {
                return;
            }
            var idClient = RepClients.SelectByIdUser(Registration.RegUser.IdUser).Id_client;
            var data = from d in RepOrder.SelectAll() where d.Id_client == idClient select d;

            var dataEnd = data.Select(d => new
            {
                Id = d.Id_order,
                Master = d.Id_master == 0 ? "не назначен" : "назначен",
                Work = d.Id_work,
                Date = d.Date_registr,
                Payment = d.Payment == 0 ? "не оплачен" : "оплачено"
            }).ToList();

            userDataGrid.ItemsSource = dataEnd;
        }
        
        private void saveUserRoom_Click(object sender, RoutedEventArgs e)
        {
            if (RepClients.SelectByIdUser(Registration.RegUser.IdUser) != null)
            {
                Clients client = new Clients();
                client.Id_client = RepClients.SelectByIdUser(Registration.RegUser.IdUser).Id_client;
                client.Feedback = textFeedback.Text;
                RepClients.Update(client);
                MessageBox.Show("Данные сохранены");
            }
            else
            {
                MessageBox.Show("Вы не сделали еще ни одного заказа.\nПожалуйста, сначала сделайте заказ,\nа потом оставьте отзыв о проделанной работе");
            }
        }

        private void exitUserRoom_Click(object sender, RoutedEventArgs e)
        {
            string mes = "Вы уверены, что хотите выйти в главное меню?";
            MessageBoxResult res = MessageBox.Show($"{mes}", "Client. Личный кабинет", MessageBoxButton.OKCancel);
            switch (res)
            {
                case MessageBoxResult.OK:

                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void userDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as DataGrid;

            object item = dg.SelectedItem;
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();

            SelectIdOrder = Convert.ToInt32(properties[0].GetValue(item));
        }

        private void userInfoOrder_Click(object sender, RoutedEventArgs e)
        {
            ClientInfoOrder infoOrder = new ClientInfoOrder();
            infoOrder.Show();
        }

        private void butUpdateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            UserPrivateRoom u = new UserPrivateRoom();
            u.Show();
            this.Close();
        }
    }
}
