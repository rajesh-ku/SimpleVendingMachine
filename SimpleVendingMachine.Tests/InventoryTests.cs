using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleVendingMachine.Entities.Exceptions;
using SimpleVendingMachine.Entities.Impl;

namespace SimpleVendingMachine.Tests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void InitialisingAnInventoryReturns_ZeroProducts()
        {
            // Arrange
            var expectedProduct = 0;
            var inventory = new Inventory();

            // Act
            var result = inventory.AllInventories;

            // Assert
            Assert.AreEqual<int>(expectedProduct, result.Keys.Count);
        } 

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void WhenAMaximumItemIsRequested_ApplicationThrowsException()
        {
            // Arrange
            var inventory = new Inventory();
            inventory.LoadInventory();

            var productIdPurchased = 1;
            var quantityPurchased = 26;

            var maximumItemAllowed = 25;

            // Act 
            inventory.ValidatePurchase(productIdPurchased, quantityPurchased, maximumItemAllowed);

            // Assert
            // This Assert should throw an exception.
        }

        [TestMethod]
        [ExpectedException(typeof(InventoryException))]
        public void WhenAQuantityMoreThanInventoryIsRequested_ApplicationThrowsException()
        {
            // Arrange
            var inventory = new Inventory();
            inventory.LoadInventory();

            var productIdPurchased = 1;
            var quantityPurchased = 21;

            var maximumItemAllowed = 25;

            // Act 
            inventory.ValidatePurchase(productIdPurchased, quantityPurchased, maximumItemAllowed);

            // Assert
            // This Assert should throw an exception.
        }
    }
}
