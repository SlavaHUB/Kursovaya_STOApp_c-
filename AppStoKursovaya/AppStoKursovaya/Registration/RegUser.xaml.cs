using AppStoKursovaya.ActionWindows;
using AppStoKursovaya.Items;
using AppStoKursovaya.MyRepository;
using System.Windows;
using System.Windows.Media;

namespace AppStoKursovaya.Registration
{
    public partial class RegUser : Window
    {
        public RegUser()
        {
            InitializeComponent();
        }

        //свойсто, которое показывает, зарегистрирован ли пользователь//
        static public bool Autorisation { get; set; }
        //свойство, которое возвращает логин текущего пользователя
        static public int IdUser { get; set; }

        //регистрация
        private void ButRegistr_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputUser())
            {
                if (RepUsers.SelectByLogPass(userLogin.Text, userPass.Password) == null)
                {
                    userLogin.Background = Brushes.Green;
                    userPass.Background = Brushes.Green;
                    Users user = new Users();
                    user.UsLogin = userLogin.Text;
                    user.UsPassword = userPass.Password;
                    user.Rank = "user";
                    RepUsers.Insert(user);
                    IdUser = RepUsers.SelectByLogPass(userLogin.Text, userPass.Password).Id_user;
                    Autorisation = true;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();

                }
                else
                {
                    userLogin.Background = Brushes.Red;
                    userPass.Background = Brushes.Red;
                    MessageBox.Show("Пользователь с таким логином уже есть.\nПопробуйте другой.");
                }
            }
        }
        //вход
        private void butInputUser_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputUser())
            {
                Users user = RepUsers.SelectByLogPass(userLogin.Text, userPass.Password);

                if (user == null)
                    MessageBox.Show("Пользователя с таким логином нет");
                else
                {
                    if (user.Rank == "user")
                    {
                        Autorisation = true;
                        IdUser = RepUsers.SelectByLogPass(userLogin.Text, userPass.Password).Id_user;
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else if (user.Rank == "manager")
                    {
                        InputManager();
                    }
                    else if(user.Rank == "master")
                    {
                        InputMaster();
                    }
                }
            }
        }
        private void InputManager()
        {
            string mes = "Вы вошли как manager. Если вы не хотели входить\nпод таким " +
              "уровнем доступа, пожалуйста, нажмите Отмена.";
            MessageBoxResult res = MessageBox.Show($"{mes}", "Автосервис. Вход", MessageBoxButton.OKCancel);
            switch (res)
            {
                case MessageBoxResult.OK:
                    IdUser = RepUsers.SelectByLogPass(userLogin.Text, userPass.Password).Id_user;
                    UserRoom.ManagerRoom mr = new UserRoom.ManagerRoom();
                    mr.Show();
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Пожалуйста, повторите ввод");
                    break;
            }
        }

        private void InputMaster()
        {
            string mes = "Вы вошли как master. Если вы не хотели входить\nпод таким " +
              "уровнем доступа, пожалуйста, нажмите Отмена.";
            MessageBoxResult res = MessageBox.Show($"{mes}", "Автосервис. Вход", MessageBoxButton.OKCancel);
            switch (res)
            {
                case MessageBoxResult.OK:
                    IdUser = RepUsers.SelectByLogPass(userLogin.Text, userPass.Password).Id_user;
                    UserRoom.MasterRoom master = new UserRoom.MasterRoom();
                    master.Show();
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Пожалуйста, повторите ввод");
                    break;
            }
        }

        //проверяем правильность введенных данных
        private bool CheckInputUser()
        {
            bool flag = true;
            if (userLogin.Text.Length < 3 || userLogin.Text.Length > 15)
            {
                userLogin.Background = Brushes.Red;
                flag = false;
            }
            if (userPass.Password.Length < 3 || userPass.Password.Length > 25)
            {
                userPass.Background = Brushes.Red;
                flag = false;
            }
            if (flag == false)
            {
                MessageBox.Show("Введенные данные не соотвествуют нужной длине.\nПовторите ввод");
                return false;
            }
            else
                return true;
            //проверка закончилась
        }
    }
}
