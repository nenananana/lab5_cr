namespace CSLab5
{
    /// <summary>
    /// Представляет картину с идентификатором, названием, ссылками на художника и стиль, а также дополнительной информацией.
    /// </summary>
    class Painting
    {
        private int _id;
        private string _name;
        private int _idArtist;
        private int _ermitagePart;
        private int _year;
        private int _idStyle;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Painting"/>.
        /// </summary>
        /// <param name="id">Идентификатор картины.</param>
        /// <param name="name">Название картины.</param>
        /// <param name="idArtist">Идентификатор художника.</param>
        /// <param name="ermitagePart">Часть Эрмитажа, в которой находится картина.</param>
        /// <param name="year">Год создания картины.</param>
        /// <param name="idStyle">Идентификатор стиля картины.</param>
        public Painting(int id, string name, int idArtist, int ermitagePart, int year, int idStyle)
        {
            _id = id;
            _name = name;
            _idArtist = idArtist;
            _ermitagePart = ermitagePart;
            _year = year;
            _idStyle = idStyle;
        }

        /// <summary>
        /// Получает или задаёт идентификатор картины.
        /// </summary>
        public int Id { get => _id; set => _id = value; }

        /// <summary>
        /// Получает или задаёт название картины.
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// Получает или задаёт идентификатор художника, автора картины.
        /// </summary>
        public int IdArtist { get => _idArtist; set => _idArtist = value; }

        /// <summary>
        /// Получает или задаёт номер части Эрмитажа, где хранится картина.
        /// </summary>
        public int ErmitagePart { get => _ermitagePart; set => _ermitagePart = value; }

        /// <summary>
        /// Получает или задаёт год создания картины.
        /// </summary>
        public int Year { get => _year; set => _year = value; }

        /// <summary>
        /// Получает или задаёт идентификатор стиля картины.
        /// </summary>
        public int IdStyle { get => _idStyle; set => _idStyle = value; }

        /// <summary>
        /// Возвращает строковое представление картины.
        /// </summary>
        /// <returns>Строка, содержащая основные свойства картины.</returns>
        public override string ToString() =>
            $"id = {_id}, name = {_name}, idArtist = {_idArtist}, ermitagePart = {_ermitagePart}, year = {_year}, idStyle = {_idStyle}";
    }
}
