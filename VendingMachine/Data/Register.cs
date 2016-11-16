using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Data
{
    public interface IRegister
    {
        void InsertMoney(List<Coin> money);
        double CheckInsertedMoney();
        List<Coin> ReturnMoney();
    }

    public class Register : IRegister
    {
        Dictionary<Coin, int> till;
        List<Coin> insertedMoney;

        public Register(List<Coin> money)
        {
            till = new Dictionary<Coin, int>();
            foreach (Coin c in money)
            {
                if (till.ContainsKey(c))
                    till[c]++;
                else
                    till.Add(c, 1);
            }

            insertedMoney = new List<Coin>();
        }

        public void InsertMoney(List<Coin> money)
        {
            insertedMoney.AddRange(money);
        }

        public double CheckInsertedMoney()
        {
            double total = 0.00;
            foreach (Coin c in insertedMoney)
            {
                if (c == Coin.quarter)
                    total += .25;
                else if (c == Coin.dime)
                    total += .10;
                else if (c == Coin.nickel)
                    total += .05;
            }

            return total;
        }

        public List<Coin> ReturnMoney()
        {
            List<Coin> temp = new List<Coin>();
            temp.AddRange(insertedMoney);
            insertedMoney.Clear();
            return temp;
        }

        public void ProcessTransaction()
        {
            StoreMoneyInTill();
        }
        
        private void StoreMoneyInTill()
        {
            foreach (Coin c in insertedMoney)
            {
                if (till.ContainsKey(c))
                    till[c]++;
                else
                    till.Add(c, 1);
            }

            insertedMoney.Clear();
        }
    }


}
