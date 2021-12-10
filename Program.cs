using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("Tell me our password, little boy : ");
            AP.Password = Console.ReadLine();
            if (AP.CheckLog(AP) == true)
            {
                AP.LoginTime = DateTime.Now;
                AP.Role = "Admin";
                Console.WriteLine($"Welcome, {AP.Role}: {AP.Name}. Time of Log: {AP.LoginTime}");
                Console.WriteLine($"Trying connection: {DateTime.Now}");
                SQLConnection SQLcon = new SQLConnection();
                SQLcon = SQLcon.MakeSomeSettings();
                SQLcon.ConnectToBD();
                Console.ReadLine();
            }
            //Асинхронный запуск процесса
            
            //Прогрузка датасета

            //Генерация ключей

        }


    }
}
