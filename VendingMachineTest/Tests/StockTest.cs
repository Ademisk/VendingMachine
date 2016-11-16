using System;
using VendingMachine.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTest.Tests
{
    [TestClass]
    public class StockTest
    {
        [TestMethod]
        public void GetItem2InStockTest()
        {
            Stock s = new Stock(new Item("Candy", 2.00), 2);

            Assert.AreEqual(s.GetQuantity(), 2);
            Item it = s.GetOneItem();
            Assert.AreEqual(s.GetQuantity(), 1);
        }

        [TestMethod]
        public void GetItem0IStockTest()
        {
            Stock s = new Stock(new Item("Candy", 2.00), 0);
            
            Item it = s.GetOneItem();
            Assert.AreEqual(it, null);
            Assert.AreEqual(s.GetQuantity(), 0);
        }
    }
}
