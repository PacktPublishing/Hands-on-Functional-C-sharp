using System;
using System.Collections.Generic;

namespace for_loop_pain_points
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new List<Book>();

            for (int i = 0; i < collection.Count; i++)
            {
                var item = collection[i];
                // do stuff with item
            }

            // Solve();
        }

        public static void Solve()
        {
            // having the numbers 1,2,2,3,4,4,5,7,9....n (limiting the collecion for brevity)
            var x = new List<int> { 1, 2, 2, 3, 3, 3, 4, 5, 7, 9, 10, 11, 11, 12, 13, 15 };

            // remove the duplicates - using the least ammont of memory and processing power
            for (int i = 0; i < x.Count - 1; i++)
            {
                if (x[i] == x[i + 1])
                {
                    x.RemoveAt(i + 1);
                }
            }
        }
    }

    public class BooksByAuthor
    {
        public readonly string author;
        public readonly List<Book> books;

        public BooksByAuthor(string author, List<Book> books)
        {
            this.author = author;
            this.books = books;
        }

        private static void OutputBooksByAuthor(List<BooksByAuthor> catalogue)
        {
            for (int i = 0; i < catalogue.Count; i++)
            {
                var ba = catalogue[i];
                Console.Write(ba.author);
                for (int j = 0; j < ba.books.Count; j++)
                {
                    Console.Write(ba.books[j].title + ", ");
                }
                Console.WriteLine(Environment.NewLine);
            }
        }
    }

    public class Book
    {
        public string title { get; set; }
    }
}
