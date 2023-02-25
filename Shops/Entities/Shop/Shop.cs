using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shops.Entities.Product;
using Shops.Entities.Shop;
using Shops.Exceptions;

namespace Shops.Entities.Shop
{
    public class Shop
    {
        private readonly List<Product.Product> _catalog = new List<Product.Product>();
        private readonly string _name;
        private readonly int _id;
        private readonly string _address;
        private float _shopsBalance;

        public Shop()
        {
            _name = " ";
            _address = " ";
        }

        public Shop(string name, string address, float shopsBalance)
        {
            _name = name;
            _address = address;
            _shopsBalance = shopsBalance;
            _id = ShopIdGenerator.GenerateNewShopId();
        }

        public float GetShopBalance() { return _shopsBalance; }
        public void SetShopBalance(float shopsBalance) { _shopsBalance = shopsBalance; }

        public string GetName() { return _name; }

        public int GetId() { return _id; }

        public string GetAddress() { return _address; }

        public ReadOnlyCollection<Product.Product> GetCatalog() { return _catalog.AsReadOnly(); }

        public void AddProduct(Product.Product newProduct)
        {
            if (IsInCatalog(newProduct))
            {
                int newAmount = _catalog[_catalog.IndexOf(newProduct)].GetAmount() + newProduct.GetAmount();
                _catalog[_catalog.IndexOf(newProduct)].SetAmount(newAmount);
                return;
            }

            _catalog.Add(newProduct);
        }

        public bool IsInCatalog(Product.Product product)
        {
            return _catalog.Any(prod => prod.GetId() == product.GetId());
        }

        public bool CheckAmountIsInCatalog(Product.Product product)
        {
            Product.Product? prod = _catalog.Find((prod) => prod.GetName() == product.GetName());

            if (prod != null)
            {
                if (prod.GetAmount() <= product.GetAmount()) return true;
                else return false;
            }

            return false;
        }

        public float GetPriceFromCatalog(Product.Product product)
        {
            Product.Product? prod = _catalog.Find((prod) => prod.GetName() == product.GetName());
            if (prod != null) return prod.GetPrice();
            throw new ProductExistenceException("Product Doesn't Exist");
        }

        public Product.Product FindProduct(Product.Product product)
        {
            return (_catalog.IndexOf(product) == -1 ? null : _catalog[_catalog.IndexOf(product)]) ?? throw new ProductExistenceException();
        }

        private bool Equals(Shop other)
        {
            return Equals(_catalog, other._catalog) && _name == other._name && _id == other._id &&
                   _address == other._address;
        }
    }
}