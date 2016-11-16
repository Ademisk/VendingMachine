using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Data
{
    public class Vendor
    {
        ICatalog cat;
        IRegister reg;

        public Vendor(ICatalog c, IRegister r)
        {
            cat = c;
            reg = r;
        }

        public void ViewItem(string key)
        {
            IStock s = cat.ViewStockedItem(key);

            if (s != null)
                Console.Write("{0}, {1} - ${2}", s.GetName(), s.GetQuantity(), s.GetPrice());
            else
                Console.Write("Item does not exist.");
        }

        public void InsertMoney(List<Coin> money)
        {
            reg.InsertMoney(money);
        }

        public List<Coin> CancelTransaction()
        {
            return reg.ReturnMoney();
        }

        public Item GetItem(string key)
        {
            IStock s = cat.ViewStockedItem(key);
            Item it = null;

            if (s == null)
                Console.Write("Item does not exist.");
            else
            {
                double diff = reg.CheckInsertedMoney() - s.GetPrice();
                if (s.GetQuantity() > 0)
                {
                    if (diff >= 0.00)
                        it = cat.GetItem(key);
                    else
                        Console.Write("Please insert {0} more.", (diff * -1).ToString("C"));
                }
                else
                {
                    Console.Write("Item is out of stock.");
                }
            }
            
            return it;
        }
    }
}
