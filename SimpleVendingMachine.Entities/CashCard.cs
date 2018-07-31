using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVendingMachine.Entities
{
    public class CashCard 
    {
        // ASSUMPTION: This card has a read only CASH Card Pin
        public int Pin { get { return 9191; } }

        // When any cash card is used, a user provides a new pin to allow access 
        // to its card and its account
        public int TransactionPin { get; private set; }

        public CashCard(int transactionPin)
        {
            TransactionPin = transactionPin;
        }

        public bool ValidatePin()
        {
            return (Pin == TransactionPin);
        }
    }
}
