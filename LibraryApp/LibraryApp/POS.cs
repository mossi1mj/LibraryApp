using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryApp
{
    class POS
    {
        private static List<Book> currentInventory { get; } = new List<Book>();
        public static void BookPOS()
        {
            GetCurrentInventory();
            MainMenu();
        }
        private static void GetCurrentInventory()
        {
            StreamReader sr = new StreamReader(@"C:\Users\mmoss\source\repos\LibraryApp\LibraryApp\LibraryApp\BookList.txt");
            string line = sr.ReadLine();
            while (line != null)
            {
                currentInventory.Add(Book.CSVToBook(line));
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private static void SaveCurrentInventory()

        {
            StreamWriter sw = new StreamWriter(@"C:\Users\mmoss\source\repos\LibraryApp\LibraryApp\LibraryApp\BookList.txt");
            foreach (Book item in currentInventory)
            {
                sw.WriteLine(Book.BookToCSV(item));
            }
            sw.Close();
        }

        public static Book SearchLibrary(List<Book> List, string input)
        {
            foreach (Book item in List)
            {
                if (item.Definition().ToLower().Contains(input.ToLower()))
                {
                    return item;
                }
            }
            return null;
        }

        private static void DisplayCurrentInventory()
        {
            foreach (var item in currentInventory)
            {
                Console.WriteLine(item.Definition());
            }
        }

        private static void DisplayAvailableInventory()
        {
            foreach (var item in currentInventory)
            {
                if (item.Status == false)
                {
                    Console.WriteLine(item.Definition());
                }
            }
        }

        static string GetCheckOutDate()
        {
            return DateTime.Now.AddDays(14).ToString("MMMM dd");
        }

        public static void Options()
        {
            //make a simple menu with options for the User
            Console.WriteLine("=====================================================================");
            Console.WriteLine("Welcome to the Library!  Please make a selection from the Menu below.");
            Console.WriteLine("1. Display Current Inventory");
            Console.WriteLine("2. Check Out Book from Library");
            Console.WriteLine("3. Check In a Book");
            Console.WriteLine("4. Type quit to exit program");
            Console.WriteLine("=====================================================================");
        }
        private static void MainMenu()
        {
            bool continueProgram = true;
            while (continueProgram)
            {
                Options();
                string userChoice = Validator.OptionChoice();

                if (userChoice.ToLower() == "quit" || userChoice == "4")
                {
                    SaveCurrentInventory();
                    continueProgram = false;
                }
                else if (userChoice == "1")
                {
                    DisplayCurrentInventory();
                }
                else if (userChoice == "2")
                {
                    while (true)
                    {
                        Console.WriteLine("Search the Book you want to Check Out:");
                        string input = Console.ReadLine();
                        Book foundBook = SearchLibrary(currentInventory, input);
                        if (foundBook != null)
                        {
                            Console.WriteLine($"Found {foundBook.Title}");
                            if (foundBook.Status == true)
                            {
                                foundBook.Status = false;
                                Console.WriteLine($"{foundBook.Title} is now Checked Out!");
                                Console.WriteLine($"Please return this book by {GetCheckOutDate()}");
                            }
                            else Console.WriteLine("This book is already Checked Out!");
                            SaveCurrentInventory();
                            break;
                        }
                    }

                }
                else if (userChoice == "3")
                {
                    while (true)
                    {
                        Console.WriteLine("Which Book do you want to Check In: ");
                        DisplayAvailableInventory();
                        string input = Console.ReadLine();
                        Book foundBook = SearchLibrary(currentInventory, input);
                        if (foundBook != null)
                        {
                            if (foundBook.Status == false)
                            {
                                foundBook.Status = true;
                                Console.WriteLine($"{foundBook.Title} is now Checked In!");
                            }
                            else Console.WriteLine("This book is already Checked In!");
                            SaveCurrentInventory();
                            break;
                        }
                    }

                }

            }
        }

    }
}
