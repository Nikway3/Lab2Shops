using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shops.Entities.Product;
using Shops.Exceptions;

namespace Shops.Entities.Customer
{
    public class Customer
    {
        private readonly List<Product.Product> _customerCart = new List<Product.Product>();
        private readonly string _name;
        private float _balance;
        public Customer(string name, float balance)
        {
            _balance = balance;
            _name = name;
        }

        public float GetBalance() { return _balance; }

        public string GetName() { return _name; }

        public ReadOnlyCollection<Product.Product> GetCustomerCart()
        {
            return _customerCart.AsReadOnly();
        }

        public void MakePurchase(Product.Product product, int amount)
        {
            if (product.GetPrice() * amount > _balance)
            {
                throw new BalanceException("Not enough money on customer's balance");
            }

            if (product.GetAmount() < amount)
            {
                throw new AmountException("There is no such products in the shop");
            }

            _balance -= product.GetPrice();
            _customerCart.Add(new PurchaseHandler().GetProductToBuy(product, amount));
        }
    }
}