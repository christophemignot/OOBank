using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOBankTest
{
    using System.Collections;

    public class Operation
    {
        public double Amount;

        public DateTime Date;

        public double Balance;

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", Amount, Date, Balance);
        }
    }

    public class Statements : IEnumerable<Operation>
    {
        private readonly IList<Operation> operations;
        private readonly IDateProvider dateProvider;

        public Statements(IDateProvider dateProvider)
        {
            this.operations = new List<Operation>();
            this.dateProvider = dateProvider;
        }

        public void AddOperation(double amount)
        {
            var balance = this.GetBalance() + amount;
            operations.Add(new Operation { Amount = amount, Date = dateProvider.GetDate(), Balance = balance});
        }

        public IEnumerator<Operation> GetEnumerator()
        {
            return operations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public double GetBalance()
        {
            if (operations.Count <= 0)
            {
                return 0;
            }
            var lastOperationIndex = operations.Count - 1;
            return operations[lastOperationIndex].Balance;
        }

        public override string ToString()
        {
            var result = "";
            foreach (var operation in operations)
            {
                result += operation + Environment.NewLine;
            }
            return result;
        }
    }
}
