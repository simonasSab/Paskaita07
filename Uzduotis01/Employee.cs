namespace Uzduotis01
{
    internal class Employee
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long ID { get; set; }
        public DateOnly WorkingSince { get; set; }

        public Employee(string name, string surname, long id)
        {
            Name = name;
            Surname = surname;
            ID = id;
            WorkingSince = DateOnly.FromDateTime(DateTime.Now);
        }

        public Employee(string name, string surname, long id, DateOnly workingSince)
        {
            Name = name;
            Surname = surname;
            ID = id;
            WorkingSince = workingSince;
        }

        public override string ToString()
        {
            return $"{Name} {Surname} {ID} (Since {WorkingSince})";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            Employee employee = (Employee)obj;

            if (employee.ID == this.ID)
                return true;
            else
                return false;
        }
    }
}
