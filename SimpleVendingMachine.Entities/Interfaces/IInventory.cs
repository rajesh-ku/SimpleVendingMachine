using System.Collections.Generic;

namespace SimpleVendingMachine.Entities.Interfaces
{
    public interface IInventory
    {
        /// <summary>
        /// A dictionary property which stores the list of all products and their stock 
        /// </summary>
        IDictionary<Product, int> AllInventories { get; }

        /// <summary>
        /// A generic method to allow inventory to load products directory from a file / database or any external 
        /// sources.  
        /// </summary>
        void LoadInventory();

        /// <summary>
        /// SetInventory allows user to supply additional products which can be added to the list of 
        /// All Inventories
        /// </summary>
        /// <param name="products"></param>
        void SetInventory(IEnumerable<Product> products);

        /// <summary>
        /// Allows the calling program to check if the number of number of requested quantity is available in the stock
        /// If the requested quantity is not available, an exception InventoryException() is thrown.
        /// It also accepts a maximum item allowed, as per the requirement.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="maximumQuantityAllowed"></param>
        /// <returns></returns>
        bool ValidatePurchase(int productId, int quantity, int maximumQuantityAllowed);
    }
}
