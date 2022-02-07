using AppStoKursovaya.ActionManager;
using AppStoKursovaya.MyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace AppStoKursovaya.UserRoom
{
    /// <summary>
    /// Логика взаимодействия для MasterRoom.xaml
    /// </summary>
    public partial class MasterRoom : Window
    {
        public MasterRoom()
        {
            InitializeComponent();
            AddToMasterGrid();
        }

        private void AddToMasterGrid()
        {
            var employee = (from empl in RepEmployee.SelectAll() 
                         where empl.Id_user == Registration.RegUser.IdUser 
                         select empl).First();

            var master = (from m in RepMaster.SelectAll()
                           where m.Id_employee == employee.Id_employee select m).First();

            var info = RepOrder.SelectAll().Where(o => o.Id_master == master.Id_master).Select(i => new
            {
                ID = i.Id_order,
                Client = (RepClients.SelectAll().Where(c => c.Id_client == i.Id_client).Select(c => c.FIO)).First(),
                Phone = (RepClients.SelectAll().Where(c => c.Id_client == i.Id_client).Select(c => c.Phone)).First(),
                Data = i.Date_registr,
                Gosnomer = (RepAutos.SelectAll().Where(a => a.Id_auto == i.Id_auto).Select(a => a.Gosnomer)).First(),
                Status = i.Status_order,
                Comment = (RepClients.SelectAll().Where(a => a.Id_client == i.Id_client).Select(c => c.Feedback)).First()
            }).ToList();

            nameMaster.Text = employee.FIO;

            masterDataGrid.ItemsSource = info;
        }

        private void masterExit_Click(object sender, RoutedEventArgs e)
        {
            string mes = "Вы уверены, что хотите выйти в главное меню?";
            MessageBoxResult res = MessageBox.Show($"{mes}", "Master. Личный кабинет", MessageBoxButton.OKCancel);
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

        private void butGridOrder_Click(object sender, RoutedEventArgs e)
        {
            ManInfoOrder manInfo = new ManInfoOrder();
            manInfo.ShowDialog();
        }

        private void masterDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as DataGrid;

            object item = dg.SelectedItem;
            if (item == null)
                return;
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();
            ManInfoOrder.ManIdOrder = Convert.ToInt32(properties[0].GetValue(item));
        }
    }
}
