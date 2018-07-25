
#region imports
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
#endregion
namespace Books.ConsoleApp
{
    class Program
    {
        private static IBookPersist bookPersist = new BooksJsonPersist();
        private static readonly int lines = 20;

        public static void Main()
        {
            var booksAll = bookPersist.Read();

            string lineRead = string.Empty;
            Console.WriteLine("Search by author. Type to see author list.");
            do
            {
                if (lineRead.Count() >= 3)
                {
                    var authors = SearchByAuthor.AutocompleteAuthors(booksAll, lineRead);
                    if (authors.Count() == 0)
                    {
                        Console.WriteLine("No author with such name found. Try:");
                        SearchByAuthor.SuggestAuthors(booksAll)
                            .ToList()
                            .ForEach(a => PrettyPrint(a + Environment.NewLine, ConsoleColor.DarkBlue));

                    }
                    else if (authors.Count() == 1)
                    {
                        Console.WriteLine($"Books found by {authors.First()}:");

                        SearchByAuthor.Search(booksAll, authors.First())
                            .ToList()
                            .ForEach(book =>
                            {
                                PrettyPrint(
                                    $"{book.title} of year {book.year} pages {book.pages} in {book.language } [{book.country}] {Environment.NewLine}Categories: {string.Join(",", book.categories)} {Environment.NewLine}",
                                    ConsoleColor.DarkGreen);
                                Console.WriteLine("------------");
                            });
                    }
                    else
                    {
                        Console.WriteLine("Found authors:");
                        authors.ToList().ForEach(Console.WriteLine);
                    }
                }

                FillOutConsole(2);
                Console.WriteLine("Type author name or part of it. Type 'exit' to exit..");
                lineRead = Console.ReadLine();

            } while (!lineRead.ToLower().Contains("exit"));
        }

        public static void FillOutConsole(int leaveLines = 0)
        {
            var lenghtToFill = linesToEndOfConsole();
            PrintOutEmptyLines(lenghtToFill - leaveLines);
        }

        private static void PrintOutEmptyLines(int number)
        {
            if (number > 0)
            {
                Enumerable.Range(0, number).ToList().ForEach(_ => Console.WriteLine());
            }
        }

        private static void PrettyPrint(string text, ConsoleColor backgroundColor)
        {
            var current = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
            Console.BackgroundColor = current;
        }

        private static int linesToEndOfConsole()
        {
            // shrink buffer and window
            var total = Console.WindowHeight;
            var cursorBotom = total - Console.CursorTop;
            return total - cursorBotom;
        }

    }
}
