using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Mini_Project_2
{
    internal class Phone : Asset
    {
        public Phone(string brand, string model, string office, DateTime purchase_date, int price)
            : base(brand, model, office, purchase_date, price)
        { }
    }
}
