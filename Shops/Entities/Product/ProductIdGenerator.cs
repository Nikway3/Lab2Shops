using System;

namespace Shops.Entities.Product
{
    public static class ProductIdGenerator
    {
        private static int _id = 1000;
        public static int GenerateNewProductId()
        {
            return ++_id;
        }
    }
}