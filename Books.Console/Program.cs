
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
                            .ForEach(a => PrintInBackground(a + Environment.NewLine, ConsoleColor.DarkBlue));
                        PrintOutEmptyLines(lines - 5);
                    }
                    else if (authors.Count() == 1)
                    {
                        Console.WriteLine($"Books found by {authors.First()}:");

                        SearchByAuthor.Search(booksAll, authors.First())
                            .ToList()
                            .ForEach(book =>
                            {
                                PrintInBackground(
                                    $"{book.title} of year {book.year} pages {book.pages} in {book.language } [{book.country}] {Environment.NewLine}Categories: {string.Join(",", book.categories)} {Environment.NewLine}",
                                    ConsoleColor.DarkGreen);
                                Console.WriteLine("------------");
                            });
                    }
                    else
                    {
                        Console.WriteLine("Found authors:");
                        authors.ToList().ForEach(Console.WriteLine);
                        PrintOutEmptyLines(lines - authors.Count());
                    }
                }
                else
                {
                    PrintOutEmptyLines(lines);
                }
                Console.WriteLine("Type author name or part of it. Type 'exit' to exit..");
                lineRead = Console.ReadLine();

            } while (!lineRead.ToLower().Contains("exit"));
        }


        private static void PrintOutEmptyLines(int number)
        {
            Enumerable.Range(0, number).ToList().ForEach(_ => Console.WriteLine());
        }

        private static void PrintInBackground(string text, ConsoleColor color)
        {
            var current = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.Write(text);
            Console.BackgroundColor = current;
        }

    }
}
