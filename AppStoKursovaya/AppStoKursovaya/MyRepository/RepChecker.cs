using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepChecker
    {
        static public List<Checker> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Checker> lst = new List<Checker>();
            string query = "select * from Checker";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Checker checker = new Checker();
                    checker.Id_checker = Convert.ToInt32(reader.GetValue(0));
                    checker.Id_employee = Convert.ToInt32(reader.GetValue(1));
                    lst.Add(checker);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }
    }
}
