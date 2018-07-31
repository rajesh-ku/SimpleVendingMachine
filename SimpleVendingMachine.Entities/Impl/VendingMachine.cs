using System;
using System.Linq;
using SimpleVendingMachine.Entities.Exceptions;
using SimpleVendingMachine.Entities.Interfaces;

namespace SimpleVendingMachine.Entities.Impl
{
    public class VendingMachine : IVendingMachine<CashCard, Account>
    {
        private static readonly Object lockObject = new object();
        private readonly IInventory _inventory;
        public Product PurchasedProduct { get; private set; }
        public int PurchasedQuantity { get; private set; }
        public decimal TotalAmount {  get { return (PurchasedProduct.Cost * PurchasedQuantity);  } }

        public VendingMachine(IInventory inventory)
        {
            _inventory = inventory;  
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Select the product id / quantity from the list of products displayed below:");
            foreach (var product in _inventory.AllInventories.Keys)
            {
                Console.WriteLine(string.Format("{0}. {1}", product.ProductId, product.Name));
            }
        }

        public int SelectItemToPurchase()
        {
            var selectedProduct = ReadNumericData();
            PurchasedProduct = _inventory.AllInventories.Keys.FirstOrDefault(p => p.ProductId == selectedProduct);
            while (PurchasedProduct == null)
            {
                Console.WriteLine("ERROR: No such product Exists.. please select again...");
                Console.WriteLine();
                DisplayInventory();
                selectedProduct = ReadNumericData();
                PurchasedProduct = _inventory.AllInventories.Keys.FirstOrDefault(p => p.ProductId == selectedProduct);
            }
            Console.WriteLine(string.Format("DEBUG: Product selected for purchase: {0}", PurchasedProduct.ToString()));
            return selectedProduct;
        }

        public int SelectQuantityToPurchase()
        {
            Console.Write("How many quantities you need? ");
            PurchasedQuantity = ReadNumericData();
            while(PurchasedQuantity <= 0)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR: (Please enter a positive number)");
                Console.Write("How many quantities you need? ");
                PurchasedQuantity = ReadNumericData();
            }
            Console.WriteLine("DEBUG: Quantity Purchased: {0}", PurchasedQuantity);
            return PurchasedQuantity;
        }

        public void AcceptPayment(Account accountOrCash)
        {
            // No product has been purchased return
            if (PurchasedProduct == null) return;

            if(accountOrCash.Balance < TotalAmount)
            {
                throw new AccountException(string.Format("ERROR: Account balance is NOT sufficient to dispense selected product."));
            }
            else
            {
                Console.WriteLine("DEBUG: Please wait .. processing transaction... ");
                lock (lockObject)
                {
                    Console.WriteLine(string.Format("DEBUG: Withdrawing money {0} from account .. ", TotalAmount));
                    accountOrCash.Withdraw(TotalAmount);
                    Console.WriteLine("DEBUG: Updating Inventory for .. {0} - Previous Quantity: {1} - Final Quantity: {2}", PurchasedProduct.Name, _inventory.AllInventories[PurchasedProduct], _inventory.AllInventories[PurchasedProduct] -= PurchasedQuantity);
                }
            }
        }

        public bool ValidatePaymentMethod(CashCard paymentMethod)
        {
            return paymentMethod.ValidatePin();
        }

        private int ReadNumericData()
        {
            var result = 0;
            try
            {
                result = int.Parse(Console.ReadLine());
            }
            catch
            {
                // Ignore any error
            }
            return result;
        }
    }
}
