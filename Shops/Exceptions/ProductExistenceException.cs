using System;

namespace Shops.Exceptions
{
    public class ProductExistenceException : ShopsException
    {
        public ProductExistenceException()
        {
        }

        public ProductExistenceException(string message)
            : base(message)
        {
        }

        public ProductExistenceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}