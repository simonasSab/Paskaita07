namespace Uzduotis01
{
    // Užduotis 1: Sukurkite programą, kuri valdo darbuotojų duomenis.
    // Programa leis pridėti darbuotojus, rodyti visus darbuotojus ir išsaugoti bei nuskaityti darbuotojų duomenis iš failo.
    // * Pridėti darbuotoją į sąrašą
    // * Pašalinti darbuotoją iš sąrašo
    // * Parodyti visus darbuotojus

    internal class Program
    {
        private static Company company = new();
        private static FileManager fileManager = new("staff.txt");
        private static int johnSmithNumber = 0;

        public static void Main(string[] args)
        {
            // Loading file to Staff list at startup
            try
            {
                company.Staff = fileManager.ReadStaffList();
            }
            catch (IOException e)
            {
                // If file does not exist, create empty file
                fileManager.WriteStaffToFile(company.Staff);
            }
            Console.WriteLine($"Hello! Loaded {company.Staff.Count} employees.\n");

            MainMenu();

            // Writing Staff list on exit
            fileManager.WriteStaffToFile(company.Staff);
        }

        private static void MainMenu()
        {
            int selection;
            bool isInt;
            do
            {
                Console.WriteLine("1. Hire employee" +
                                "\n2. Hire robot" +
                                "\n3. Fire employee" +
                                "\n4. Display all employees" +
                                "\n\n0. Save and quit.\n");
                isInt = int.TryParse(Console.ReadLine(), out selection);
                Console.WriteLine();
            }
            while (!isInt || selection < 0 || selection > 4);

            switch (selection)
            {
                case 0:
                    Console.WriteLine($"Saving employees and quitting.");
                    return;
                case 1: // Hire employee
                    company.HireEmployee(NewEmployee());
                    break;
                case 2: // Hire robot
                    company.HireEmployee(NewEmployee(true));
                    break;
                case 3: // Fire employee
                    company.DisplayAllEmployees();
                    company.FireEmployee(SelectEmployeeID());
                    break;
                case 4: // Display all employees 
                    company.DisplayAllEmployees();
                    break;
                default:
                    Console.WriteLine($"Unexpected error - the program is terminating.");
                    return;
            }
            MainMenu();
        }

        public static Employee? NewEmployee()
        {
            // Get name
            string? name;
            do
            {
                Console.WriteLine("What is your name? " +
                    "(Cancel: -1)\n");
                name = Console.ReadLine();
                if (name == "-1")
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (name == null);

            // Get surname
            string? surname;
            do
            {
                Console.WriteLine("What is your surname? " +
                    "(Cancel: -1)\n");
                surname = Console.ReadLine();
                if (surname == "-1")
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (surname == null);

            // Get ID
            long id;
            bool isLong;
            do
            {
                Console.WriteLine("What is your personal 11-digit ID number? " +
                    "(Cancel: -1)\n");
                isLong = long.TryParse(Console.ReadLine(), out id);
                if (id == -1)
                {
                    Console.WriteLine("\nCancelled\n");
                    return null;
                }
                Console.WriteLine();
            }
            while (!isLong || id < 10000000000 || id > 100000000000);
            Console.WriteLine();

            // Create employee and return it
            Employee employee = new(name, surname, id);

            return employee;
        }

        public static Employee NewEmployee(bool randomJohnSmith)
        {
            // Get ID
            Random random = new();
            long id = random.NextInt64(10000000000, 100000000000);

            // Create employee and return it
            Employee employee = new($"John_{johnSmithNumber:000}", "Smith", id);
            johnSmithNumber++;

            return employee;
        }

        public static long SelectEmployeeID()
        {
            long id;
            bool isLong;
            do
            {
                Console.WriteLine("Enter employee personal ID (11 digits, e.g., \"39501011234\") " +
                    "(Cancel: -1)\n");
                isLong = long.TryParse(Console.ReadLine(), out id);
                if (id == -1)
                {
                    Console.WriteLine("\nCancelled\n");
                    return id;
                }
            }
            while (!isLong || id < 10000000000 || id > 100000000000 || !company.IDExists(id));

            Console.WriteLine();

            return id;
        }
    }
}
