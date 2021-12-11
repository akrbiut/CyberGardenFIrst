using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace CyberGardenFIrst
{
    internal class Program
    {
        static
            void Main(string[] args)
        {
            //Авторизация в панель
            AdminPoint AP = new AdminPoint();
            Console.Write("WhoOoOo are you? : ");
            AP.Name = Console.ReadLine();
            Console.Write("Tell me our password, little boy : ");
            AP.Password = Console.ReadLine();
            //Синхронизация с БД
            if (AP.CheckLog(AP) == true)
            {
                Console.WriteLine($"Welcome, {AP.Role}: {AP.Name}. Time of Log: {AP.LoginTime}");
                Console.WriteLine($"Trying connection: {DateTime.Now}");
                SQLConnection SQLcon = new SQLConnection();
                SQLcon = SQLcon.MakeSomeSettings();
                SQLcon.ConnectToBD();
            }
            //Прогрузка HttpListener
            DoCheck();

            //Генерация ключей

        }

        public static async void DoCheck()
        {
            await Task.Run(() => CheckAndWrite());
        }

        public static async Task CheckAndWrite()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8888/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
            Console.ReadLine();

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                string responseString = "<html><head><meta charset='utf8'></head><body>Привет мир!</body></html>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();

            }
        }
    }
}
