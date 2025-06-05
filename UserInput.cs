﻿class UserInput
{
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
        } while (string.IsNullOrEmpty(userInput));

        return userInput;
    }

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