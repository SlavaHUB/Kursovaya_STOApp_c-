using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    static class RepAutos
    {
        static public void Insert(Autos auto)
        {
            MyDb.Db.Connection.Open();

            string query = "insert into Autos (Year_release, Id_mark, Type_engine, Gosnomer) " +
                                    "values (@year, @mark, @engine, @gos)";
            OleDbCommand command = new OleDbCommand(query, MyDb.Db.Connection);
            command.Parameters.AddWithValue("@year", auto.Year_release);
            command.Parameters.AddWithValue("@mark", auto.Id_mark);
            command.Parameters.AddWithValue("@engin", auto.Type_engine);
            command.Parameters.AddWithValue("@gos", auto.Gosnomer);

            command.ExecuteNonQuery();

            MyDb.Db.Connection.Close();
        }
        static public List<Autos> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Autos> lst = new List<Autos>();
            string query = "select * from Autos";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if(reader.HasRows)
            {
                while(reader.Read())
                {
                    Autos auto = new Autos();
                    auto.Id_auto = Convert.ToInt32(reader.GetValue(0));
                    auto.Year_release = Convert.ToInt32(reader.GetValue(1));
                    auto.Id_mark = Convert.ToInt32(reader.GetValue(2));
                    auto.Type_engine = reader.GetValue(3).ToString();
                    auto.Gosnomer = reader.GetValue(4).ToString();
                    lst.Add(auto);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }

        static public Autos SelectByGosnomer(string gosnom)
        {
            MyDb.Db.Connection.Open();

            Autos auto = new Autos();
            string query = "select * from Autos where Gosnomer = @gosnom";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            select.Parameters.AddWithValue("@gosNom", gosnom);
            OleDbDataReader reader = select.ExecuteReader();
            if(!reader.HasRows)
            {
                reader.Close();
                MyDb.Db.Connection.Close();
                return null;
            }
            else
            {
                while (reader.Read())
                {
                    auto.Id_auto = Convert.ToInt32(reader.GetValue(0));
                    auto.Year_release = Convert.ToInt32(reader.GetValue(1));
                    auto.Id_mark = Convert.ToInt32(reader.GetValue(2));
                    auto.Type_engine = reader.GetValue(3).ToString();
                    auto.Gosnomer = reader.GetValue(4).ToString();
                }
                reader.Close();
                MyDb.Db.Connection.Close();
                return auto;
            }
        }
    }
}
