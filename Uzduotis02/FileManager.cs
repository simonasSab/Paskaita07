namespace Uzduotis02
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

        public void WriteObjectToFile(Product? product)
        {
            if (product != null)
            {
                OpenStreamWriterToFile();
                _streamWriter.WriteLine($"{product.ID},{product.Name},{product.Price},{product.Quantity}");
                _streamWriter.Close();
            }
        }

        public void WriteObjectToFile(int variable)
        {
            OpenStreamWriterToFile();
            _streamWriter.WriteLine(variable);
            _streamWriter.Close();
        }

        public void WriteObjectToFile(List<Product> catalog)
        {
            OpenStreamWriterToFile();

            foreach (Product product in catalog)
            {
                _streamWriter.WriteLine($"{product.ID},{product.Name},{product.Price},{product.Quantity}");
            }
            _streamWriter.Close();
        }

        public List<Product> ReadObjectList()
        {
            List<Product> catalog = new();

            _streamReader = new StreamReader(_filePath);
            string line;
            while ((line = _streamReader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                catalog.Add(new Product(int.Parse(values[0]), values[1], double.Parse(values[2]), int.Parse(values[3])));
            }
            _streamReader.Close();
            return catalog;
        }

        public int ReadInt()
        {
            int number;
            _streamReader = new StreamReader(_filePath);
            string line;
            if ((line = _streamReader.ReadLine()) != null && int.TryParse(line, out number))
            {
                _streamReader.Close();
                return number;
            }
            else
            {
                _streamReader.Close();
                return 0;
            }
        }
    }
}
