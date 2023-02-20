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
            newProduct.SetAmount(30);
            if (twixPrice * newProduct.GetAmount() <= thisShopBalance)
            {
                thisShopBalance = thisShopBalance - (twixPrice * newProduct.GetAmount());
                _management.AddProductToTheShop(newShop, newProduct);
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
            float shopsBalance = 10000;
            newShop1 = _management.AddShop("Lenta", "BlackRiver", shopsBalance);
            newShop2 = _management.AddShop("Real", "Koroleva", shopsBalance);
            var newProduct1 = new Product("MeatChops", 200);
            var newProduct2 = new Product("MeatChops", 280);
            var newProduct3 = new Product("FreshMix", 210);
            var newProduct4 = new Product("FreshMix", 200);
            var newProductList1 = new List<Product>();
            var newProductList2 = new List<Product>();
            newProductList1.Add(newProduct1);
            newProductList1.Add(newProduct3);
            newProductList2.Add(newProduct2);
            newProductList2.Add(newProduct4);
            _management.AddProductToTheShop(newShop1, newProduct1);
            _management.AddProductToTheShop(newShop2, newProduct2);
            _management.AddProductToTheShop(newShop1, newProduct3);
            _management.AddProductToTheShop(newShop2, newProduct4);
            shopWithLeastPrices = _management.FindProductListWithLessPrice(newProductList1, newShop1, newProductList2, newShop2);
            Assert.Equal(shopWithLeastPrices, newShop1);
        }
    }
}