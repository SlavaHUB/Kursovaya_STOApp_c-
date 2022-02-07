using AppStoKursovaya.Items;
using AppStoKursovaya.MyRepository;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppStoKursovaya.ActionWindows
{
    public partial class OrderMenu : Window
    {
        public OrderMenu()
        {
            InitializeComponent();
            AddToComBoxProblem();
            AddToComBoxAuto();
            AddToComBoxYear();
            AddToComBoxTypeEngine();
        }

        private void AddToComBoxProblem()
        {
            var works = from w in RepWorks.SelectAll() select w.Description_work;
            int c = 0;
            foreach (var w in works)
            {
                if (c % 2 == 0)
                    orderProblem.Items.Add(new TextBlock { FontSize = 14, Text = w });
                else
                     orderProblem.Items.Add(new TextBlock { FontSize = 14, Text = w, Foreground = Brushes.Orange });

                c++;
            }
        }

        private void AddToComBoxAuto()
        {
            var marks = from m in RepMarks.SelectAll() select m.Name_mark;
            int c = 0;
            foreach (var m in marks)
            {
                if (c % 2 == 0)
                    orderMarka.Items.Add(new TextBlock { FontSize = 14, Text = m });
                else
                    orderMarka.Items.Add(new TextBlock { FontSize = 14, Text = m, Foreground = Brushes.Orange });

                c++;
            }
        }

        private void AddToComBoxYear()
        {
            int curYear = int.Parse(DateTime.Now.Year.ToString());

            while (curYear > 1950)
            {
                if (curYear % 2 == 0)
                    orderYear.Items.Add(new TextBlock { FontSize = 14, Text = curYear.ToString() });
                else
                    orderYear.Items.Add(new TextBlock { FontSize = 14, Text = curYear.ToString(), Foreground = Brushes.Orange });
                curYear--;
            }
        }

        private void AddToComBoxTypeEngine()
        {
            orderTypeEngine.Items.Add(new TextBlock { FontSize = 14, Text = "Бензин" });
            orderTypeEngine.Items.Add(new TextBlock { FontSize = 14, Text = "Дизель", Foreground = Brushes.Orange });
            orderTypeEngine.Items.Add(new TextBlock { FontSize = 14, Text = "Гибрид" });
        }

        private bool CheckInputTextBox()
        {
            Regex regPhone = new Regex(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$");
            Regex regGosnomer = new Regex("[0-9]{4}[A-Z]{2}");
            bool flag = true;

            if (orderUserSurname.Text.Length > 50 || orderUserSurname.Text.Length < 1)
            {
                orderUserSurname.Background = Brushes.Red;
                flag = false;
            }
            else
                orderUserSurname.Background = Brushes.White;

            if (!regPhone.IsMatch(orderTelNom.Text))
            {
                orderTelNom.Background = Brushes.Red;
                flag = false;
            }
            else
                orderTelNom.Background = Brushes.White;

            if (!regGosnomer.IsMatch(orderNomer.Text))
            {
                orderNomer.Background = Brushes.Red;
                flag = false;
            }
            else
                orderNomer.Background = Brushes.White;

            if (flag == false)
            {
                MessageBox.Show("Неправильно введены данные");
                return flag;
            }
            else
                return flag;

        }
        private bool CheckSelectComboBox()
        {
            bool flag = true;
            if (orderProblem.SelectedItem == null)
            {
                orderProblem.Background = Brushes.Red;
                flag = false;
            }
            if (orderMarka.SelectedItem == null)
            {
                orderMarka.Background = Brushes.Red; 
                flag = false;
            }
            if (orderYear.SelectedItem == null)
            {
                orderYear.Background = Brushes.Red;
                flag = false;
            }
            if (orderTypeEngine.SelectedItem == null)
            {
                orderTypeEngine.Background = Brushes.Red;
                flag = false;
            }
            if (flag == false)
            {
                MessageBox.Show("Выбраны не все элементы");
                return flag;
            }
            else
                return flag;
        }
        private void OrderCost()
        {
            orderCost.Text = (from w in RepWorks.SelectAll() 
                              where w.Id_work == orderProblem.SelectedIndex + 1
                              select w.Cost_work).First().ToString();
        }
        private void orderReg_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputTextBox() && CheckSelectComboBox())
            {
                //проверяем если наш автомобиль в таблице, если нет, то добавляем
                if (RepAutos.SelectByGosnomer(orderNomer.Text) == null)
                {
                    Autos auto = new Autos();
                    auto.Year_release = Convert.ToInt32(orderYear.Text);
                    auto.Id_mark = Convert.ToInt32((RepMarks.SelectAll().Where(m=> m.Name_mark == orderMarka.Text).Select(n=>n.Id_mark)).First());
                    auto.Type_engine = orderTypeEngine.Text;
                    auto.Gosnomer = orderNomer.Text;
                    RepAutos.Insert(auto);
                }

                Clients c = new Clients();
                c.Phone = orderTelNom.Text;
                c.FIO = orderUserSurname.Text;
                c.Id_user = Registration.RegUser.IdUser;
                RepClients.Insert(c);

                //добавляем в таблицу заказы
                Orders order = new Orders();
                order.Id_work = Convert.ToInt32(RepWorks.SelectAll().Where(w=>w.Description_work == orderProblem.Text).Select(w=>w.Id_work).First());
                order.Id_client = RepClients.SelectByIdUser(Registration.RegUser.IdUser).Id_client;
                order.Id_auto = RepAutos.SelectByGosnomer(orderNomer.Text).Id_auto;
                order.Date_registr = DateTime.Now.ToString();

                RepOrder.Insert(order);

                string mes = "Регистрация заказа прошла успешно.\nСостояние и подробности заказа Вы можете узнать в личном кабинете.";
                MessageBox.Show(mes);
                this.Close();
            }
        }

        private void orderProblem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (orderProblem.SelectedItem != null)
                OrderCost();
        }
    }
}
