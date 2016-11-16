using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Data
{
    public interface ICatalog
    {
        IStock ViewStockedItem(string key);
        Item GetItem(string key);
    }

    public class Catalog : ICatalog
    {
        Dictionary<string, IStock> cat;

        public Catalog(Dictionary<string, IStock> dic)
        {
            cat = dic;
        }

        public void Add(string key, Stock s)
        {
            if (s == null)
                return;

            if (!cat.ContainsKey(key))
            {
                cat.Add(key, s);
            }
            else
                cat[key] = s;
        }

        public void Add(string key, Item it)
        {
            if (it == null)
                return;

            if (!cat.ContainsKey(key))
            {
                cat.Add(key, new Stock(it, 1));
            }
            else
                cat[key] = new Stock(it, cat[key].GetQuantity());
        }

        public IStock ViewStockedItem(string key)
        {
            if (cat.ContainsKey(key))
                return cat[key];
            else
                return null;
        }

        public Item GetItem(string key)
        {
            if (cat.ContainsKey(key))
            {
                return cat[key].GetOneItem();
            }
            else
                return null;
        }
    }
}
