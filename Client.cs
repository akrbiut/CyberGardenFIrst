using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CyberGardenFIrst
{
    public class Client
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string PhoneNumber { get; set; }
        public string SecondName { get; set; }
        public Rate MainRecomendation(Product prod, User TypeUser, Rate RT)
        {
            if (prod.Сounter >= 4)
            {
                prod.Favorite = true;
                prod.MerchantName = RT.MerchantName;
                
            }
            return RT;
        }
    }
}
