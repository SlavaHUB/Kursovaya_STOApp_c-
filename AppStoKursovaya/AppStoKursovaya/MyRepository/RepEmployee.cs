using AppStoKursovaya.Items;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyRepository
{
    class RepEmployee
    {
        static public List<Employee> SelectAll()
        {
            MyDb.Db.Connection.Open();

            List<Employee> lst = new List<Employee>();

            string query = "select * from Employee";
            OleDbCommand select = new OleDbCommand(query, MyDb.Db.Connection);
            OleDbDataReader reader = select.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.Id_employee = reader.GetInt32(0);
                    employee.FIO = reader.GetString(1);
                    employee.Id_position = Convert.ToInt32(reader.GetValue(2));
                    employee.Id_user = reader.GetInt32(3);
                    employee.Private_data = reader.GetValue(4).ToString();
                    lst.Add(employee);
                }
            }

            reader.Close();
            MyDb.Db.Connection.Close();
            return lst;
        }
    }
}
