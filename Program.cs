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
            Console.WriteLine("WhoOoOo are you? : ");
            AP.Name = Console.ReadLine();
            Console.WriteLine("Tell me our password, little boy : ");
            AP.Password = Console.ReadLine();
            if (AP.CheckLog(AP) == true)
            {
                Console.WriteLine($"Welcome, {AP.Role}: {AP.Name}. Time of Log: {AP.LoginTime}");
            }
            //Асинхронный запуск процесса

            //Прогрузка датасета

            //Генерация ключей

            //
        }


    }
}
