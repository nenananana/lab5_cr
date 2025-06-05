using Aspose.Cells;

namespace CSLab5
{
    /// <summary>
    /// Представляет базу данных, содержащую художников, стили и картины, загружаемую и сохраняемую в Excel-файл.
    /// </summary>
    class Database
    {
        private readonly string _filePath;
        private readonly List<Artist> _artists;
        private readonly List<Style> _styles;
        private readonly List<Painting> _paintings;

        /// <summary>
        /// Возвращает путь к Excel-файлу базы данных.
        /// </summary>
        public string FilePath => _filePath;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Database"/> и загружает данные из указанного Excel-файла.
        /// </summary>
        /// <param name="filePath">Путь к Excel-файлу (.xls).</param>
        /// <exception cref="FileNotFoundException">Выбрасывается, если файл не найден.</exception>
        /// <exception cref="ArgumentException">Выбрасывается, если файл не имеет расширение .xls.</exception>
        public Database(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файла не существует!");
            if (!filePath.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Тип файла должен быть .xls!");

            Workbook workbook = new(filePath);

            _artists = new List<Artist>();
            Worksheet artistsWorksheet = workbook.Worksheets["Художники"];
            for (int rowIndex = 1; rowIndex <= artistsWorksheet.Cells.MaxDataRow; rowIndex++)
            {
                Row currentRow = artistsWorksheet.Cells.Rows[rowIndex];
                _artists.Add(new Artist(currentRow[0].IntValue, currentRow[1].StringValue));
            }

            _styles = new List<Style>();
            Worksheet stylesWorksheet = workbook.Worksheets["Стиль"];
            for (int rowIndex = 1; rowIndex <= stylesWorksheet.Cells.MaxDataRow; rowIndex++)
            {
                Row currentRow = stylesWorksheet.Cells.Rows[rowIndex];
                _styles.Add(new Style(currentRow[0].IntValue, currentRow[1].StringValue));
            }

            _paintings = new List<Painting>();
            Worksheet paintingsWorksheet = workbook.Worksheets["Картины"];
            for (int rowIndex = 1; rowIndex <= paintingsWorksheet.Cells.MaxDataRow; rowIndex++)
            {
                Row currentRow = paintingsWorksheet.Cells.Rows[rowIndex];
                try
                {
                    int id = currentRow[0].IntValue;
                    string name = currentRow[1].StringValue;
                    int artistId = currentRow[2].IntValue;
                    int hermitagePart = currentRow[3].IntValue;
                    int year = currentRow[4].Type == CellValueType.IsNull ? 0 : currentRow[4].IntValue;
                    int styleId = currentRow[5].IntValue;

                    _paintings.Add(new Painting(id, name, artistId, hermitagePart, year, styleId));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при чтении строки {rowIndex + 1}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Добавляет нового художника в базу данных.
        /// </summary>
        /// <param name="name">Имя художника.</param>
        /// <param name="logger">Экземпляр логгера для записи действия.</param>
        public void AddArtist(string name, Protocoler logger)
        {
            int nextId = _artists.Count > 0 ? _artists[^1].Id + 1 : 1;
            var newArtist = new Artist(nextId, name);
            _artists.Add(newArtist);
            Console.WriteLine($"Добавлен художник: {newArtist}");
            logger.WriteLine($"Добавлен художник: {newArtist}");
            logger.Save();
            Save();
        }

        /// <summary>
        /// Добавляет новый стиль в базу данных.
        /// </summary>
        /// <param name="name">Название стиля.</param>
        /// <param name="logger">Экземпляр логгера для записи действия.</param>
        public void AddStyle(string name, Protocoler logger)
        {
            int nextId = _styles.Count > 0 ? _styles[^1].Id + 1 : 1;
            var newStyle = new Style(nextId, name);
            _styles.Add(newStyle);
            Console.WriteLine($"Добавлен стиль: {newStyle}");
            logger.WriteLine($"Стиль добавлен: {newStyle}");
            logger.Save();
            Save();
            Console.WriteLine("База данных сохранена после добавления стиля.");
        }

        /// <summary>
        /// Добавляет новую картину в базу данных.
        /// </summary>
        /// <param name="name">Название картины.</param>
        /// <param name="artistId">ID художника.</param>
        /// <param name="hermitagePart">Номер зала Эрмитажа.</param>
        /// <param name="year">Год создания.</param>
        /// <param name="styleId">ID стиля.</param>
        public void AddPainting(string name, int artistId, int hermitagePart, int year, int styleId)
        {
            int nextId = _paintings.Count > 0 ? _paintings[^1].Id + 1 : 1;
            _paintings.Add(new Painting(nextId, name, artistId, hermitagePart, year, styleId));
            Save();
        }

        /// <summary>
        /// Выводит всё содержимое базы данных в консоль и логирует действия.
        /// </summary>
        /// <param name="logger">Экземпляр логгера для записи просмотра.</param>
        /// <exception cref="ArgumentNullException">Если <paramref name="logger"/> равен null.</exception>
        public void PrintAll(Protocoler logger)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            Console.WriteLine("--- Художники ---");
            foreach (var artist in _artists)
            {
                Console.WriteLine(artist);
                logger.WriteLine("Просмотр: " + artist.ToString());
            }

            Console.WriteLine("--- Стили ---");
            foreach (var style in _styles)
            {
                Console.WriteLine(style);
                logger.WriteLine("Просмотр: " + style.ToString());
            }

            Console.WriteLine("--- Картины ---");
            foreach (var painting in _paintings)
            {
                Console.WriteLine(painting);
                logger.WriteLine("Просмотр: " + painting.ToString());
            }

            logger.Save();
        }

        /// <summary>
        /// Удаляет сущность (художник, стиль, картина) по ID.
        /// </summary>
        /// <param name="entityType">Тип сущности.</param>
        /// <param name="id">ID сущности.</param>
        /// <param name="logger">Экземпляр логгера для записи действия.</param>
        /// <returns>True, если удаление выполнено успешно; иначе — false.</returns>
        public bool DeleteById(EntityType entityType, int id, Protocoler logger)
        {
            switch (entityType)
            {
                case EntityType.Artist:
                    var artist = _artists.FirstOrDefault(x => x.Id == id);
                    if (artist != null)
                    {
                        _artists.Remove(artist);
                        logger.WriteLine($"Удалён художник: {artist}");
                        Save();
                        return true;
                    }
                    break;

                case EntityType.Style:
                    var style = _styles.FirstOrDefault(x => x.Id == id);
                    if (style != null)
                    {
                        _styles.Remove(style);
                        logger.WriteLine($"Удалён стиль: {style}");
                        Save();
                        return true;
                    }
                    break;

                case EntityType.Painting:
                    var painting = _paintings.FirstOrDefault(x => x.Id == id);
                    if (painting != null)
                    {
                        _paintings.Remove(painting);
                        logger.WriteLine($"Удалена картина: {painting}");
                        Save();
                        return true;
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// Изменяет имя художника по его ID.
        /// </summary>
        /// <param name="id">ID художника.</param>
        /// <param name="newName">Новое имя художника.</param>
        /// <param name="logger">Экземпляр логгера для записи действия.</param>
        /// <returns>True, если редактирование прошло успешно; иначе — false.</returns>
        public bool EditArtist(int id, string newName, Protocoler logger)
        {
            var artist = _artists.FirstOrDefault(x => x.Id == id);
            if (artist != null)
            {
                logger.WriteLine($"Изменено имя художника: {artist.Name} → {newName}");
                artist.Name = newName;
                Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Изменяет название стиля по его ID.
        /// </summary>
        /// <param name="id">ID стиля.</param>
        /// <param name="newName">Новое название стиля.</param>
        /// <param name="logger">Экземпляр логгера для записи действия.</param>
        /// <returns>True, если редактирование прошло успешно; иначе — false.</returns>
        public bool EditStyle(int id, string newName, Protocoler logger)
        {
            var style = _styles.FirstOrDefault(x => x.Id == id);
            if (style != null)
            {
                logger.WriteLine($"Изменено имя стиля: {style.Name} → {newName}");
                style.Name = newName;
                Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Изменяет данные картины по её ID.
        /// </summary>
        /// <param name="id">ID картины.</param>
        /// <param name="newName">Новое название картины.</param>
        /// <param name="newArtistId">Новый ID художника.</param>
        /// <param name="newHermitagePart">Новый номер зала Эрмитажа.</param>
        /// <param name="newYear">Новый год создания.</param>
        /// <param name="newStyleId">Новый ID стиля.</param>
        /// <param name="logger">Экземпляр логгера для записи действия.</param>
        /// <returns>True, если редактирование прошло успешно; иначе — false.</returns>
        public bool EditPainting(int id, string newName, int newArtistId, int newHermitagePart, int newYear, int newStyleId, Protocoler logger)
        {
            var painting = _paintings.FirstOrDefault(x => x.Id == id);
            if (painting != null)
            {
                logger.WriteLine($"Изменена картина: {painting}");
                painting.Name = newName;
                painting.IdArtist = newArtistId;
                painting.ErmitagePart = newHermitagePart;
                painting.Year = newYear;
                painting.IdStyle = newStyleId;
                logger.WriteLine($"→ стало: {painting}");
                Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает количество художников, у которых более 5 картин во 2-й части Эрмитажа.
        /// </summary>
        /// <returns>Количество таких художников.</returns>
        public int CountArtistsWithMoreThan5PaintingsInPart2()
        {
            var query = from painting in _paintings
                        where painting.ErmitagePart == 2
                        group painting by painting.IdArtist into artistGroup
                        where artistGroup.Count() > 5
                        select artistGroup.Key;
            return query.Count();
        }

        /// <summary>
        /// Возвращает список картин, авторы которых содержат указанную подстроку в имени.
        /// </summary>
        /// <param name="searchString">Подстрока для поиска в имени автора.</param>
        /// <returns>Список объектов с названием картины и именем автора.</returns>
        public IEnumerable<object> GetPaintingsWithAuthorNameContaining(string searchString)
        {
            return from painting in _paintings
                   join artist in _artists on painting.IdArtist equals artist.Id
                   where artist.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                   select new { Painting = painting.Name, Author = artist.Name };
        }

        /// <summary>
        /// Возвращает список картин, относящихся к заданному стилю.
        /// </summary>
        /// <param name="styleName">Название стиля.</param>
        /// <returns>Список объектов с названием картины, именем автора и годом создания.</returns>
        public IEnumerable<object> GetPaintingsByStyleName(string styleName)
        {
            return from painting in _paintings
                   join artist in _artists on painting.IdArtist equals artist.Id
                   join style in _styles on painting.IdStyle equals style.Id
                   where style.Name.Equals(styleName, StringComparison.OrdinalIgnoreCase)
                   select new { Painting = painting.Name, Author = artist.Name, Year = painting.Year };
        }

        /// <summary>
        /// Сохраняет текущие данные базы в Excel-файл.
        /// </summary>
        public void Save()
        {
            Workbook workbook = new(_filePath);

            Worksheet artistsWorksheet = workbook.Worksheets["Художники"];
            artistsWorksheet.Cells.DeleteRows(1, artistsWorksheet.Cells.Rows.Count - 1);
            int rowIndex = 1;
            foreach (var artist in _artists)
            {
                artistsWorksheet.Cells[rowIndex, 0].PutValue(artist.Id);
                artistsWorksheet.Cells[rowIndex, 1].PutValue(artist.Name);
                rowIndex++;
            }

            Worksheet stylesWorksheet = workbook.Worksheets["Стиль"];
            stylesWorksheet.Cells.DeleteRows(1, stylesWorksheet.Cells.Rows.Count - 1);
            rowIndex = 1;
            foreach (var style in _styles)
            {
                stylesWorksheet.Cells[rowIndex, 0].PutValue(style.Id);
                stylesWorksheet.Cells[rowIndex, 1].PutValue(style.Name);
                rowIndex++;
            }

            Worksheet paintingsWorksheet = workbook.Worksheets["Картины"];
            paintingsWorksheet.Cells.DeleteRows(1, paintingsWorksheet.Cells.Rows.Count - 1);
            rowIndex = 1;
            foreach (var painting in _paintings)
            {
                paintingsWorksheet.Cells[rowIndex, 0].PutValue(painting.Id);
                paintingsWorksheet.Cells[rowIndex, 1].PutValue(painting.Name);
                paintingsWorksheet.Cells[rowIndex, 2].PutValue(painting.IdArtist);
                paintingsWorksheet.Cells[rowIndex, 3].PutValue(painting.ErmitagePart);
                paintingsWorksheet.Cells[rowIndex, 4].PutValue(painting.Year);
                paintingsWorksheet.Cells[rowIndex, 5].PutValue(painting.IdStyle);
                rowIndex++;
            }

            workbook.Save(_filePath);
        }
    }

    /// <summary>
    /// Определяет тип сущности в базе данных.
    /// </summary>
    public enum EntityType
    {
        Artist,
        Style,
        Painting
    }
}