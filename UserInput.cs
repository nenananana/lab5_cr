/// <summary>
/// Класс для получения валидированного 
/// пользовательского ввода с консоли.
/// </summary>
class UserInput
{
    /// <summary>
    /// Запрашивает у пользователя ввод строки 
    /// с проверкой на пустое значение.
    /// </summary>
    /// <param name="prompt">Текст подсказки пользователю. 
    /// По умолчанию "Введите строку: ".</param>
    /// <returns>Введённая пользователем непустая строка.</returns>
    public static string StringInput(string prompt = "Введите строку: ")
    {
        string? userInput;

        do
        {
            Console.Write(prompt);
            userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("Строка не должна быть пустой!");
            }

            else
            {
                return userInput;
            }
        } 

        while (string.IsNullOrEmpty(userInput));

        return userInput;
    }

    /// <summary>
    /// Запрашивает у пользователя ввод целого числа 
    /// с возможной проверкой на положительность.
    /// </summary>
    /// <param name="isPositive">Если true, число должно 
    /// быть положительным (>= 0).</param>
    /// <param name="prompt">Текст подсказки пользователю. 
    /// По умолчанию "Введите целое число: ".</param>
    /// <returns>Введённое пользователем целое число, 
    /// удовлетворяющее условию положительности при необходимости.</returns>
    public static int IntInput(bool isPositive = false, string prompt = "Введите целое число: ")
    {
        string? userInput;
        bool isValidInput = false;
        int result = 0;

        while (!isValidInput)
        {
            Console.Write(prompt);
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out result))
            {
                if (isPositive && result < 0)
                {
                    Console.WriteLine("Число должно быть положительным!");
                }

                else
                {
                    isValidInput = true;
                }
            }

            else
            {
                Console.WriteLine("Введенное значение не является целым числом!");
            }
        }

        return result;
    }
}