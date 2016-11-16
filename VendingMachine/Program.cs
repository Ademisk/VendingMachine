using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Data;

/*Tests to Add:
 - Handle case where buyer overpaid for item
 - Check out of stock before if enough money inserted

*/

// To Do:
// - Add Moqing to unit tests
// - Parametarize methods in unit tests

// - Look into decoupling further

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Vendor vendor = VendorFactory.GetVendor();

            vendor.ViewItem("Z9");                  //Outputs to console itemName, cost, quantity if present, or "Empty" if no item

            List<Coin> money = new List<Coin>();    //Collection of money to be inserted
            money.Add(Coin.quarter);
            money.Add(Coin.quarter);
            money.Add(Coin.quarter);
            money.Add(Coin.quarter);
            money.Add(Coin.quarter);
            money.Add(Coin.quarter);
            money.Add(Coin.quarter);
            money.Add(Coin.quarter);
            money.Add(Coin.dime);
            money.Add(Coin.dime);
            money.Add(Coin.nickel);

            vendor.InsertMoney(money);              //
            Item item = vendor.GetItem("A3");       //Attempt to make selection. If enough money, return item. Else it's null. Output status to console inside

            vendor.ViewItem("A3");                  //Outputs to console itemName, cost, quantity if present, or "Empty" if no item

            money = vendor.CancelTransaction();     //Returns insert money
            vendor.InsertMoney(money);
            item = vendor.GetItem("A3");

            Console.ReadKey();
        }
    }
}