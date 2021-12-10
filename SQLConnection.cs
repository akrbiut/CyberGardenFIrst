using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CyberGardenFIrst
{
    public class SQLConnection
    {
        /*
         * Data Source=cgosmire.database.windows.net;Initial Catalog=CG;Persist Security Info=True;User ID=akrbout;Password=UQQeoCSK1290
         */
        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string PersistSecurityInfo { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public SQLConnection MakeSomeSettings()
        {
            DataSource = Properties.Settings.Default.DataSource;
            InitialCatalog = Properties.Settings.Default.InitialCatalog;
            PersistSecurityInfo = Properties.Settings.Default.PersistSecurityInfo;
            UserID = Properties.Settings.Default.UserID;
            Password = Properties.Settings.Default.Password;
            return this;
        }
        public void ConnectToBD()
        {
            string connLine = $"Data Source={DataSource};Initial Catalog={InitialCatalog};Persist Security Info={PersistSecurityInfo};User ID={UserID};Password={Password}";
            SqlConnection conn = new SqlConnection(connLine);
            try
            {
                Console.WriteLine("Connecting!");
                conn.Open();
                Console.WriteLine("Connection Successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
