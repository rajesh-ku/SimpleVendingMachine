using System;
using System.Linq;
using SimpleVendingMachine.Entities.Interfaces;
using System.Collections.Generic;
using SimpleVendingMachine.Entities.Exceptions;

namespace SimpleVendingMachine.Entities.Impl
{
    public class Inventory : IInventory
    {
        public IDictionary<Product, int> AllInventories { get; private set; }

        public Inventory()
        {
            AllInventories = new Dictionary<Product, int>();
        }

        public void LoadInventory()
        {
            // This is a dummy method to load a set of products in inventory from external sources
            // like database / flat files or csv files 
            // for the purpose of this example, I am hardcoding the values 
            AllInventories.Add(new Product { ProductId = 1, Name = "Drink Can", Cost = 0.5m }, 20);
        }

        public void SetInventory(IEnumerable<Product> products)
        {
            foreach(var product in products)
            {
                if (AllInventories.Keys.Contains(product))
                {
                    AllInventories[product]++;
                }
                else
                {
                    AllInventories.Add(product, 1);
                }              
            }
        }

        public bool ValidatePurchase(int productId, int quantity, int maximumQuantityAllowed)
        {
            // VALIDATION 1: If the requested Purchase is more than the Maximum Allowed the application throws a 
            //               validation exception
            if (quantity > maximumQuantityAllowed)
            {
                throw new ValidationException(string.Format("ERROR: Maximum item allowed is : {0}, you requested : {1}. ", maximumQuantityAllowed, quantity));
            }

            var product = AllInventories.Keys.FirstOrDefault(p => p.ProductId == productId);
            if(product != null)
            {
                // VALIDATION 2: If the number of item requested is less that the item in inventory, 
                //               application should throw an error.
                var itemsInInventory = AllInventories[product];
                if (itemsInInventory < quantity)
                {
                    throw new InventoryException("ERROR: Insufficient product in the Inventory. Please choose a lower value.");
                }
                return true;
            }
            else
            {
                throw new KeyNotFoundException(string.Format("No such product found: {0}", product.ToString()));
            }
        }
    }
}
