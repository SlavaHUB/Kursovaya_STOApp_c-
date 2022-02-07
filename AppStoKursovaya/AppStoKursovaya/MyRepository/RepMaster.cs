using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepMaster
    {
        static public List<Master> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Master> lst = new List<Master>();
            string query = "select * from Master";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Master master = new Master();
                    master.Id_master = Convert.ToInt32(reader.GetValue(0));
                    master.Id_employee = Convert.ToInt32(reader.GetValue(1));
                    lst.Add(master);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }
    }
}
