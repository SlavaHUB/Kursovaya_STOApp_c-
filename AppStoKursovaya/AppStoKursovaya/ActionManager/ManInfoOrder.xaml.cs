using AppStoKursovaya.Items;
using AppStoKursovaya.MyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppStoKursovaya.ActionManager
{
    public partial class ManInfoOrder : Window
    {
        static public int ManIdOrder { get; set; }
        public ManInfoOrder()
        {
            InitializeComponent();
            AddInfoMaster();
            AddInfo();
            CheckStatusOrder();
            infoIdOrder.Text = ManIdOrder.ToString();
        }
        private Orders GetOrder()
        {
            return (from ord in RepOrder.SelectAll() where ord.Id_order == ManIdOrder select ord).First();
        }

        private void NameMasterTextBox()
        {
            nameMasterText.Visibility = Visibility.Visible;
            int idEmpl = (from master in RepMaster.SelectAll()
                          where master.Id_master == GetOrder().Id_master
                          select master.Id_employee).First();
            var nameMaster = (RepEmployee.SelectAll().Where(n => n.Id_employee == idEmpl).Select(n => n.FIO)).First();
            nameMasterText.Text = nameMaster;
        }
        private void CheckStatusOrder()
        {
            if (GetOrder().Status_order.ToLower() == "в ожидании")
            {
                butAccept.Visibility = Visibility.Visible;
                butRefuse.Visibility = Visibility.Visible;
                nameMaster.Visibility = Visibility.Visible;
            }
            else if(GetOrder().Status_order.ToLower() == "выполняется")
            {
                butClose.Visibility = Visibility.Visible;
                nameMasterText.Visibility = Visibility.Visible;
                NameMasterTextBox();
            }
            else if (GetOrder().Status_order.ToLower() == "закрыто")
            {
                NameMasterTextBox();
            }
        }
        private void AddInfo()
        {
            var order = GetOrder();
            
            var client = (from cl in RepClients.SelectAll() where cl.Id_client == order.Id_client select cl.FIO).First();
            infoClient.Text = client;

            var auto = (from a in RepAutos.SelectAll() where a.Id_auto == order.Id_auto select a).First();
            infoAutoMarka.Text = (from m in RepMarks.SelectAll() where m.Id_mark == auto.Id_mark select m.Name_mark).First();

            infoAutoGosnom.Text = auto.Gosnomer;
            infoAutoTypeEngine.Text = auto.Type_engine;

            infoWork.Text = (from w in RepWorks.SelectAll() where w.Id_work == order.Id_work select w.Description_work).First();
        }

        private void AddInfoMaster()
        {
            List<string> namesMaster = new List<string>();
            var masters = from master in RepMaster.SelectAll() select master;
            foreach (var m in masters)
            {
                string fio = (from master in RepEmployee.SelectAll() where master.Id_employee == m.Id_employee select master.FIO).First();
                namesMaster.Add(fio);
            }
            foreach(var m in namesMaster)
            {
                nameMaster.Items.Add(new TextBlock { Text = m, FontSize = 14 });
            }
        }

        private void butAccept_Click(object sender, RoutedEventArgs e)
        {
            if (nameMaster.SelectedItem == null)
                MessageBox.Show("Ошибка: не все поля заполнены");
            else
            {
                Orders order = new Orders();
                order.Status_order = "Выполняется";
                order.Id_order = ManIdOrder;
                order.Id_master = nameMaster.SelectedIndex + 1;
                int idEmpl = (from empl in RepEmployee.SelectAll()
                              where empl.Id_user == Registration.RegUser.IdUser
                              select empl.Id_employee).First();

                int idChecker = (from checker in RepChecker.SelectAll() 
                                 where checker.Id_employee == idEmpl 
                                 select checker.Id_checker).First();

                order.Id_checker = idChecker;
                RepOrder.Update(order);
                MessageBox.Show("Данные сохранены");
            }
        }

        private void butRefuse_Click(object sender, RoutedEventArgs e)
        {
            RepOrder.UpdateStatus(new Orders { Status_order = "отказанно", Id_order = ManIdOrder });
            this.Close();
            MessageBox.Show("Успех: статус заказа изменен на ОТКАЗАННО");
        }

        private void butClose_Click(object sender, RoutedEventArgs e)
        {
            RepOrder.UpdateStatus(new Orders { Status_order = "закрыто", Id_order = ManIdOrder });
            MessageBox.Show("Успех: статус заказа изменен на ЗАКРЫТО");
        }
    }
}
