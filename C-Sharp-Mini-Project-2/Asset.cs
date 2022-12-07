using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Mini_Project_2
{
    internal class Asset
    {
        public Asset(string brand, string model, string office, DateTime purchase_date, int price)
        {
            Brand = brand;
            Model = model;
            Office = office;
            Purchase_date = purchase_date;
            Price = price;
        }

        public string GetCurrency()
        {
            if (Office == "USA")
            {
                return "USD";
            }
            else if (Office == "Sweden")
            {
                return "SEK";
            }
            else if (Office == "Spain")
            {
                return "EUR";
            }

            // failsafe text
            return "ERROR";
        }

        public double LocalPrice(string currency)
        {
            if (currency == "USD")
            {
                // no change
                return Price;
            }
            else if (currency == "SEK")
            {
                // USD to SEK
                return Price * 10.8;
            }
            else if (currency == "EUR")
            {
                // USD to EUR
                return Price * 0.99;
            }

            // fail number
            return -1;
        }

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Office { get; set; }
        public DateTime Purchase_date { get; set; }
        public int Price { get; set; }
    }
}
