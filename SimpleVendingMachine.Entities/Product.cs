namespace SimpleVendingMachine.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Cost { get; set; }

        public override string ToString()
        {
            return string.Format("Product| Id: '{0}', Name: '{1}', Cost: '{2}'", ProductId, Name, Cost);
        }
    }
}
