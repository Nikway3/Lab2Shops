using System;

namespace Shops.Exceptions
{
    public class AmountException : ShopsException
    {
        public AmountException()
        {
        }

        public AmountException(string message)
            : base(message)
        {
        }

        public AmountException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}