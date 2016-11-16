using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine;
using VendingMachine.Data;
using System.Collections.Generic;
using Moq;

namespace VendingMachineTest
{
    [TestClass]
    public class VendorTest
    {
        FileStream fs;
        string testFile = "./consoleTest.txt";
        TextWriter tmp;
        StreamWriter sw;
        StreamReader sr;

        [TestInitialize]
        public void TestSetup()
        {
            File.Delete(testFile);
            fs = new FileStream(testFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            sw = new StreamWriter(fs);
            sr = new StreamReader(fs);
            tmp = Console.Out;
            Console.SetOut(sw);
        }

        [TestCleanup]
        public void TestTearDown()
        {
            Console.SetOut(tmp);

            sw.Close();
            sr.Close();
        }

        [TestMethod]
        public void GetVendorTest()
        {
            Vendor v = VendorFactory.GetVendor();

            Assert.AreNotEqual(null, v);
        }

        [TestMethod]
        public void ViewExistingItemTest()
        {
            //Arrange
            Mock<ICatalog> mCat = new Mock<ICatalog>();
            Mock<IRegister> mReg = new Mock<IRegister>();

            mCat.Setup(m => m.ViewStockedItem("A3")).Returns(new Stock("Candy", 2.25, 1));

            Vendor v = new Vendor(mCat.Object, mReg.Object);

            //Act
            v.ViewItem("A3");
            
            sw.Flush();
            fs.Position = 0;

            //Assert
            string expectedResult = "Candy, 1 - $2.25";
            string result = sr.ReadLine();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ViewNotExistingItemTest()
        {
            //Arrange
            Mock<ICatalog> mCat = new Mock<ICatalog>();
            Mock<IRegister> mReg = new Mock<IRegister>();

            mCat.Setup(m => m.ViewStockedItem("Z9")).Returns((Stock)null);

            Vendor v = new Vendor(mCat.Object, mReg.Object);

            //Act
            v.ViewItem("Z9");
            
            sw.Flush();
            fs.Position = 0;

            //Assert
            string expectedResult = "Item does not exist.";
            string result = sr.ReadLine();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetItemTest()
        {
            //Arrange
            Mock<ICatalog> mCat = new Mock<ICatalog>();
            Mock<IRegister> mReg = new Mock<IRegister>();
            Mock<IStock> mStock = new Mock<IStock>();

            List<Coin> coins = new List<Coin>();
            
            mStock.Setup(m => m.GetPrice()).Returns(2.25);
            mStock.Setup(m => m.GetQuantity()).Returns(1);
            mCat.Setup(m => m.ViewStockedItem("A3")).Returns(mStock.Object);
            mCat.Setup(m => m.GetItem("A3")).Returns(new Item("Candy", 2.25));
            mReg.Setup(m => m.InsertMoney(coins));
            mReg.Setup(m => m.CheckInsertedMoney()).Returns(2.25);
            
            Vendor v = new Vendor(mCat.Object, mReg.Object);

            //Act
            v.InsertMoney(coins);
            Item it = v.GetItem("A3");

            string expectedResult = "Candy";
            Assert.AreNotEqual(null, it);
            Assert.AreEqual(expectedResult, it.GetName());
        }

        [TestMethod]
        public void GetItemFailNotEnoughMoneyTest()
        {
            Vendor v = VendorFactory.GetVendor();
            List<Coin> coins = new List<Coin>();
            coins.Add(Coin.quarter);

            v.InsertMoney(coins);
            Item it = v.GetItem("A3");

            sw.Flush();
            fs.Position = 0;

            Assert.AreEqual(it, null);

            string expectedResult = "Please insert $2.00 more.";
            string result = sr.ReadLine();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetItemNoSuchItemTest()
        {
            Vendor v = VendorFactory.GetVendor();
            List<Coin> coins = new List<Coin>();
            coins.Add(Coin.quarter);

            v.InsertMoney(coins);
            Item it = v.GetItem("Z9");

            sw.Flush();
            fs.Position = 0;

            Assert.AreEqual(it, null);

            string expectedResult = "Item does not exist.";
            string result = sr.ReadLine();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void GetItemOutOfStockTest()
        {
            Vendor v = VendorFactory.GetVendor();
            List<Coin> coins = new List<Coin>();
            coins.Add(Coin.quarter);

            v.InsertMoney(coins);
            Item it = v.GetItem("A2");

            sw.Flush();
            fs.Position = 0;

            Assert.AreEqual(it, null);

            string expectedResult = "Item is out of stock.";
            string result = sr.ReadLine();
            Assert.AreEqual(expectedResult, result);
        }
    }
}
