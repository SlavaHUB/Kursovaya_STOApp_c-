using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.MyDb
{
    static class Db
    {
        private readonly static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"D:\\Документы Д\\Программирование\\Course works\\CourseWork c# STOapp\\Kursovaya_STOApp_c#\\BD_kursach.mdb\"";
        private static readonly OleDbConnection myConnection = new OleDbConnection(connectString);

        static public OleDbConnection Connection => myConnection;
    }
}
