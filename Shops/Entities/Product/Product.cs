namespace Shops.Entities.Product
{
    public class Product
    {
        private int _id;
        private float _price;
        private int _amount;
        private string _name;

        public Product(string name, float price)
        {
            _name = name;
            _price = price;
            _id = ProductIdGenerator.GenerateNewProductId();
        }

        public int GetId() { return _id; }

        public float GetPrice() { return _price; }

        public string GetName() { return _name; }

        public void SetAmount(int amount) { _amount = amount; }

        public int GetAmount() { return _amount; }
    }
}