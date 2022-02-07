using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepClients
    {
        static public void Insert(Clients client)
        {
            MyDb.Db.Connection.Open();

            string query = "INSERT INTO Clients (Phone, FIO, Id_user) VALUES (@p, @fio, @id)";

            OleDbCommand insert = new OleDbCommand(query, MyDb.Db.Connection);
            insert.Parameters.AddWithValue("@p", client.Phone);
            insert.Parameters.AddWithValue("@fio", client.FIO);
            insert.Parameters.AddWithValue("@id", client.Id_user);

            insert.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }
        static public List<Clients> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Clients> lst = new List<Clients>();
            
            string query = "select * from Clients";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Clients client = new Clients();
                    client.Id_client = Convert.ToInt32(reader.GetValue(0));
                    client.Phone = reader.GetValue(1).ToString();
                    client.FIO = reader.GetValue(2).ToString();
                    client.Id_user = Convert.ToInt32(reader.GetValue(3));
                    client.Feedback = reader.GetValue(4).ToString();
                    lst.Add(client);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }

        static public bool SelectById(int id)
        {
            MyDb.Db.Connection.Open();

            string query = "select * from Clients where Id_client = @id";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            select.Parameters.AddWithValue("@id", id);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                MyDb.Db.Connection.Close();
                return true;
            }
            else
            {
                reader.Close();
                MyDb.Db.Connection.Close();
                return false;
            }
        }

        static public Clients SelectByIdUser(int idUser)
        {
            MyDb.Db.Connection.Open();

            Clients client = new Clients();
            string query = "select * from Clients where Id_user = @id";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            select.Parameters.AddWithValue("@id", idUser);
            OleDbDataReader reader = select.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                MyDb.Db.Connection.Close();
                return null;
            }
            else
            {
                while (reader.Read())
                {
                    client.Id_client = Convert.ToInt32(reader.GetValue(0));
                    client.Phone = reader.GetValue(1).ToString();
                    client.FIO = reader.GetValue(2).ToString();
                    client.Id_user = Convert.ToInt32(reader.GetValue(3));
                    client.Feedback = reader.GetValue(4).ToString();
                }
                reader.Close();
                MyDb.Db.Connection.Close();
                return client;
            }
        }

        static public void Update(Clients client)
        {
            MyDb.Db.Connection.Open();

            string query = "UPDATE Clients SET Feedback = @comm WHERE Id_client = @id";
            OleDbCommand update = new OleDbCommand(query, MyDb.Db.Connection);
            update.Parameters.AddWithValue("comm", client.Feedback);
            update.Parameters.AddWithValue("id", client.Id_client);

            update.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }
    }
}
