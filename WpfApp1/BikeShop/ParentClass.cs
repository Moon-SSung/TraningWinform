using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeShop
{
    public class ParentClass : BaseClass
    {
        public double Account { get; set; }
        public string LastNAme { get; set; }

        public double GiveAccount()
        {
            return Account;
        }
    }
}
