# Simple Vending Machine

This is a simple Vending Machine implementation which accepts a Cash Card.

Vending machine:
- 
* Vending Machine has an initial inventory of 25 cans
* Vending Machine cannot vend more than 25 cans  
* Cannot Vend if Cash Card PIN supplied is invalid
* Cannot Vend if the account balance is less than 50p on the card
  * _ASSUMPTION_: As the CAN cost is 50p, rather than hard coding 50p, the amount should be checked for the price of the product. If the account doesnot have minimum balance then vending should be aborted.

Accounts
-
* There are two CashCards Linked to a Joint Account
  * _ASSUMPTION_: Though only two cash card is mentioned, but it is assumed that an account can have multiple cash cards.
* As multiple cash card is linked ot one account, account may be accessed by multiple requests. 

# Project Structure
Project is structured as:
1. **ConsoleRunner**: A  command line routine which runs the Vending machine program as a sequence of pre-defined steps.
1. **Entities**: This contains all Domain objects, and processing logic.
   * This project can be nicely abstracted further into Entities (or Model) / Business Objects / Workflows etc.
1. **Tests**: This project includes all the tests for the application.
   * InventoryTests
   * VendingMachineTests
   
# Design Decisions
* Implemented Interfaced based design so that testing can be easier.
* As Vending machine can be different types, it was decided to have a generic implementation which can allow individuals to write different implementation based on Payment Method and Account Type

# Improvements
* Add Unity Container as DI container
* Better modelling of Account / Cash Card
* Instead of using Console.WriteLine() for Logs, use common Logging framework's.

