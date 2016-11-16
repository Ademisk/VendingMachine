using System;
using VendingMachine.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTest.Tests
{
    [TestClass]
    public class ItemTest
    {
        [TestMethod]
        public void CreateItemTest()
        {
            Item it = new Item("Candy", 2.25);

            Assert.AreEqual(it.GetName(), "Candy");
            Assert.AreEqual(it.GetPrice(), 2.25);
        }

        [TestMethod]
        public void MinPriceTest()
        {
            Item it = new Item("Candy", -1.00);

            Assert.AreNotEqual(it.GetPrice(), -1.00);
            Assert.AreEqual(it.GetPrice(), 0.00);
        }

        [TestMethod]
        public void NameDefaultTest()
        {
            Item it = new Item("", 2.00);

            Assert.AreNotEqual(it.GetName(), "");
            Assert.AreEqual(it.GetName(), "Item");
        }

        [TestMethod]
        public void NameMax16CharsTest()
        {
            string nameString = "abcdefghijklmnopq";
            Item it = new Item(nameString, 2.00);

            Assert.AreNotEqual(it.GetName(), nameString);
            Assert.AreEqual(it.GetName(), nameString.Substring(0, 16));
        }
    }
}
