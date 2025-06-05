namespace CSLab5
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Выберите режим протоколирования:");
            Console.WriteLine("1 - Дописать в существующий файл");
            Console.WriteLine("2 - Создать новый файл");
            Console.Write("Ваш выбор: ");
            string? choiceFile = Console.ReadLine();
            string protocolFile = "Protocol.txt";

            if (choiceFile == "2")
            {
                bool created = false;
                while (!created)
                {
                    protocolFile = UserInput.StringInput("Введите название нового файла: ");
                    if (!protocolFile.EndsWith(".txt"))
                    {
                        Console.WriteLine("Файл должен иметь расширение .txt!");
                        continue;
                    }

                    try
                    {
                        if (File.Exists(protocolFile)) File.Delete(protocolFile);
                        File.Create(protocolFile).Close();
                        created = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
                    }
                }
            }
            else if (choiceFile != "1" || !File.Exists(protocolFile))
            {
                Console.WriteLine("Файл не найден. Завершение работы.");
                return;
            }


            Protocoler protocoler = new(protocolFile);
            protocoler.WriteLine("Программа запущена");
            protocoler.Save();

            Database database = new("LR5-var11.xls");

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1 - Просмотр базы данных");
                Console.WriteLine("2 - Удаление элемента по ID");
                Console.WriteLine("3 - Корректировка элемента по ID");
                Console.WriteLine("4 - Добавление элемента");
                Console.WriteLine("5 - Запрос: художники с >5 картин в части 2 Эрмитажа");
                Console.WriteLine("6 - Запрос: список картин и авторов (содержит 'ов')");
                Console.WriteLine("7 - Запрос: количество стилей с картинами после 1800 года");
                Console.WriteLine("8 - Запрос: картины в стиле 'Импрессионизм' с автором и годом");
                Console.WriteLine("9 - Выход");

                string input = Console.ReadLine() ?? "";

                switch (input)
                {
                    case "1":
                        database.PrintAll(protocoler);
                        break;

                    case "2":
                        Console.WriteLine("Удалить из таблицы:");
                        Console.WriteLine("1 - Художники");
                        Console.WriteLine("2 - Стили");
                        Console.WriteLine("3 - Картины");
                        string delChoice = Console.ReadLine() ?? "";
                        int idDel = UserInput.IntInput(true, "Введите ID для удаления: ");
                        EntityType? delEntity = delChoice switch
                        {
                            "1" => EntityType.Artist,
                            "2" => EntityType.Style,
                            "3" => EntityType.Painting,
                            _ => null
                        };

                        if (delEntity == null)
                        {
                            Console.WriteLine("Неверный выбор.");
                        }
                        else if (database.DeleteById(delEntity.Value, idDel, protocoler))
                        {
                            Console.WriteLine("Элемент удалён.");
                        }
                        else
                        {
                            Console.WriteLine("Элемент не найден.");
                        }

                        break;

                    case "3":
                        Console.WriteLine("Редактировать в таблице:");
                        Console.WriteLine("1 - Художники");
                        Console.WriteLine("2 - Стили");
                        Console.WriteLine("3 - Картины");
                        string editChoice = Console.ReadLine() ?? "";
                        int idEdit = UserInput.IntInput(true, "Введите ID: ");

                        if (editChoice == "1")
                        {
                            string newName = UserInput.StringInput("Новое имя художника: ");
                            database.EditArtist(idEdit, newName, protocoler);
                        }
                        else if (editChoice == "2")
                        {
                            string newName = UserInput.StringInput("Новое название стиля: ");
                            database.EditStyle(idEdit, newName, protocoler);
                        }
                        else if (editChoice == "3")
                        {
                            string newName = UserInput.StringInput("Новое название картины: ");
                            int newArtist = UserInput.IntInput(true, "Новый ID художника: ");
                            int newErm = UserInput.IntInput(true, "Новая часть Эрмитажа: ");
                            int newYear = UserInput.IntInput(true, "Новый год: ");
                            int newStyle = UserInput.IntInput(true, "Новый ID стиля: ");
                            database.EditPainting(idEdit, newName, newArtist, newErm, newYear, newStyle, protocoler);
                        }
                        else
                        {
                            Console.WriteLine("Неверный выбор.");
                        }
                        break;


                    case "4":
                        Console.WriteLine("1 - Художник 2 - Стиль 3 - Картина");
                        string option = Console.ReadLine();
                        if (option == "1")
                        {
                            string name = UserInput.StringInput("Имя художника: ");
                            database.AddArtist(name, protocoler);
                        }
                        else if (option == "2")
                        {
                            string name = UserInput.StringInput("Название стиля: ");
                            database.AddStyle(name, protocoler);
                        }
                        else if (option == "3")
                        {
                            string name = UserInput.StringInput("Название картины: ");
                            int idArtist = UserInput.IntInput(true, "ID художника: ");
                            int ermitage = UserInput.IntInput(true, "Часть Эрмитажа: ");
                            int year = UserInput.IntInput(true, "Год: ");
                            int idStyle = UserInput.IntInput(true, "ID стиля: ");
                            database.AddPainting(name, idArtist, ermitage, year, idStyle);
                        }
                        break;

                    case "5":
                        int request1 = database.CountArtistsWithMoreThan5PaintingsInPart2();
                        Console.WriteLine($"Результат: {request1}");
                        protocoler.WriteLine("Запрос 1: " + request1);
                        protocoler.Save();
                        break;

                    case "6":
                        var request2 = database.GetPaintingsWithAuthorNameContaining("ов");
                        foreach (var item in request2)
                            Console.WriteLine(item);
                        protocoler.WriteLine("Запрос 2 выполнен.");
                        protocoler.Save();
                        break;

                    case "7":
                        int request3 = database.GetPaintingsByStyleName("Классицизм").Count();
                        Console.WriteLine($"Результат: {request3}");
                        protocoler.WriteLine("Запрос 3: " + request3);
                        protocoler.Save();
                        break;

                    case "8":
                        var request4 = database.GetPaintingsByStyleName("Импрессионизм");
                        foreach (var item in request4)
                            Console.WriteLine(item);
                        protocoler.WriteLine("Запрос 4 выполнен.");
                        protocoler.Save();
                        break;

                    case "9":
                        protocoler.WriteLine("Выход из программы");
                        protocoler.Close();
                        return;

                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }
    }
}
