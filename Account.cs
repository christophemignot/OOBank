using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOBankTest
{
    using System.Runtime.CompilerServices;
    using System.Security.Policy;

    public class Account
    {
        private double _balance;
        private readonly Statements innerOperations;

        public Account(IDateProvider dateProvider)
            : this(0, dateProvider)
        {

        }

        public Account(double initialAmount, IDateProvider dateProvider)
        {
            innerOperations = new Statements(dateProvider);
            innerOperations.AddOperation(initialAmount);
        }

        public void Deposit(double amount)
        {
            innerOperations.AddOperation(amount);
        }

        public void Withdrawal(double amount)
        {
            innerOperations.AddOperation(-1 * amount);
        }
        public double GetBalance()
        {
            return innerOperations.GetBalance();
        }

        public void Transfert(Account accountDestination, double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Montant invalide", "amount");
            }
            accountDestination.Deposit(amount);
            Withdrawal(amount);
        }

        public IEnumerable<Operation> GetStatements()
        {
            return innerOperations;
        }

        public override string ToString()
        {
            return "Amount\tDate\tBalance" + Environment.NewLine + innerOperations;
        }
    }
}
