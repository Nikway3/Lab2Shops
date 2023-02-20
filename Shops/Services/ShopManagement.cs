using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shops.Entities.Customer;
using Shops.Entities.Product;
using Shops.Entities.Shop;
using Shops.Exceptions;

namespace Shops.Services
{
    public class ShopManagement : IShopManagement
    {
        private readonly List<Shop> _database = new List<Shop>();
        private readonly List<Product> _listOfProducts = new List<Product>();
        public Shop AddShop(string name, string address, float shopBalance)
        {
            var newShop = new Shop(name, address, shopBalance);
            _database.Add(newShop);
            return newShop;
        }

        public void CustomerMakePurchase(Customer customer, Shop shop, Product product, int amount)
        {
            if (!IsShopInDatabase(shop))
            {
                throw new ShopsException("No such shop in database.");
            }

            if (shop.IsInCatalog(product))
            {
                throw new ProductExistenceException("No such product in catalog.");
            }

            customer.MakePurchase(product, amount);
            new PurchaseHandler().SetNewAmount(product, amount);
        }

        public void AddToProductList(Product product)
        {
            _listOfProducts.Add(product);
        }

        public void AddProductToTheShop(Shop shop, Product product)
        {
            float shopBalance;
            if (!IsShopInDatabase(shop))
            {
                throw new ShopsException("No such shop in database.");
            }

            shopBalance = shop.GetShopBalance() - (product.GetAmount() * product.GetPrice());
            if (shop.GetShopBalance() < product.GetAmount() * product.GetPrice())
            {
                throw new BalanceException("Your balance is too low");
            }

            shop.SetShopBalance(shopBalance);
            _database[_database.IndexOf(shop)].AddProduct(product);
        }

        public void DeliverProducts(Shop shop, ReadOnlyCollection<Product> productList)
        {
            if (!IsShopInDatabase(shop))
            {
                throw new ShopsException("No such shop in database.");
            }

            foreach (Product newProduct in productList)
            {
                if (shop.IsInCatalog(newProduct))
                {
                    int newAmount = newProduct.GetAmount() + shop.FindProduct(newProduct).GetAmount();
                    shop.FindProduct(newProduct).SetAmount(newAmount);
                }
                else
                {
                    GetShopFromDatabase(shop).AddProduct(newProduct);
                }
            }
        }

        /*public Shop ShopWithLeastPrice(Shop shop1, Shop shop2)
        {
        }*/

        public Product FindProductWithMinPrice()
        {
            float min = float.MaxValue;
            Product? foundProduct = null;
            foreach (Product product in from shop in _database from product in shop.GetCatalog() where product.GetPrice() < min select product)
            {
                foundProduct = product;
                min = product.GetPrice();
            }

            if (foundProduct == null)
            {
                throw new Exception();
            }

            return foundProduct;
        }

        public Customer CreateNewCustomer(string name, int balance)
        {
            return new Customer(name, balance);
        }

        public Product ChangePrice(Product product, float newPrice)
        {
            return new Product(product.GetName(), newPrice);
        }

        public Shop FindProductListWithLessPrice(List<Product> newList, Shop newShop1, List<Product> newList2, Shop newShop2)
        {
            if (newList == null || newList2 == null)
            {
                throw new ProductExistenceException("There is no one product in the list");
            }

            float firstListPrice = GetAllPriceFromTheList(newList);
            float secondListPrice = GetAllPriceFromTheList(newList2);
            if (firstListPrice >= secondListPrice)
            {
                return newShop2;
            }

            return newShop1;
        }

        private bool IsShopInDatabase(Shop shop)
        {
            return _database.Contains(shop);
        }

        private ReadOnlyCollection<Product> GetShopCatalogFromDatabase(Shop shop)
        {
            return GetShopFromDatabase(shop).GetCatalog();
        }

        private Shop GetShopFromDatabase(Shop shop)
        {
            if (!IsShopInDatabase(shop))
            {
                throw new ShopsException("No such shop in database.");
            }

            return _database[_database.IndexOf(shop)];
        }

        private float GetAllPriceFromTheList(List<Product> newList)
        {
            float allPrice = 0;
            foreach (Product product in newList)
            {
                allPrice = allPrice + product.GetPrice();
            }

            return allPrice;
        }
    }
}