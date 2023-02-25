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

        public void AddProductToTheShop(Shop shop, Product product, int amount)
        {
            product.SetAmount(amount);
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

        public Shop FindShop(List<Product> productsList)
        {
            var shopWithMinPrice = new Shop();
            float sumPrice = 0;
            float minPrice = float.MaxValue;
            foreach (var shop in _database)
            {
                sumPrice = 0;
                foreach (var product in productsList)
                {
                    if (shop.CheckAmountIsInCatalog(product))
                    {
                        sumPrice += product.GetAmount() * shop.GetPriceFromCatalog(product);
                    }
                    else
                    {
                        break;
                    }
                }

                if (sumPrice < minPrice)
                {
                    minPrice = sumPrice;
                    shopWithMinPrice = shop;
                }
            }

            return shopWithMinPrice;
        }

        public void BuyingSomeGoods(Dictionary<Product, int> newCustomerList, Customer newCustomer, Shop newShop)
        {
            foreach (Product product in newCustomerList.Keys)
            {
                if (!newShop.IsInCatalog(product))
                {
                    throw new ProductExistenceException("This product doesn't delivered to this shop");
                }
            }

            if (newCustomer.GetBalance() < GetAllPriceFromTheList(newCustomerList.Keys))
            {
                throw new BalanceException("Balance is too low to buy this products");
            }

            float newBalance = newCustomer.GetBalance() - GetAllPriceFromTheList(newCustomerList.Keys);
            newCustomer.SetBalance(newBalance);
            foreach (var product in newCustomerList)
            {
                int newAmount = product.Key.GetAmount() - product.Value;
                product.Key.SetAmount(newAmount);
            }
        }

        public Customer CreateNewCustomer(string name, int balance)
        {
            return new Customer(name, balance);
        }

        public Product ChangePrice(Product product, float newPrice)
        {
            return new Product(product.GetName(), newPrice);
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

        private float GetAllPriceFromTheList(Dictionary<Product, int>.KeyCollection newList)
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