namespace Uzduotis02
{
    // Užduotis 2: Produktų katalogo valdymo sistema
    // Sukurkite programą, kuri valdo produktų katalogą. Programa leis atlikti šias operacijas:
    // Pridėti naują produktą.
    // Rodyti visus produktus.
    // Ieškoti produkto pagal pavadinimą.
    // Atnaujinti produkto informaciją.
    // Ištrinti produktą.
    // Išsaugoti ir nuskaityti produktų duomenis iš failo.

    internal class Program
    {
        private static FileManager fileManager = new("catalog0.txt");
        private static FileManager fileManager2 = new("productID.txt");
        private static Store store = new();
        private static int genericNameTag = 0;
        private static int productID = 0;

        public static void Main(string[] args)
        {
            // Loading file to list at startup
            try
            {
                store.Catalog = fileManager.ReadObjectList();
            }
            catch (IOException)
            {
                // If file does not exist, create empty file
                fileManager.WriteObjectToFile(store.Catalog);
            }
            Console.WriteLine($"Hello! Loaded {store.Catalog.Count} items.\n");

            // Loading productID at startup
            try
            {
                productID = fileManager2.ReadInt();
            }
            catch (IOException)
            {
                // If file does not exist, create empty file
                fileManager2.WriteObjectToFile(productID);
            }

            MainMenu();

            // Saving list on exit
            fileManager.WriteObjectToFile(store.Catalog);
            fileManager2.WriteObjectToFile(productID);
        }

        private static void MainMenu()
        {
            int selection;
            bool isInt;
            do
            {
                Console.WriteLine("1.Create exclusive product and add to catalog" +
                                "\n2.Create generic product and add to catalog" +
                                "\n3.Display all products" +
                                "\n4.Find product by name" +
                                "\n5.Modify product" +
                                "\n6.Remove product" +
                                "\n7.Save current catalog" +
                                "\n8.Load catalog from file" +
                                "\n           0. Save and quit.\n");
                isInt = int.TryParse(Console.ReadLine(), out selection);
                Console.WriteLine();
            }
            while (!isInt || selection < 0 || selection > 8);

            switch (selection)
            {
                case 0:
                    Console.WriteLine($"Saving and quitting.");
                    return;
                case 1: // Create exclusive product and add to catalog
                    store.AddProduct(NewItem());
                    break;
                case 2: // Create generic product and add to catalog
                    store.AddProduct(NewItem(true));
                    break;
                case 3: // Display all products
                    store.DisplayAllProducts();
                    break;
                case 4: // Find product by name
                    store.FindProductsByName(GetName());
                    break;
                case 5: // Modify product
                    if (store.DisplayAllProducts())
                        store.ModifyElement(ModifyItem(SelectID()));
                    break;
                case 6: // Remove product
                    if (store.DisplayAllProducts())
                        store.RemoveProduct(SelectID());
                    break;
                case 7: // Save current catalog
                    SaveToFile();
                    break;
                case 8: // Reload last saved catalog
                    LoadFromFile();
                    break;
                default:
                    Console.WriteLine($"Unexpected error - program is quitting.");
                    return;
            }
            MainMenu();
        }

        private static void SaveToFile()
        {
            int selection;
            bool isInt;
            do
            {
                Console.WriteLine("Select file to save to");
                Console.WriteLine($"1.catalog1{EmptySaveText("catalog1.txt")}" +
                                $"\n2.catalog2{EmptySaveText("catalog2.txt")}" +
                                $"\n3.catalog3{EmptySaveText("catalog3.txt")}" +
                                "\n           0. Cancel.\n");
                isInt = int.TryParse(Console.ReadLine(), out selection);
                Console.WriteLine();
            }
            while (!isInt || selection < 0 || selection > 3);

            switch (selection)
            {
                case 0:
                    break;
                case 1:
                    FileManager fileManager1 = new("catalog1.txt");
                    fileManager.WriteObjectToFile(store.Catalog);
                    break;
                case 2:
                    FileManager fileManager2 = new("catalog2.txt");
                    fileManager.WriteObjectToFile(store.Catalog);
                    break;
                case 3:
                    FileManager fileManager3 = new("catalog3.txt");
                    fileManager.WriteObjectToFile(store.Catalog);
                    break;
                default:
                    Console.WriteLine($"Unexpected error.\n");
                    break;
            }
        }

