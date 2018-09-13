
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
            while (true)
            {
                Console.WriteLine("\nActions available:");
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
                Console.WriteLine("\nSearch by book title or a part of it. \n^^^^Type 'exit' to go back^^^^");
                var authorName = Console.ReadLine();
                if (authorName == "exit")
                {
                    return;
                }
                if (!string.IsNullOrEmpty(authorName))
                {
                    var booksByAuthor = Search.ByTitle(books, authorName);

                    if (booksByAuthor.Count() == 0)
                    {
                        Console.WriteLine($"No books found for '{authorName}'");
                    }
                    else
                    {
                        foreach (var b in booksByAuthor)
                        {
                            Console.WriteLine($"{b.author}: {b.title}");
                        }
                    }
                }

                Console.WriteLine("----------------------");
            }
        }
    }
}
