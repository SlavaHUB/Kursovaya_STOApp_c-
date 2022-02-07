using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepWorks
    {
        static public void Insert(Works work)
        {
            MyDb.Db.Connection.Open();

            string query = "INSERT INTO Works (Description_work, Cost_work) VALUES (@descr, @cost)";

            OleDbCommand insert = new OleDbCommand(query, MyDb.Db.Connection);
            insert.Parameters.AddWithValue("@descr", work.Description_work);
            insert.Parameters.AddWithValue("@cost", work.Cost_work);

            insert.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }
        static public List<Works> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Works> lst = new List<Works>();

            string query = "select * from Works";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Works work = new Works();
                    work.Id_work = Convert.ToInt32(reader.GetValue(0));
                    work.Description_work = reader.GetValue(1).ToString();
                    work.Cost_work = Convert.ToInt32(reader.GetValue(2));
                    lst.Add(work);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }

        static public void Update(Works work)
        {
            MyDb.Db.Connection.Open();

            string query = "UPDATE Works SET Description_work = @newDes, Cost_work = @newCost WHERE Id_work = @id";

            OleDbCommand update = new OleDbCommand(query, MyDb.Db.Connection);
            update.Parameters.AddWithValue("@newDes", work.Description_work);
            update.Parameters.AddWithValue("@newCost", work.Cost_work);
            update.Parameters.AddWithValue("id", work.Id_work);

            update.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }

        static public void Delete(int id)
        {
            MyDb.Db.Connection.Open();

            string query = "delete from Works where Id_work = @id";
            OleDbCommand delete = new OleDbCommand(query, MyDb.Db.Connection);
            delete.Parameters.AddWithValue("@id", id);

            delete.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }
    }
}
