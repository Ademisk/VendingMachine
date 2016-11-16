using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Data;

namespace VendingMachine
{
    public class VendorFactory : Factory
    {
        public static Vendor GetVendor()
        {
            Dictionary<string, IStock> d = new Dictionary<string, IStock>();
            cat = new Catalog(d);
            cat.Add("A2", new Stock(new Item("Candy", 2.25), 0));
            cat.Add("A3", new Stock(new Item("Candy", 2.25), 1));
            List<Coin> coins = new List<Coin>();
            reg = new Register(coins);

            return new Vendor(cat, reg);
        }
    }
}
