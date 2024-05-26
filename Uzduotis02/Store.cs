namespace Uzduotis02
{
    internal class Store
    {
        public List<Product> Catalog { get; set; }

        public Store()
        {
            Catalog = new();
        }

        public void AddProduct(Product? product)
        {
            if (product != null && Catalog.Contains(product))
            {
                Console.WriteLine("Catalog already has product with specified ID\n");
                return;
            }

            if (product != null)
            {
                Catalog.Add(product);
                Console.WriteLine($"Added product: {product.ToString()}\n");
            }
            else
            {
                Console.WriteLine("Product was not added.\n");
            }
        }

        public void FindProductsByName(string name)
        {
            if (name == "-1")
                return;

            if (Catalog.Count > 0)
            {
                    for (int i = 0; i < Catalog.Count; i++)
                    {
                        if (Catalog[i].Name == name)
                        {
                            Console.WriteLine($"{Catalog[i].ToString()}");
                        }
                    }
                    Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The store is yet to add a product to the catalog.\n");
            }
        }

        public void RemoveProduct(int id)
        {
            if (Catalog.Count > 0)
            {
                if (id != -1)
                {
                    Console.WriteLine($"Removed product: {Catalog[GetIndexFromID(id)].ToString()}\n");
                    Catalog.RemoveAt(GetIndexFromID(id));
                }
                else
                {
                    Console.WriteLine("Product was not removed.\n");
                }
            }
            else
            {
                Console.WriteLine("The store is yet to add a product to the catalog.\n");
            }
        }

        public bool DisplayAllProducts()
        {
            if (Catalog.Count > 0)
            {
                foreach (Product product in Catalog)
                {
                    Console.WriteLine(product.ToString());
                }
                Console.WriteLine();
                return true;
            }
            else
            {
                Console.WriteLine("The store is yet to add a product to the catalog.\n");
                return false;
            }
            return false;
        }

        public void ModifyElement(Product? product)
        {
            if (Catalog.Count > 0)
            {
                if (product != null)
                {
                    Catalog[GetIndexFromID(product.ID)].ModifyObject(product.Name, product.Price, product.Quantity);
                    Console.WriteLine($"Modified product to {product.ToString()}\n");
                }
            }
            else
            {
                Console.WriteLine("The store is yet to add a product to the catalog.\n");
            }
        }

        private int GetIndexFromID(int id)
        {
            if (Catalog.Count > 0)
            {
                for (int i = 0; i < Catalog.Count; i++)
                {
                    if (Catalog[i] != null && Catalog[i].ID == id)
                        return i;
                }
                Console.WriteLine("\nError: product ID not found.\n");
                return -1;
            }
            else
            {
                Console.WriteLine("The store is yet to add a product to the catalog.\n");
                return -1;
            }
        }

        public bool IDExists(int id)
        {
            if (Catalog.Count > 0)
            {
                foreach (Product product in Catalog)
                {
                    if (product.ID == id)
                        return true;
                }
                Console.WriteLine();
                return false;
            }
            else
            {
                Console.WriteLine("The store is yet to add a product to the catalog.\n");
                return false;
            }
        }
    }
}
