using System;

namespace Shops.Exceptions
{
    public class AddressException : ShopsException
    {
        public AddressException()
        {
        }

        public AddressException(string message)
            : base(message)
        {
        }

        public AddressException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}