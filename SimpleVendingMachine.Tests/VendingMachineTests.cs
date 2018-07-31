using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleVendingMachine.Entities;
using SimpleVendingMachine.Entities.Exceptions;
using SimpleVendingMachine.Entities.Impl;
using SimpleVendingMachine.Entities.Interfaces;
using System.Linq;

namespace SimpleVendingMachine.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void UsingInvalidPinForCashCard_ReturnsFalse()
        {
            // Set Expectations
            var expectedResult = false;

            // Arrange
            IInventory inventory = new Inventory();
            inventory.LoadInventory();

            // Using the CashCard Vending Machine
            var vendingMachine = new VendingMachine(inventory);

            // Use a CashCard with wrong Pin
            var cashCard = new CashCard(0);

            // Act
            var actualResult = vendingMachine.ValidatePaymentMethod(cashCard);

            // Assert
            Assert.IsFalse(actualResult);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountException))]
        public void InsufficientBalanceInAccount_ThrowsException()
        {
            // Arrange
            IInventory inventory = new Inventory();
            inventory.LoadInventory();

            // Use a CashCard with wrong Pin
            var cashCard = new CashCard(9191);

            var account = new Account("TEST Account Number");
            account.Deposit(0.5m);

            // Using the CashCard Vending Machine
            var vendingMachine = new VendingMachine(inventory);
            vendingMachine.PurchasedProduct = new Product { ProductId = 1, Cost = 0.5m };
            vendingMachine.PurchasedQuantity = 2;

            // Act
            vendingMachine.AcceptPayment(account);

            // Assert
            // Throws an exception
        }

        [TestMethod]
        public void SufficientBalanceInAccount_CreatesSuccessfulTransaction()
        {
            // Set Expectations
            var expectedStockAfterTransaction = 18;
            var expectedAmountAfterTransaction = 4.0m;

            // Arrange
            IInventory inventory = new Inventory();
            inventory.LoadInventory();

            // Use a CashCard with wrong Pin
            var cashCard = new CashCard(9191);

            var account = new Account("TEST Account Number");
            account.Deposit(5.0m);

            // Using the CashCard Vending Machine
            var vendingMachine = new VendingMachine(inventory);
            vendingMachine.PurchasedProduct = inventory.AllInventories.ElementAt(0).Key;
            vendingMachine.PurchasedQuantity = 2;

            // Act
            vendingMachine.AcceptPayment(account);

            // Assert
            var actualStockAfterTransaction = inventory.AllInventories.ElementAt(0).Value;
            var actualAmountAfterTransaction = account.Balance;

            Assert.AreEqual(expectedStockAfterTransaction, actualStockAfterTransaction);
            Assert.AreEqual(expectedAmountAfterTransaction, actualAmountAfterTransaction);
        }
    }
}
