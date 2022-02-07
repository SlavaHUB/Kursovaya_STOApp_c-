using AppStoKursovaya.Items;
using AppStoKursovaya.MyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AppStoKursovaya.ActionManager
{
    public partial class ManInfoWork : Window
    {
        public static int ManIdWork;
        private bool CheckAdd;
        public ManInfoWork()
        {
            InitializeComponent();
            AddInfo();
        }

        private void butAddInfoWork_Click(object sender, RoutedEventArgs e)
        {
            if (newDes.Text == "" && newCost.Text == "")
                MessageBox.Show("Все поля должны быть заполнены");
            else
            {
                if (int.TryParse(newCost.Text, out int n))
                {
                    Works work = new Works();
                    work.Id_work = ManIdWork;
                    work.Description_work = newDes.Text;
                    work.Cost_work = Convert.ToInt32(newCost.Text);
                    if (CheckAdd == true)
                        //RepWorks.Insert(work);
                        MessageBox.Show("Добавили");
                    else
                        RepWorks.Update(work);
                    MessageBox.Show("Данные успешно сохранены");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверно введены данные");
                    newCost.Text = "";
                }
            }
        }

        private void AddInfo()
        {
            if (ManIdWork == 0)
            {
                CheckAdd = true;
                return;
            }
            else
            {
                var work = (from w in RepWorks.SelectAll() where w.Id_work == ManIdWork select w).First();

                newDes.Text = work.Description_work;
                newCost.Text = work.Cost_work.ToString();
            }
        }
    }
}
