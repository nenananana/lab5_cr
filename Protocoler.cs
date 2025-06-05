namespace CSLab5
{
    class Protocoler
    {
        private readonly string _file;
        private StreamWriter _streamWriter;

        public Protocoler(string file = "Protocol.txt")
        {
            if (!File.Exists(file))
                throw new Exception("Файла с заданным путем не существет!");

            if (!file.EndsWith(".txt"))
                throw new Exception("Тип файла должен быть txt!");

            _file = file;
            _streamWriter = new StreamWriter(file, true);
        }

        public void WriteLine(string s) => _streamWriter.WriteLine($"{DateTime.Now} - {s}");

        public void Save()
        {
            _streamWriter.Close();
            _streamWriter = new StreamWriter(_file, true);
        }

        public void Close() => _streamWriter.Close();
    }
}