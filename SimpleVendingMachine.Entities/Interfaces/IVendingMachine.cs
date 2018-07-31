namespace SimpleVendingMachine.Entities.Interfaces
{
    public interface IVendingMachine<T, K> 
    {
        void DisplayInventory();

        int SelectItemToPurchase();

        int SelectQuantityToPurchase();

        bool ValidatePaymentMethod(T paymentMethod);

        void AcceptPayment(K accountOrCash);
    }
}
