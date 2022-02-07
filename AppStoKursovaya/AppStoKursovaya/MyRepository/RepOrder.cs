using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepOrder
    {
        static public void Insert(Orders order)
        {
            MyDb.Db.Connection.Open();

            string query = "insert into Orders (Id_work, Id_client, Id_master, Id_checker, Id_auto, Status_order, Date_registr) " +
                                    "values (@idWork, @idClient, @idMaster, @idChecker, @idAuto, @status, @date)";
            OleDbCommand command = new OleDbCommand(query, MyDb.Db.Connection);
            command.Parameters.AddWithValue("@idWork", order.Id_work);
            command.Parameters.AddWithValue("@idClient", order.Id_client);
            command.Parameters.AddWithValue("@idMaster", DBNull.Value);
            command.Parameters.AddWithValue("@idChecker", DBNull.Value);
            command.Parameters.AddWithValue("@idAuto", order.Id_auto);
            command.Parameters.AddWithValue("@status", "в ожидании");
            command.Parameters.AddWithValue("@date", order.Date_registr);

            command.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }
        static public List<Orders> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Orders> lst = new List<Orders>();

            string query = "SELECT Orders.* FROM[Orders]";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Orders order = new Orders();
                    order.Id_order = Convert.ToInt32(reader.GetValue(0));
                    order.Id_work = Convert.ToInt32(reader.GetValue(1));
                    order.Id_client = Convert.ToInt32(reader.GetValue(2));

                    if (reader.GetValue(3) != DBNull.Value)
                        order.Id_master = Convert.ToInt32(reader.GetValue(3));

                    if (reader.GetValue(4) != DBNull.Value)
                        order.Id_checker = Convert.ToInt32(reader.GetValue(4));


                    order.Id_auto = Convert.ToInt32(reader.GetValue(5));
                    order.Status_order = reader.GetValue(6).ToString();
                    order.Date_registr = reader.GetValue(7).ToString();
                    order.Payment = Convert.ToInt32(reader.GetValue(8));
                    lst.Add(order);
                }
            }
            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }

        static public void Update(Orders order)
        {
            MyDb.Db.Connection.Open();

            string query = "UPDATE Orders SET Id_master = @idM, Id_checker = @idC, Status_order = @newInfo WHERE Id_order = @id";
            OleDbCommand update = new OleDbCommand(query, MyDb.Db.Connection);
            update.Parameters.AddWithValue("@idM", order.Id_master);
            update.Parameters.AddWithValue("@idC", order.Id_checker);
            update.Parameters.AddWithValue("@newInfo", order.Status_order);
            update.Parameters.AddWithValue("@id", order.Id_order);
            update.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }

        static public void UpdateStatus(Orders order)
        {
            MyDb.Db.Connection.Open();

            string query = "UPDATE Orders SET Status_order = @newInfo WHERE Id_order = @id";
            OleDbCommand update = new OleDbCommand(query, MyDb.Db.Connection);
            update.Parameters.AddWithValue("@newInfo", order.Status_order);
            update.Parameters.AddWithValue("@id", order.Id_order);
            update.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }

        static public void UpdatePayment(Orders order)
        {
            MyDb.Db.Connection.Open();

            string query = "UPDATE Orders SET Payment = @newInfo WHERE Id_order = @id";
            OleDbCommand update = new OleDbCommand(query, MyDb.Db.Connection);
            update.Parameters.AddWithValue("@newInfo", order.Payment);
            update.Parameters.AddWithValue("@id", order.Id_order);
            update.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }
    }
}
