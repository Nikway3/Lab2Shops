using System.Collections.ObjectModel;
using Shops.Entities.Customer;
using Shops.Entities.Product;
using Shops.Entities.Shop;

namespace Shops.Services
{
    public interface IShopManagement
    {
        Shop AddShop(string name, string address, float shopBalance);
        Customer CreateNewCustomer(string name, int balance);
        void CustomerMakePurchase(Customer customer, Shop shop, Product product, int amount);
        void AddProductToTheShop(Shop shop, Product product, int amount);
        Shop FindShop(List<Product> productsList);
    }
}