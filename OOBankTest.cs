using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OOBankTest
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [TestClass]
    public class OOBankTest
    {
        [TestMethod]
        public void WhenDepositThenAddMoney()
        {
            var account = new Account(new MockDateProvider());
            account.Deposit(100.0);

            Assert.AreEqual(100.0, account.GetBalance());

        }

        [TestMethod]
        public void WhenCreateAccountWith100Moneys()
        {
            var account = new Account(100.0, new MockDateProvider());
         

            Assert.AreEqual(100.0, account.GetBalance());
        }

        [TestMethod]
        public void WhenWithdrawalThenRemoveMoney()
        {
            var account = new Account(100.0, new MockDateProvider());
            account.Withdrawal(50.0);

            Assert.AreEqual(50.0, account.GetBalance());
        }

        [TestMethod]
        public void WhenTransferMoneyThenDestinationAccountRaised()
        {
            var accountSource = new Account(100.0, new MockDateProvider());
            var accountDestination = new Account(1000.0, new MockDateProvider());
            
            accountSource.Transfert(accountDestination, 100.0);


            Assert.AreEqual(0.0, accountSource.GetBalance());
            Assert.AreEqual(1100.0, accountDestination.GetBalance());
        }

        [TestMethod]
        public void WhenTransferInvalidAmountThenDestinationAccountRaisedEXception()
        {
            var accountSource = new Account(100.0, new MockDateProvider());
            var accountDestination = new Account(1000.0, new MockDateProvider());
            var exceptionRaised = false;
            try
            {
                accountSource.Transfert(accountDestination, -100.0);
            }
            catch (ArgumentException e)
            {
                exceptionRaised = (e.ParamName == "amount" && e.Message=="Montant invalide\r\nNom du paramètre : amount");
            }
            Assert.IsTrue(exceptionRaised);
        }

        [TestMethod]
        public void WhenOperationThenStatementIncreased()
        {
            var account = new Account(new MockDateProvider());
            account.Deposit(100.0);

            var statements = account.GetStatements();

            Assert.AreEqual(2, statements.Count());
        }

        [TestMethod]
        public void WhenPrintingAccountResultIsOK()
        {
            var account = new Account(new MockDateProvider());
            account.Deposit(100.0);
            account.Deposit(50.0);
            account.Withdrawal(71.0);
            Assert.AreEqual(@"Amount	Date	Balance
0	01/02/2014 09:00:00	0
100	01/02/2014 10:00:00	100
50	01/02/2014 11:00:00	150
-71	01/02/2014 12:00:00	79
", account.ToString());
        }
    }
}
