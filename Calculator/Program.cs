using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            bool helper = true;
            while (helper)
            {
                
                Console.WriteLine("Введите выражение");
                var expression = calculator.ExpressionImporter();
                var HandledExpression = calculator.Arithmetic(expression);
                if (HandledExpression[1] == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Данные имели неверный формат.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(HandledExpression[0]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                var key= Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    helper = false;
                    continue;
                }
                Console.Clear();
                
            }
        }
    }
}
