using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;

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
            QSQL(conn);
        }

        public void QSQL(SqlConnection conn)
        {
            string res = "";
            using (conn)
            {
                string sql = "Select ProductName, count (UserId) as 'Counter' from dbo.HistoryDataSet where UserId = '679' group by ProductName";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    Console.WriteLine("Connecting!");
                    conn.Open();
                    Console.WriteLine($"{DateTime.Now} - Connection Successful!");
                    using (SqlDataReader read = comm.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            res += read.GetString(0) + read.GetValue(1);
                        }
                    }
                }
            }
        }
    }
}
