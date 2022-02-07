using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepMarks
    {
        static public List<Marks> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Marks> lst = new List<Marks>();

            string query = "select * from Marks";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Marks mark = new Marks();

                    mark.Id_mark = Convert.ToInt32(reader.GetValue(0));
                    mark.Name_mark = reader.GetValue(1).ToString();
                    lst.Add(mark);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }
    }
}
