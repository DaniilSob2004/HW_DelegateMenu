using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace HW_Delegate
{
    internal class Program
    {
        static string[] arrMenu = { "1 - новая игра", "2 - показать количество очков", "3 - правила", "4 - об авторе", "0 - выход" };
        const string url = "https://github.com/DaniilSob2004";
        const int amountAttempts = 5;

        static int userGrades = 0;

        enum MenuValue { MinMenu, MaxMenu = 4 };
        enum GameNumber { MinNumber = 1, MaxNumber = 15 };

        public delegate void DelMenu();


        static void ShowMenu()
        {
            Console.WriteLine();
            foreach (string menu in arrMenu)
            {
                Console.WriteLine(menu);
            }
        }

        static int InputNumber(string text, int minValue, int maxValue)
        {
            string choice;

            while (true)
            {
                try
                {
                    Console.Write(text);
                    choice = Console.ReadLine();

                    if (int.Parse(choice) < minValue || int.Parse(choice) > maxValue)
                        Console.WriteLine("Ошибка! Неверный ввод диапазона!");
                    else
                        return int.Parse(choice);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Неверный формат ввода!");
                }
            }
        }


        static void Exit()
        {
            Console.WriteLine("Goodbye!");
        }

        static void StartGame()
        {
            int userNumber;

            // загадывается рандомное число
            int hiddenNumber = new Random().Next((int)GameNumber.MinNumber, (int)GameNumber.MaxNumber);

            Console.WriteLine("\nДобро пожаловать в игру!");
            Console.WriteLine($"Ввод чисел от {(int)GameNumber.MinNumber} до {(int)GameNumber.MaxNumber}!");
            for (int i = 0; i < amountAttempts; i++)
            {
                userNumber = InputNumber($"Попытка #{i + 1}: ", (int)GameNumber.MinNumber, (int)GameNumber.MaxNumber);

                // если угадывает, то плюс 1 очко
                if (hiddenNumber == userNumber)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Поздравляем! Вы угадали число! +1 очко!");
                    Console.ForegroundColor = ConsoleColor.White;

                    userGrades++;
                    break;
                }

                // если это последняя попытка(значит не угадал), то минус 1 очко
                else if (i == amountAttempts - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("К сожалению вы не угадали число( -1 очко...");
                    Console.ForegroundColor = ConsoleColor.White;

                    if (userGrades > 0) userGrades--;
                }
            }
        }

        static void ShowGrades()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Количество очков: {userGrades})");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Rules()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Игра называется 'Угадай число'");
            Console.WriteLine($"У вас есть {amountAttempts} попыток для того чтобы угадать число от {(int)GameNumber.MinNumber} до {(int)GameNumber.MaxNumber}.");
            Console.WriteLine("Желаю удачи!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void AboutAuthor()
        {
            // открываем ссылку на мой гитхаб в браузере 
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
        }


        static void Main(string[] args)
        {
            int userChoice;

            // создаём цепочку(массив) методов
            DelMenu myMenu = Exit;
            myMenu += StartGame;
            myMenu += ShowGrades;
            myMenu += Rules;
            myMenu += AboutAuthor;

            do
            {
                ShowMenu();
                userChoice = InputNumber("Ваш выбор: ", (int)MenuValue.MinMenu, (int)MenuValue.MaxMenu);

                // вызываем метод по индексу
                ((DelMenu)(myMenu.GetInvocationList()[userChoice]))();
            } while (userChoice != (int)MenuValue.MinMenu);
        }
    }
}
