using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepPositions
    {
        static public List<Positions> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Positions> lst = new List<Positions>();
            string query = "select * from Positions";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Positions position = new Positions();
                    position.Id_position = Convert.ToInt32(reader.GetValue(0));
                    position.Name_position = reader.GetValue(1).ToString();
                    position.Salary = Convert.ToInt32(reader.GetValue(2));
                    position.Description_position = reader.GetValue(3).ToString();
                    lst.Add(position);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }
    }
}
