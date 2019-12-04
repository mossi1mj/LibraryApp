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
            //first point the StreamReader object at the text file that holds the current inventory in CSV format
            StreamReader sr = new StreamReader(@"C:\Users\mmoss\source\repos\LibraryApp\LibraryApp\LibraryApp\BookList.txt");

            //string that grabs and holds the first line of the CSV text file
            string line = sr.ReadLine();

            //while loop to iterate through the text file of CSV's building inventory List
            while (line != null)//as long as the first line of the text file is not null then continue with parsing
            {
                //spilt the CSV on the comma's until we have the sparate values indexed in our string array
                currentInventory.Add(Book.CSVToBook(line));

                //we advance the CSV text file to the next row of data
                line = sr.ReadLine();
            }

            //close the text file when done with File I/O operations
            sr.Close();
        }

        private static void SaveCurrentInventory()

        {
            //create new streamwriter object
            StreamWriter sw = new StreamWriter(@"C:\Users\mmoss\source\repos\LibraryApp\LibraryApp\LibraryApp\BookList.txt");

            //iterate through our list of books and first make CSV string out of the objects data, and then write that data to the CSV text file
            foreach (Book item in currentInventory)
            {
                sw.WriteLine(Book.BookToCSV(item));
            }

            //closed the connection saving data to the text file
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
            //iterate through the static List of cars
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
            //establish a bool to control program loop flow
            bool continueProgram = true;

            //while loop to control program flow
            while (continueProgram)
            {
                //display the Options menu for the User
                Options();

                //make a string variable that will hold the Users Choice
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
