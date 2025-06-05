namespace CSLab5
{
    /// <summary>
    /// Представляет художника с идентификатором и именем.
    /// </summary>
    class Artist
    {
        private int _id;
        private string _name;

        /// <summary>
        /// Инициализирует новый экземпляр класса 
        /// <see cref="Artist"/>.
        /// </summary>
        /// <param name="id">
        /// Идентификатор художника.
        /// </param>
        /// <param name="name">
        /// Имя художника.
        /// </param>
        public Artist(int id, string name)
        {
            _id = id;
            _name = name;
        }

        /// <summary>
        /// Получает или задаёт идентификатор художника.
        /// </summary>
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Получает или задаёт имя художника.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Возвращает строковое представление художника.
        /// </summary>
        /// <returns>
        /// Строка, содержащая идентификатор и имя художника.
        /// </returns> 
        public override string ToString() =>
            $"id = {_id}, name = {_name}";
    }
}
