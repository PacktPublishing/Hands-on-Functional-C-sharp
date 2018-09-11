
#region imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion
namespace Books.ConsoleApp
{
    class Program
    {
        private static IBooksSource BooksSource = new BooksJsonSource();

        // we will use this as global state
        private static List<BooksByAuthor> BooksByAuthorCatalog;

        public static void Main()
        {
            DemoLINQ.Demo();

            IEnumerable<Book> books = BooksSource.Read();

            BooksByAuthorCatalog = new List<BooksByAuthor>();

            foreach(var book in books)
            {
                Console.WriteLine("Actions available:");
                Console.WriteLine("1 - Output all books by author (Section 2)");
                Console.WriteLine("2 - Search books by title (Section 3)");
                Console.WriteLine("Any other key - Exit");

                var key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1': Output.BooksByAuthor(BooksSource.Read()); break;
                    case '2': DoSearch(); break;
                    default: return;
                }
            }
        }

        public static void DoSearch()
        {
            var books = BooksSource.Read();
            while (true)
            {
                Console.WriteLine("\nType author's name or part of it. \n^^^^Type 'exit' to go back^^^^");
                var authorName = Console.ReadLine();
                if (authorName == "exit")
                {
                    return;
                }

                var authors = Search.ByTitle(books, authorName);

                if (authors.Count() == 0)
                {
                    Console.WriteLine($"No authors found for '{authorName}'");
                }
                else
                {
                    foreach (var author in authors)
                    {
                        Console.WriteLine(author);
                    }
                }

                Console.WriteLine("----------------------");
            }
        }
    }
}
