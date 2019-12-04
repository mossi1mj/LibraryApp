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

            //this is only checking that the string is not null or blank and within a range
            while (string.IsNullOrEmpty(choice) || string.IsNullOrWhiteSpace(choice))
            {
                //this checks that input is a number
                if (!int.TryParse(choice, out int result))
                {
                    Console.Clear();
                    Console.WriteLine($"That input was not correct for Options");
                    POS.Options();
                    choice = Console.ReadLine();
                }
                //this checks that if it's a number that is in a certain range
                else if (result < 1 || result > 4)
                {
                    Console.Clear();
                    Console.WriteLine($"That input was not correct for Options");
                    POS.Options();
                    choice = Console.ReadLine();
                }
                //if it is a number and within the range then turn result into a string and hold that value in choice variable
                else
                {
                    choice = result.ToString();
                }
            }

            return choice;
        }
    }
}
