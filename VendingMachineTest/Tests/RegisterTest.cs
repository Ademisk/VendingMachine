using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Data;

namespace VendingMachineTest.Tests
{
    [TestClass]
    public class RegisterTest
    {
        //Tests that money is counted correctly once inserted
        [TestMethod]
        public void CheckInsertedMoneyTest()
        {
            List<Coin> coins = new List<Coin>();
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.nickel);
            coins.Add(Coin.dime);
            coins.Add(Coin.dime);

            List<Coin> defaultMoney = new List<Coin>();

            Register r = new Register(defaultMoney);
            r.InsertMoney(coins);

            double moneyAsserted = 1.00;
            double iMoney = r.CheckInsertedMoney();
            Assert.AreEqual(iMoney, moneyAsserted);
        }

        [TestMethod]
        public void CheckMultipleInsertedMoneyTest()
        {
            List<Coin> coins = new List<Coin>();
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.nickel);
            coins.Add(Coin.dime);
            coins.Add(Coin.dime);

            List<Coin> defaultMoney = new List<Coin>();

            Register r = new Register(defaultMoney);
            r.InsertMoney(coins);

            double moneyAsserted = 1.00;
            Assert.AreEqual(r.CheckInsertedMoney(), moneyAsserted);

            List<Coin> moreMoney = new List<Coin>();
            moreMoney.Add(Coin.nickel);
            r.InsertMoney(moreMoney);

            double moneyAssertedAgain = 1.05;
            Assert.AreEqual(r.CheckInsertedMoney(), moneyAssertedAgain);
        }

        //Returns the same coins as those put in
        [TestMethod]
        public void ReturnMoneyCorrectCoinsTest()
        {
            List<Coin> coins = new List<Coin>();
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.nickel);
            coins.Add(Coin.dime);
            coins.Add(Coin.dime);

            List<Coin> defaultMoney = new List<Coin>();

            Register r = new Register(defaultMoney);
            r.InsertMoney(coins);

            List<Coin> returned = r.ReturnMoney();
            int qCount = 0;
            int dCount = 0;
            int nCount = 0;

            foreach(Coin c in returned)
            {
                if (c == Coin.quarter)
                    qCount++;
                else if (c == Coin.dime)
                    dCount++;
                else if (c == Coin.nickel)
                    nCount++;
            }

            Assert.AreEqual(qCount, 3);
            Assert.AreEqual(dCount, 2);
            Assert.AreEqual(nCount, 1);
        }

        //Makes sure register correctly realizes no money is left to return after money has been returned
        [TestMethod]
        public void ReturnMoneyNoneRemainInsideTest()
        {
            List<Coin> coins = new List<Coin>();
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.nickel);
            coins.Add(Coin.dime);
            coins.Add(Coin.dime);

            List<Coin> defaultMoney = new List<Coin>();

            Register r = new Register(defaultMoney);
            r.InsertMoney(coins);

            Assert.AreEqual(r.CheckInsertedMoney(), 1.00);
            List<Coin> returned = r.ReturnMoney();
            Assert.AreEqual(r.CheckInsertedMoney(), 0.00);
        }

        //Makes sure processed money can't be returned
        [TestMethod]
        public void ProcessTransactionTest()
        {
            List<Coin> coins = new List<Coin>();
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.quarter);
            coins.Add(Coin.nickel);
            coins.Add(Coin.dime);
            coins.Add(Coin.dime);

            List<Coin> defaultMoney = new List<Coin>();

            Register r = new Register(defaultMoney);
            r.InsertMoney(coins);

            Assert.AreEqual(r.CheckInsertedMoney(), 1.00);
            r.ProcessTransaction();
            Assert.AreEqual(r.CheckInsertedMoney(), 0.00);
        }
    }
}