        private static void LoadFromFile()
        {
            int selection;
            bool isInt;
            do
            {
                Console.WriteLine("Select a file to load from");
                Console.WriteLine($"1.catalog1{EmptySaveText("catalog1.txt")}" +
                                $"\n2.catalog2{EmptySaveText("catalog2.txt")}" +
                                $"\n3.catalog3{EmptySaveText("catalog3.txt")}" +
                                "\n           0. Cancel.\n");
                isInt = int.TryParse(Console.ReadLine(), out selection);
                Console.WriteLine();
            }
            while (!isInt || selection < 0 || selection > 3);

            switch (selection)
            {
                case 0:
                    break;
                case 1:
                    if (File.Exists("catalog1.txt"))
                    {
                        FileManager fileManager1 = new("catalog1.txt");
                        store.Catalog = fileManager1.ReadObjectList();
                        Console.WriteLine($"File catalog1.txt loaded successfully\n");
                    }
                    else
                    {
                        Console.WriteLine($"File catalog1.txt does not exist.\n");
                    }
                    break;
                case 2:
                    if (File.Exists("catalog2.txt"))
                    {
                        FileManager fileManager1 = new("catalog2.txt");
                        store.Catalog = fileManager1.ReadObjectList();
                        Console.WriteLine($"File catalog2.txt loaded successfully\n");
                    }
                    else
                    {
                        Console.WriteLine($"File catalog2.txt does not exist.\n");
                    }
                    break;
                case 3:
                    if (File.Exists("catalog3.txt"))
                    {
                        FileManager fileManager1 = new("catalog3.txt");
                        store.Catalog = fileManager1.ReadObjectList();
                        Console.WriteLine($"File catalog3.txt loaded successfully\n");
                    }
                    else
                    {
                        Console.WriteLine($"File catalog3.txt does not exist.\n");
                    }
                    break;
                default:
                    Console.WriteLine($"Unexpected error.\n");
                    break;
            }
        }

        public static string EmptySaveText(string filePath)
        {
            if (File.Exists(filePath))
                return "";
            else
                return " - empty";
        }

        public static Product? NewItem()
        {
            // Get name
            string? name;
            do
            {
                Console.WriteLine("Product name: \n(Cancel: -1)\n");
                name = Console.ReadLine();
                if (name == "-1")
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (name == null || name.Trim() == "");

            // Get price
            double price;
            bool varCheck;
            do
            {
                Console.WriteLine("Product price: \n(Cancel: -1)\n");
                varCheck = double.TryParse(Console.ReadLine(), out price);
                if (price == -1)
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (!varCheck || price < 0.01);

            // Get quantity
            int quantity;
            do
            {
                Console.WriteLine("How many items? \n(Cancel: -1)\n");
                varCheck = int.TryParse(Console.ReadLine(), out quantity);
                if (quantity == -1)
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (!varCheck || quantity < 1);
            Console.WriteLine();

            // Create product and return it
            Product? product = new(productID, name, price, quantity);

            return product;
        }

        public static Product? NewItem(bool isGeneric)
        {
            Random random = new();

            // Get name
            string name = $"Generic{genericNameTag:00000}";

            // Get price
            double price = random.NextDouble() * 100 + random.NextDouble();

            // Get quantity
            int quantity = random.Next(1, 1000);


            // Create product and return it
            Product? product = new(productID, name, price, quantity);

            return product;
        }

        public static Product? ModifyItem(int id)
        {
            // Get name
            string? name;
            do
            {
                Console.WriteLine("Product name: \n(Cancel: -1)\n");
                name = Console.ReadLine();
                if (name == "-1")
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (String.IsNullOrEmpty(name));

            // Get price
            double price;
            bool varCheck;
            do
            {
                Console.WriteLine("Product price: \n(Cancel: -1)\n");
                varCheck = double.TryParse(Console.ReadLine(), out price);
                if (price == -1)
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (!varCheck || price < 0.01);

            // Get quantity
            int quantity;
            do
            {
                Console.WriteLine("How many items left? \n(Cancel: -1)\n");
                varCheck = int.TryParse(Console.ReadLine(), out quantity);
                if (quantity == -1)
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (!varCheck || quantity < 1);
            Console.WriteLine();

            // Create product and return it
            Product? product = new(id, name, price, quantity);

            return product;
        }

        public static int SelectID()
        {
            int id;
            bool isInt;
            do
            {
                Console.WriteLine("Enter product ID number. \n(Cancel: -1)\n");
                isInt = int.TryParse(Console.ReadLine(), out id);
                if (id == -1)
                {
                    Console.WriteLine("\nCancelled\n");
                    return id;
                }
            }
            while (!isInt || !store.IDExists(id));

            Console.WriteLine();

            return id;
        }

        public static string GetName()
        {
            if (store.Catalog.Count < 1)
            {
                Console.WriteLine($"Catalog is empty.\n");
                return "-1";
            }

            string? name;
            do
            {
                Console.WriteLine("Product name: \n(Cancel: -1)\n");
                name = Console.ReadLine();
                if (name == "-1")
                {
                    Console.WriteLine("\nCancelled\n");
                    return name;
                }
                Console.WriteLine();
            }
            while (name == null || name.Trim() == "");

            return name;
        }
    }
}
