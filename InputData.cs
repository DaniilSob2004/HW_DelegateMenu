using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HW_DelegateMenu
{
    public static class InputData
    {
        static public int InputNumber(string text, int minValue, int maxValue)
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
    }
}
