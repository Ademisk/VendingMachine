using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Data
{
    public interface IStock
    {
        string GetName();
        double GetPrice();
        int GetQuantity();
        Item GetOneItem();
    }

    public class Stock : IStock
    {
        Item item;
        int quantity;

        public Stock(string name, double p, int q)
        {
            item = new Item(name, p);
            quantity = q;
        }

        public Stock(Item i, int q)
        {
            item = i;
            quantity = q;
        }

        public string GetName()
        {
            return item.GetName();
        }

        public double GetPrice()
        {
            return item.GetPrice();
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public Item GetOneItem()
        {
            if (quantity > 0)
            {
                quantity--;
                return item;
            }
            else
                return null;
        }
    }
}
