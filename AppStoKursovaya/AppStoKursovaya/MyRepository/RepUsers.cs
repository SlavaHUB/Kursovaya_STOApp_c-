using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepUsers
    {
        static public void Insert(Users user)
        {
            MyDb.Db.Connection.Open();

            string query = "insert into Users (UsLogin, UsPassword, Rank) " +
                                    "values (@log, @pass, @rank)";
            OleDbCommand command = new OleDbCommand(query, MyDb.Db.Connection);
            command.Parameters.AddWithValue("@log", user.UsLogin);
            command.Parameters.AddWithValue("@pass", user.UsPassword);
            command.Parameters.AddWithValue("@rank", user.Rank);

            command.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }
        static public List<Users> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Users> lst = new List<Users>();
            string query = "select * from Users";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Users user = new Users();
                    user.Id_user = Convert.ToInt32(reader.GetValue(0));
                    user.UsLogin = reader.GetValue(1).ToString();
                    user.UsPassword = reader.GetValue(2).ToString();
                    user.Rank = reader.GetValue(3).ToString();
                    lst.Add(user);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }
        static public Users SelectByLogPass(string login, string pass)
        {
            MyDb.Db.Connection.Open();

            Users user = new Users();
            string query = "select * from Users where UsLogin = @login and UsPassword = @pass";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            select.Parameters.AddWithValue("@id", login);
            select.Parameters.AddWithValue("@pass", pass);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.Id_user = Convert.ToInt32(reader.GetValue(0));
                    user.UsLogin = reader.GetValue(1).ToString();
                    user.UsPassword = reader.GetValue(2).ToString();
                    user.Rank = reader.GetValue(3).ToString();
                }
            }
            else
            {
                reader.Close();
                MyDb.Db.Connection.Close();
                return null;
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return user;
        }
    }
}
