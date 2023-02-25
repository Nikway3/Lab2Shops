using Shops.Entities.Product;
using Shops.Entities.Shop;
using Shops.Services;
using Xunit;

namespace Shops.Test
{
    public class ShopsTest
    {
        private ShopManagement _management = new ShopManagement();

        [Fact]
        public void DeliveryClub()
        {
            float thisShopBalance = 20000;
            const float twixPrice = 40;
            var newShop = new Shop("Lenta", "Komendantski squere", thisShopBalance);
            newShop = _management.AddShop("Lenta", "Komendantski squere", thisShopBalance);
            var newProduct = new Product("Twix", twixPrice);
            if (twixPrice * newProduct.GetAmount() <= thisShopBalance)
            {
                thisShopBalance = thisShopBalance - (twixPrice * newProduct.GetAmount());
                _management.AddProductToTheShop(newShop, newProduct, 30);
            }

            Assert.Equal(newShop.GetShopBalance(), thisShopBalance);
        }

        [Fact]
        public void TryToChangePrice()
        {
            var newProduct = new Product("Cake", 400);
            float newPrice = 200;
            newProduct = _management.ChangePrice(newProduct, newPrice);
            Assert.Equal(newProduct.GetPrice(), newPrice);
        }

        [Fact]
        public void FindLessPriceProduct()
        {
            Shop shopWithLeastPrices, newShop1, newShop2;
            float shopsBalance = 100000;
            newShop1 = _management.AddShop("Lenta", "BlackRiver", shopsBalance);
            newShop2 = _management.AddShop("Real", "Koroleva", shopsBalance);
            var newProduct1 = new Product("MeatChops", 200);
            var newProduct2 = new Product("MeatChops", 280);
            var newProduct3 = new Product("FreshMix", 210);
            var newProduct4 = new Product("FreshMix", 200);
            _management.AddProductToTheShop(newShop1, newProduct1, 10);
            _management.AddProductToTheShop(newShop2, newProduct2, 10);
            _management.AddProductToTheShop(newShop1, newProduct3, 10);
            _management.AddProductToTheShop(newShop2, newProduct4, 10);
            var productFind1 = new Product("MeatChops");
            var productFind2 = new Product("FreshMix");
            var mixProducts = new List<Product>();
            mixProducts.Add(productFind1);
            mixProducts.Add(productFind2);
            shopWithLeastPrices = _management.FindShop(mixProducts);
            Assert.Equal(shopWithLeastPrices, newShop1);
        }

        [Fact]
        public void BuyingGroupOfProducts()
        {
            var newCustomer = _management.CreateNewCustomer("Dani", 3000);
            Shop newShop = _management.AddShop("Lenta", "Kronversci", 40000);
            var product1 = new Product("Cerials", 150);
            var product2 = new Product("Milk", 200);
            _management.AddProductToTheShop(newShop, product1, 15);
            _management.AddProductToTheShop(newShop, product2, 10);
            var mixProducts = new Dictionary<Product, int>();
            mixProducts.Add(product1, 3);
            mixProducts.Add(product2, 4);
            _management.BuyingSomeGoods(mixProducts, newCustomer, newShop);
            Assert.Equal(12, product1.GetAmount());
            Assert.Equal(6, product2.GetAmount());
        }
    }
}