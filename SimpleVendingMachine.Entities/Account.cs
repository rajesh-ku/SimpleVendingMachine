using System;
using System.Collections.Generic;

namespace SimpleVendingMachine.Entities
{
    public class Account
    {
        private readonly Object lockObject = new Object();
        public IList<CashCard> LinkedCashCards { get; private set; }

        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }

        public Account(string accountNumber) : this(accountNumber, new List<CashCard>())
        {
        }

        public Account(string accountNumber, IList<CashCard> cashCards)
        {
            AccountNumber = accountNumber;
            if (cashCards != null)
            {
                LinkedCashCards = cashCards;
            }
        }

        public void Withdraw(decimal amount)
        {
            lock (lockObject)
            {
                if (amount > Balance)
                {
                    throw new Exception("Insufficient funds in account to withdraw!");
                }
                Balance -= amount;
            }
        }

        public void Deposit(decimal amount)
        {
            lock (lockObject)
            {
                Balance += amount;
            }
        }
    }
}
