using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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

        public string FindBigSales(string id)
        {
            string connLine = $"Data Source={DataSource};Initial Catalog={InitialCatalog};Persist Security Info={PersistSecurityInfo};User ID={UserID};Password={Password}";
            SqlConnection conn = new SqlConnection(connLine);
            JSONParamsPrioritet JSONClass = new JSONParamsPrioritet();
            string res = "";
            List<JSONParamsPrioritet> list = new List<JSONParamsPrioritet>();
            using (conn)
            {
                string sql = $"Select top (10) ProductName, Points, count (UserId) as 'Counter' from dbo.HistoryDataSet where UserId = '{id}' group by ProductName, Points order by Counter DESC";
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
                            JSONClass.ProductName = read.GetString(0);
                            if ((int)read.GetValue(2) >= 5)
                            {
                                JSONClass.Prior = 5.ToString();
                            }
                            else if ((int)read.GetValue(2) < 5)
                            {
                                Random rand = new Random();
                                JSONClass.Prior = rand.Next(1, 4).ToString();
                            }
                            res += read.GetString(0) + " " + read.GetValue(1) + " " + read.GetValue(2) + "\n";
                            list.Add(JSONClass);
                        }
                        Console.WriteLine("==========Отладочная информация==========");
                    }
                }
            }
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            string result = "";
            foreach (var a in list)
            {
                var json = JsonSerializer.Serialize<JSONParamsPrioritet>(a, options);
                result += json + "\n";
            }
            Console.WriteLine(res);
            return result;
        }

        public void TopOfMerchants()
        {
            string connLine = $"Data Source={DataSource};Initial Catalog={InitialCatalog};Persist Security Info={PersistSecurityInfo};User ID={UserID};Password={Password}";
            SqlConnection conn = new SqlConnection(connLine);
            List<JSONParamsPrioritet> list = new List<JSONParamsPrioritet>();
            JSONParamsPrioritet JSPAP = new JSONParamsPrioritet();
            string res = "";
            using (conn)
            {
                string sql = "Select top 10 ProductName, count (UserId) as 'Counter' from dbo.HistoryDataSet group by ProductName order by Counter DESC";
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
                            JSPAP.ProductName = read.GetString(0);
                            Console.WriteLine(JSPAP.ProductName);
                            JSPAP.Counter = read.GetString(1);
                            res += read.GetString(0) + " " + read.GetValue(1) + "\n";
                            Console.WriteLine(res);
                            list.Add(JSPAP);
                        }
                        Console.WriteLine("==========Отладочная информация==========");
                    }
                }
            }
            Console.WriteLine(res);
        }
    }
}
