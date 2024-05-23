namespace Uzduotis01
{
    internal class Company
    {
        public List<Employee> Staff { get; set; }

        public Company()
        {
            Staff = new();
        }

        public void HireEmployee(Employee? employee)
        {
            if (employee != null && Staff.Contains(employee))
            {
                Console.WriteLine("Staff already has employee with specified ID\n");
                return;
            }

            if (employee != null)
            {
                Staff.Add(employee);
                Console.WriteLine($"Our newest recruit:\n{employee.ToString()}\n");
            }
            else
            {
                Console.WriteLine("Employee was not hired.\n");
            }
        }

        public void FireEmployee(long id)
        {
            if (Staff.Count > 0)
            {
                if (id != -1)
                {
                    Console.WriteLine($"Employee fired: {Staff[GetIndexFromID(id)].ToString()}\n");
                    Staff.RemoveAt(GetIndexFromID(id));
                }
                else
                {
                    Console.WriteLine("Employee was not fired.\n");
                }
            }
            else
            {
                Console.WriteLine("The company is yet to hire its first employee.\n");
            }
        }

        public void DisplayAllEmployees()
        {
            if (Staff.Count > 0)
            {
                foreach (Employee employee in Staff)
                {
                    Console.WriteLine(employee.ToString());
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The company is yet to hire its first employee.\n");
            }
        }

        private int GetIndexFromID(long id)
        {
            if (Staff.Count > 0)
            {
                for (int i = 0; i < Staff.Count; i++)
                {
                    if (Staff[i] != null && Staff[i].ID == id)
                        return i;
                }
                Console.WriteLine("\nError: ID not found.\n");
                return -1;
            }
            else
            {
                Console.WriteLine("The company is yet to hire its first employee.\n");
                return -1;
            }
        }

        public bool IDExists(long id)
        {
            if (Staff.Count > 0)
            {
                foreach (Employee employee in Staff)
                {
                    if (employee.ID == id)
                        return true;
                }
                Console.WriteLine();
                return false;
            }
            else
            {
                Console.WriteLine("The company is yet to hire its first employee.\n");
                return false;
            }
        }
    }
}
