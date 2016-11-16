using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Data;

namespace VendingMachineTest.Tests
{
    [TestClass]
    public class CatalogTest
    {
        
        [TestMethod]
        public void AddItemAndGetTest()
        {
            string cName = "Candy";
            double cPrice = 2.00;
            int cQuantity = 1;
            string key = "A3";

            Catalog c = new Catalog(new Dictionary<string, IStock>());
            c.Add(key, new Item(cName, cPrice));
            Item it = c.GetItem(key);

            Assert.AreEqual(it.GetName(), cName);
            Assert.AreEqual(it.GetPrice(), cPrice);
        }

        [TestMethod]
        public void AddItemAndGetFailTest()
        {
            string cName = "Candy";
            double cPrice = 2.00;
            int cQuantity = 1;
            string key = "A3";

            Catalog c = new Catalog(new Dictionary<string, IStock>());
            c.Add(key, new Item(cName, cPrice));
            Item it = c.GetItem("Z9");

            Assert.AreEqual(it, null);
        }

        [TestMethod]
        public void AddStockAndViewStockedItemTest()
        {
            string cName = "Candy";
            double cPrice = 2.00;
            int cQuantity = 1;
            string key = "A3";

            Catalog c = new Catalog(new Dictionary<string, IStock>());
            c.Add(key, new Stock(new Item(cName, cPrice), cQuantity));
            IStock s = c.ViewStockedItem(key);

            Assert.AreEqual(s.GetName(), cName);
            Assert.AreEqual(s.GetPrice(), cPrice);
            Assert.AreEqual(s.GetQuantity(), cQuantity);
        }

        [TestMethod]
        public void AddStockAndViewStockedItemFailTest()
        {
            string cName = "Candy";
            double cPrice = 2.00;
            int cQuantity = 1;

            Catalog c = new Catalog(new Dictionary<string, IStock>());
            c.Add(cName, new Stock(new Item(cName, cPrice), cQuantity));
            IStock s = c.ViewStockedItem("Chips");

            Assert.AreEqual(s, null);
        }
    }
}
