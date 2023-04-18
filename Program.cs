using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HW_DelegateMenu.Game;


namespace HW_DelegateMenu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StartSettings();

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
                userChoice = InputData.InputNumber("Ваш выбор: ", (int)MenuValue.MinMenu, (int)MenuValue.MaxMenu);
                Console.Clear();

                // вызываем метод по индексу
                ((DelMenu)(myMenu.GetInvocationList()[userChoice]))();
            } while (userChoice != (int)MenuValue.MinMenu);
        }
    }
}
