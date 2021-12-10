using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CyberGardenFIrst
{
    public class AdminPoint
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public DateTime LoginTime { get; set; }
        public bool Status { get; set; }
        public bool CheckLog(AdminPoint AP)
        {

            Console.WriteLine($"T: {DateTime.Now} - connecting");

            return true;

        }
    }
}
