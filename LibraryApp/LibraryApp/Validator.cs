using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp
{
    class Validator
    {
        public static string OptionChoice()
        {
            string choice = Console.ReadLine();
            while (string.IsNullOrEmpty(choice) || string.IsNullOrWhiteSpace(choice))
            {
                if (!int.TryParse(choice, out int result))
                {
                    Console.Clear();
                    Console.WriteLine($"That input was not correct for Options");
                    POS.Options();
                    choice = Console.ReadLine();
                }
                else if (result < 1 || result > 4)
                {
                    Console.Clear();
                    Console.WriteLine($"That input was not correct for Options");
                    POS.Options();
                    choice = Console.ReadLine();
                }
                else
                {
                    choice = result.ToString();
                }
            }
            return choice;
        }
    }
}
