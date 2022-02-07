using AppStoKursovaya.ActionWindows;
using AppStoKursovaya.MyRepository;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace AppStoKursovaya
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string infoText = "1. Гарантия\nОфициальная гарантия на выполненные работы и запчасти! " +
            "Читали отзывы об автосервисах Гомеля о дополнительной трате на обслуживание авто даже в гарантийный период? " +
            "Обратившись к нам, вы избежите таких ситуаций.Наша гарантия официальная, " +
            "документально подтвержденная\n" +
            "2. Крупнейшая сеть\nНаше СТО является единственным представителем в Гомеле первой и " +
            "самой крупной сети станций технического обслуживания автомобилей в Беларуси\n" +
            "3. «Прозрачная» работа\nВы будете знать с точностью, за что платите. " +
            "Мы обязательно согласовываем все действия по работам и запчастям со своими клиентами";
        public MainWindow()
        {
            InitializeComponent();
            infoTextBlock.Text = infoText;
            AddToComBoxOurUsl();
        }

        private void butInput_Click(object sender, RoutedEventArgs e)
        {
            Registration.RegUser regUser = new Registration.RegUser();
            regUser.Show();
            this.Close();
        }

        private void butPrivRoom_Click(object sender, RoutedEventArgs e)
        {
            if (!Registration.RegUser.Autorisation)
                MessageBox.Show("Вы не авторизированны. Пожалуйста, авторизируйтесь");
            else
            {
                UserRoom.UserPrivateRoom userRoom = new UserRoom.UserPrivateRoom();
                userRoom.Show();
                this.Close();
            }
        }

        private void butOrder_Click(object sender, RoutedEventArgs e)
        {
            if (!Registration.RegUser.Autorisation)
                MessageBox.Show("Вы не авторизированны. Пожалуйста, авторизируйтесь");
            else
            {
                OrderMenu orderMenu = new OrderMenu();
                orderMenu.ShowDialog();
            }
        }

        private void butInfo_Click(object sender, RoutedEventArgs e)
        {
            handInfo.Content = "О нас";
            infoTextBlock.Text = "Наш автосервис открылся в 2014 году. " +
                "Современное оборудование, закупленное и установленное в ремонтных цехах, " +
                "отвечает всем нормам безопасности и производительности современной станции " +
                "технического обслуживания автомобилей. " +
                "Целью создания автокомплекса было повышение качества оказания услуг в сфере " +
                "обслуживания автомобилей в Гомеле и выведение на новый уровень неофициального сервиса. " +
                "Задача стояла амбициозная и ее удалось решить благодаря сокращению сроков ремонта, " +
                "профессиональному подбору запчастей и организации оперативной доставки запчастей к ремонту; " +
                "повышению уровня обслуживания клиентов и создания комфортной зоны ожидания; " +
                "благодаря созданию довольно большого склада запчастей на территории комплекса." +
                " Мы гордимся теми результатами, которых нам удалось достичь, идем дальше, " +
                "открывая новые горизонты в этой непростой отрасли.";
        }

        private void butComm_Click(object sender, RoutedEventArgs e)
        {
            infoTextBlock.Text = "";
            handInfo.Content = "Отзывы";

            var clients = from cl in RepClients.SelectAll() where cl.Feedback != "" select cl;
            StringBuilder sb = new StringBuilder();
            foreach (var c in clients)
                sb.Append(c.FIO + "\n" + c.Feedback + "\n\n");

            infoTextBlock.Text = sb.ToString();
        }

        private void butMain_Click(object sender, RoutedEventArgs e)
        {
            handInfo.Content = "3 причины выбрать наш автосервис";
            infoTextBlock.Text = infoText;
        }

        private void AddToComBoxOurUsl()
        {
            var works = from w in RepWorks.SelectAll() select w.Description_work;
            foreach(var w in works)
                ourUsl.Items.Add(new TextBlock { FontSize = 14, Text = w });

            ourUsl.Text = works.First();
        }

        private void ourUsl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { }

        private void infoUsl_Click(object sender, RoutedEventArgs e)
        {
            var work = (from c in RepWorks.SelectAll()
                        where c.Description_work == ourUsl.Text
                        select c).First();

            infoTextBlock.Text = work.Description_work +
                "\nСтоимость услуги от " + work.Cost_work + " руб";
        }
    }
}
