using System;

using System;

namespace Shops.Exceptions
{
    public class BalanceException : ShopsException
    {
        public BalanceException()
        {
        }

        public BalanceException(string message)
            : base(message)
        {
        }

        public BalanceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}