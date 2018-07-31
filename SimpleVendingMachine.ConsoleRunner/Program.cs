using SimpleVendingMachine.Entities;
using SimpleVendingMachine.Entities.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleVendingMachine.ConsoleRunner
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Any unhandled exceptions MUST be caust by this exception handler
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Initialise inventory before starting the Vending Machine
            var inventory = new Inventory();
            inventory.LoadInventory();

            // Start the vending machine object by passing in the inventory
            var vendingMachine = new VendingMachine(inventory);

            vendingMachine.DisplayInventory();

            vendingMachine.SelectItemToPurchase();

            vendingMachine.SelectQuantityToPurchase();

            var cashCard = new CashCard(9191);
            vendingMachine.ValidatePaymentMethod(cashCard);

            var account = new Account("My Account");
            account.Deposit(2.5m);

            vendingMachine.AcceptPayment(account);

            Exit(0);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            //LogException("An exception has been caught in WorkflowRunner.", ex);
            Exit(-1);
        }

        private static void Exit(int exitCode, string message = "")
        {
            if(!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }

            // If the debugger is attached, make sure to keep the console active to review the log messages
            if(Debugger.IsAttached)
            {
                Console.WriteLine();
                Console.WriteLine("Press <Enter> to exit ...");
                Console.ReadLine();
            }
            Environment.Exit(exitCode);
        }
    }
}
