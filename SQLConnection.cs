using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;

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
            int point = 0;
            string res = "";
            using (conn)
            {
                string sql = "Select ProductName, Points, count (UserId) as 'Counter' from dbo.HistoryDataSet where UserId = '2382' group by ProductName, Points";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    Console.WriteLine("Connecting!");
                    conn.Open();
                    Console.WriteLine($"{DateTime.Now} - Connection Successful!");
                    
                    using (SqlDataReader read = comm.ExecuteReader())
                    {
                        Console.WriteLine("==========Отладочная информация==========");
                        while (read.Read())
                        {
                            //point = (int)read.GetValue(2);
                            Console.WriteLine(point);
                            res += read.GetString(0) + " " + read.GetValue(1) + " " + read.GetValue(2) + '\n';
                        }
                    }
                }
            }
            Console.WriteLine(res);
        }
    }
}
