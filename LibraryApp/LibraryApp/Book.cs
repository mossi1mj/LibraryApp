using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp
{
    class Book
    {
        public string Title;

        public string Author;

        public bool Status;

        public Book()
        {

        }

        public Book(string title, string author, bool status)
        {
            Title = title;
            Author = author;
            Status = status;
        }

        public string CheckInOption()
        {
            if (Status)
            {
                return "Available";
            }
            return "Checked Out";
        }
        public virtual string Definition()
        {
            return $"{Title,-40} {Author,-20} {CheckInOption(),-10}";
        }

        public static Book CSVToBook(string csv)
        {
            string[] bookArray = csv.Split(',');
            return new Book(bookArray[0], bookArray[1], bool.Parse(bookArray[2]));
        }

        public static string BookToCSV(Book book)
        {
            return $"{book.Title},{book.Author},{book.Status}";
        }


    }
}
