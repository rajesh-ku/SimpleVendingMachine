using System;
using System.Runtime.Serialization;

namespace SimpleVendingMachine.Entities.Exceptions
{
    public class InventoryException : Exception
    {
        public InventoryException()
        {
        }

        public InventoryException(string message) : base(message)
        {
        }

        public InventoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InventoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
