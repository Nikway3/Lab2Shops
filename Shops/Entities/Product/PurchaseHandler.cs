namespace Shops.Entities.Product
{
    public class PurchaseHandler
    {
        public Product GetProductToBuy(Product product, int amount)
        {
            var productToBuy = new Product(product.GetName(), product.GetPrice());
            productToBuy.SetAmount(amount);
            return productToBuy;
        }

        public void SetNewAmount(Product product, int amount)
        {
            product.SetAmount(product.GetAmount() - amount);
        }
    }
}