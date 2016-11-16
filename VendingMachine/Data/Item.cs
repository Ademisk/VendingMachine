using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Data
{
    public class Item
    {
        const string DEFAULT_NAME = "Item";

        string name;
        double price;

        public Item(string n, double p)
        {
            if (n.Length < 1)
                name = DEFAULT_NAME;
            else if (n.Length > 16)
                name = n.Substring(0, 16);
            else
                name = n;

            if (p >= 0.00)
                price = p;
            else
                price = 0.00;
        }

        public string GetName()
        {
            return name;
        }

        public double GetPrice()
        {
            return price;
        }
    }
}
