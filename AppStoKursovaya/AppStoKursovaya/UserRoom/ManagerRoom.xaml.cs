using AppStoKursovaya.ActionManager;
using AppStoKursovaya.Items;
using AppStoKursovaya.MyRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace AppStoKursovaya.UserRoom
{
    /// <summary>
    /// Логика взаимодействия для ManagerRoom.xaml
    /// </summary>
    public partial class ManagerRoom : Window
    {
        private string OrderOrWork;
        public ManagerRoom()
        {
            InitializeComponent();
            AddInfoStat();
            AddFioManager();
        }

        private void butManClients_Click(object sender, RoutedEventArgs e)
        {
            manGrid.Visibility = Visibility.Visible;
            manGridOrder.Visibility = Visibility.Collapsed;
            infoStack.Visibility = Visibility.Collapsed;
            stackInfoMonth.Visibility = Visibility.Collapsed;

            var clients = RepClients.SelectAll().Select(c => new
            {
                Id = c.Id_client,
                Phone = c.Phone,
                FIO = c.FIO,
                Comment = c.Feedback
            }).ToList();


            manGrid.ItemsSource = clients;
        }

        private void AddFioManager()
        {

            fioManager.Text = (from empl in RepEmployee.SelectAll()
                            where empl.Id_user == Registration.RegUser.IdUser
                            select empl).First().FIO;
        }

        private void butManOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderOrWork = "order";
            manGridOrder.Visibility = Visibility.Visible;
            manGrid.Visibility = Visibility.Collapsed;
            infoStack.Visibility = Visibility.Collapsed;
            stackInfoMonth.Visibility = Visibility.Collapsed;

            var payments = from p in RepOrder.SelectAll() select p.Payment;

            var orders = RepOrder.SelectAll().Select(order => new
            {
                Id = order.Id_order,
                Work = order.Id_work,
                Condition = order.Status_order,
                Date = order.Date_registr,
            }).ToList();

            manGridOrder.ItemsSource = orders;
        }

        private void butManEmpl_Click(object sender, RoutedEventArgs e)
        {
            manGrid.Visibility = Visibility.Visible;
            manGridOrder.Visibility = Visibility.Collapsed;
            infoStack.Visibility = Visibility.Collapsed;
            stackInfoMonth.Visibility = Visibility.Collapsed;

            var employee = from emp in RepEmployee.SelectAll() select emp;

            manGrid.ItemsSource = employee;
        }

        private void butManWorks_Click(object sender, RoutedEventArgs e)
        {
            OrderOrWork = "work";
            manGrid.Visibility = Visibility.Collapsed;
            manGridOrder.Visibility = Visibility.Visible;
            infoStack.Visibility = Visibility.Collapsed;
            stackInfoMonth.Visibility = Visibility.Collapsed;

            var works = from w in RepWorks.SelectAll() select w;

            manGridOrder.ItemsSource = works;
        }

        private void butManStat_Click(object sender, RoutedEventArgs e)
        {
            AddInfoStat();
        }

        private void AddInfoStat()
        {
            infoStack.Visibility = Visibility.Visible;
            manGrid.Visibility = Visibility.Collapsed;
            manGridOrder.Visibility = Visibility.Collapsed;
            stackInfoMonth.Visibility = Visibility.Collapsed;

            infoOrder.Text = (from order in RepOrder.SelectAll() select order.Id_order).Count().ToString();

            infoClient.Text = (from c in RepClients.SelectAll() select c.Id_client).Count().ToString();

            infoEmpl.Text = (from epml in RepEmployee.SelectAll() select epml.Id_employee).Count().ToString();

            var works = from w in RepWorks.SelectAll() select w;
            infoWorks.Text = (from w in works select w.Id_work).Count().ToString();

            var workIds = from o in RepOrder.SelectAll() select o.Id_order;
            List<double> lst = new List<double>();
            foreach (var w in works)
            {
                var t = from c in workIds where c == w.Id_work select w.Cost_work;
                if (t.Count() != 0)
                    lst.Add(t.Average());
            }
            infoCostWorks.Text = Math.Round(lst.Average(), 2).ToString();
        }

        private void butGridOrder_Click(object sender, RoutedEventArgs e)
        {
            if (OrderOrWork == "order")
            {
                ManInfoOrder manInfo = new ManInfoOrder();
                manInfo.ShowDialog();
            }
            else if(OrderOrWork == "work")
            {
                ManInfoWork infoWork = new ManInfoWork();
                infoWork.ShowDialog();
            }
        }

        private void manGridOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = sender as DataGrid;

            object item = dg.SelectedItem;
            if (item == null)
                return;
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();
            if (OrderOrWork == "order")
            {
                ManInfoOrder.ManIdOrder = Convert.ToInt32(properties[0].GetValue(item));
            }
            else if (OrderOrWork == "work")
            {
                ManInfoWork.ManIdWork = Convert.ToInt32(properties[0].GetValue(item));
            }
        }

        private void manExit_Click(object sender, RoutedEventArgs e)
        {
            string mes = "Вы уверены, что хотите выйти в главное меню?";
            MessageBoxResult res = MessageBox.Show($"{mes}", "Manager. Личный кабинет", MessageBoxButton.OKCancel);
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

        private void addInfoWork_Click(object sender, RoutedEventArgs e)
        {
            ManInfoWork manInfo = new ManInfoWork();
            manInfo.Show();
        }

        private void butManInfoMonth_Click(object sender, RoutedEventArgs e)
        {
            stackInfoMonth.Visibility = Visibility.Visible;

            manGrid.Visibility = Visibility.Collapsed;
            manGridOrder.Visibility = Visibility.Collapsed;
            infoStack.Visibility = Visibility.Collapsed;

            string curMonth = DateTime.Now.Month.ToString();

            if (curMonth.Length == 1)
                curMonth = "0" + curMonth;

            curMonthInfo.Text = curMonth;

            var orders = from order in RepOrder.SelectAll() select order;
            int cnt = 0;
            List<int> costs = new List<int>();
            foreach (var order in orders)
            {
                string d = order.Date_registr.Split(' ')[0].Split('.')[1];
                if (d == curMonth)
                {
                    costs.Add(RepWorks.SelectAll().Where(c => c.Id_work == order.Id_work).Select(c => c.Cost_work).First());
                    cnt++;
                }
            }
            infoColOrdersMonth.Text = cnt.ToString();
            infoCostOrdersMonth.Text = Math.Round(costs.Average(), 2).ToString();
        }

        private void butManAddWork_Click(object sender, RoutedEventArgs e)
        {
            ManInfoWork infoWork = new ManInfoWork();
            infoWork.ShowDialog();
        }

        private void butGridDel_Click(object sender, RoutedEventArgs e)
        {
            if (OrderOrWork == "order")
                MessageBox.Show("Вы не можете удалить заказ");
            else
            {
                string mes = "Вы действительно хотите удалить эту услугу?";
                MessageBoxResult res = MessageBox.Show($"{mes}", "Удаление услуги", MessageBoxButton.OKCancel);
                switch (res)
                {
                    case MessageBoxResult.OK:
                        RepWorks.Delete(ManInfoWork.ManIdWork);
                        break;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
        }
    }
}
