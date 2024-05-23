namespace Uzduotis01
{
    internal class FileManager
    {
        private StreamWriter? _streamWriter;
        private StreamReader? _streamReader;
        private readonly string _filePath;

        public FileManager(string filePath)
        {
            _filePath = filePath;
        }

        private void OpenStreamWriterToFile()
        {
            _streamWriter = new StreamWriter(_filePath);
        }

        private void OpenStreamWriterToFile(bool append)
        {
            _streamWriter = new StreamWriter(_filePath, append);
        }

        public void WriteEmployeeToFile(Employee? employee)
        {
            if (employee != null)
            {
                OpenStreamWriterToFile();
                if (_streamWriter != null)
                {
                    _streamWriter.WriteLine($"{employee.Name},{employee.Surname},{employee.ID},{employee.WorkingSince}");
                    _streamWriter.Close();
                }
            }
        }

        public void WriteStaffToFile(List<Employee> staff)
        {
            OpenStreamWriterToFile();
            if (_streamWriter != null)
            {
                foreach (Employee employee in staff)
                {
                    _streamWriter.WriteLine($"{employee.Name},{employee.Surname},{employee.ID},{employee.WorkingSince}");
                }
                _streamWriter.Close();
            }
        }

        public List<Employee> ReadStaffList()
        {
            List<Employee> staff = new();

            _streamReader = new StreamReader(_filePath);
            string line;
            while ((line = _streamReader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                staff.Add(new Employee((values[0]), values[1], long.Parse(values[2]), DateOnly.Parse(values[3])));
            }
            _streamReader.Close();
            return staff;
        }
    }
}
