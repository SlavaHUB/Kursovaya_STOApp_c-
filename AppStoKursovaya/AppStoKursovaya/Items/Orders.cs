using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStoKursovaya.Items
{
    class Orders
    {
        public int Id_order { get; set; }
        public int Id_work { get; set; }
        public int Id_client { get; set; }
        public int Id_master { get; set; }
        public int Id_checker { get; set; }
        public int Id_auto { get; set; }
        public string Status_order { get; set; }
        public string Date_registr { get; set; }
        public int Payment {get; set; }
    }
}
