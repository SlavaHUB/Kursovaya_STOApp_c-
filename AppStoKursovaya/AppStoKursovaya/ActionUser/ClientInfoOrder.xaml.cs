using AppStoKursovaya.Items;
using AppStoKursovaya.MyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppStoKursovaya.ActionUser
{
    public partial class ClientInfoOrder : Window
    {
        private Orders order;
        public ClientInfoOrder()
        {
            InitializeComponent();

            order = new Orders();
            order = (from o in RepOrder.SelectAll()
                     where o.Id_order == UserRoom.UserPrivateRoom.SelectIdOrder
                     select o).First();

            AddInfoMaster();
            AddInfoAuto();
            AddInfoDateRegistr();
            AddInfoOrder();
            AddInfoIdOrder();
        }
        private void AddInfoIdOrder()
        {
            clientInfoIdOrder.Text = order.Id_order.ToString();
        }

        private void AddInfoMaster()
        {
            var empls = from master in RepMaster.SelectAll()
                         where master.Id_master == order.Id_master
                         select master;

            if (empls.Count() != 0)
            {
                int idEmpl = empls.First().Id_employee;
                var nameMaster = (RepEmployee.SelectAll().Where(n => n.Id_employee == idEmpl).Select(n => n.FIO)).First();
                clientNameMaster.Text = nameMaster;
            }
        }

        private void AddInfoAuto()
        {
            var auto = (from a in RepAutos.SelectAll() where a.Id_auto == order.Id_auto select a).First();

            clientInfoAutoMarka.Text = (from m in RepMarks.SelectAll() where m.Id_mark == auto.Id_mark select m.Name_mark).First();
            clientInfoAutoGosnom.Text = auto.Gosnomer;
            clientInfoAutoTypeEngine.Text = auto.Type_engine;
        }
        private void AddInfoDateRegistr()
        {
            clientDataOrder.Text = order.Date_registr;
        }
        private void AddInfoOrder()
        {
            var works = (from w in RepWorks.SelectAll() where w.Id_work == order.Id_work select w).First();
            clientInfoWork.Text += works.Description_work + "\n" + "Стоимость: " + works.Cost_work;
        }

        private void butPayment_Click(object sender, RoutedEventArgs e)
        {
            if (order.Payment == 0)
            {
                UserInfoOrder userInfo = new UserInfoOrder();
                userInfo.curIdOrder = order.Id_order;
                userInfo.ShowDialog();
                this.Close();
            }
            else
                MessageBox.Show("Ваш заказ уже оплачен.");
        }

        private void butClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
