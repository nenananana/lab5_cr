namespace CSLab5
{
    /// <summary>
    /// Класс для записи протокольных сообщений в текстовый файл с отметкой времени.
    /// </summary>
    class Protocoler
    {
        private readonly string _file;
        private StreamWriter _streamWriter;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="Protocoler"/>, открывая файл для записи.
        /// </summary>
        /// <param name="file">Путь к файлу протокола. По умолчанию "Protocol.txt".</param>
        /// <exception cref="Exception">Выбрасывается, если файл не существует или не является .txt файлом.</exception>
        public Protocoler(string file = "Protocol.txt")
        {
            if (!File.Exists(file))
                throw new Exception("Файла с заданным путем не существет!");

            if (!file.EndsWith(".txt"))
                throw new Exception("Тип файла должен быть txt!");

            _file = file;
            _streamWriter = new StreamWriter(file, true);
        }

        /// <summary>
        /// Записывает строку в файл протокола с текущей датой и временем.
        /// </summary>
        /// <param name="s">Сообщение для записи.</param>
        public void WriteLine(string s) => _streamWriter.WriteLine($"{DateTime.Now} - {s}");

        /// <summary>
        /// Закрывает текущий поток записи и открывает его заново для продолжения записи.
        /// </summary>
        public void Save()
        {
            _streamWriter.Close();
            _streamWriter = new StreamWriter(_file, true);
        }

        /// <summary>
        /// Закрывает поток записи в файл.
        /// </summary>
        public void Close() => _streamWriter.Close();
    }
}