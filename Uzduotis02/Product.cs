namespace Uzduotis02
{
    internal class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, double price, int quantity)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void ModifyObject(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{ID:00000} {Name}: {Price:.00} Eur, {Quantity} remaining";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            Product product = (Product)obj;

            if (product.ID == this.ID)
                return true;
            else
                return false;
        }
    }
}
