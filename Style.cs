namespace CSLab5
{
    // <summary>
    /// Представляет стиль с идентификатором и названием.
    /// </summary>
    class Style
    {
        private int _id;
        private string _name;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Style"/>.
        /// </summary>
        /// <param name="id">Идентификатор стиля.</param>
        /// <param name="name">Название стиля.</param>
        public Style(int id, string name)
        {
            _id = id;
            _name = name;
        }

        /// <summary>
        /// Получает или задаёт идентификатор стиля.
        /// </summary>
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Получает или задаёт название стиля.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Возвращает строковое представление стиля.
        /// </summary>
        /// <returns>Строка, содержащая идентификатор и название стиля.</returns>
        public override string ToString() => $"id = {_id}, name = {_name}";
    }
}
