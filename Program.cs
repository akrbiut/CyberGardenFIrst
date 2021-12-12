using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using SimpleHttp;

namespace CyberGardenFIrst
{
    internal class Program
    {
        static void Main(string[] args)
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
            }
            SQLConnection SQLcon = new SQLConnection();
            SQLcon = SQLcon.MakeSomeSettings();
            //Прогрузка HttpListener
            Route.Add("/", (req, res, props) =>
            {
                res.AsText("Osmire (two).");
            });
            Route.Add("/users/{id}", (req, res, props) =>
            {
                res.AsText($"Client ID: {props["id"]}");
            });
            Route.Add("/{id}", (req, res, props) =>
            {
                res.AsText(SQLcon.FindBigSales(props["id"]));
            });
            Route.Add("/topmerchant", (req, res, props) =>
            {
                SQLcon.TopOfMerchants();
                res.AsText($"Client ID: {props["id"]}");
            });

            HttpServer.ListenAsync(80, CancellationToken.None, Route.OnHttpRequestAsync).Wait();
        }
    }
}
