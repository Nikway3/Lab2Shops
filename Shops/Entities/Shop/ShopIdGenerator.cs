namespace Shops.Entities.Shop
{
    public static class ShopIdGenerator
    {
        private static int _id;
        public static int GenerateNewShopId()
        {
            return ++_id;
        }
    }
}