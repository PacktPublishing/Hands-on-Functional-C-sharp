
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

        private static ConsoleColor authorColor = ConsoleColor.DarkBlue;
        private static ConsoleColor bookColor = ConsoleColor.DarkGreen;
        private static ConsoleColor suggestionColor = ConsoleColor.DarkCyan;


        public static void Main()
        {
            var booksAll = bookPersist.Read();

            string lineRead = string.Empty;
            Console.WriteLine("Search by author.");
            do
            {
                if (lineRead.Count() >= 3)
                {
                    var authors = SearchByAuthor.AutocompleteAuthors(booksAll, lineRead);
                    if (authors.Count() == 0)
                    {
                        Console.Write($"No author with name like ");
                        PrettyPrint(lineRead, authorColor);
                        Console.WriteLine(" found. Try:");
                        SearchByAuthor.SuggestAuthors(booksAll)
                            .ToList()
                            .ForEach(a => PrettyPrintLine(a, authorColor));

                    }
                    else if (authors.Count() == 1)
                    {
                        Console.WriteLine($"Books found by {authors.First()}:");

                        SearchByAuthor.Search(booksAll, authors.First())
                            .ToList()
                            .ForEach(book =>
                            {
                                PrettyPrintLine(
                                    $"{book.title} of year {book.year} pages {book.pages} in {book.language } [{book.country}] {Environment.NewLine}Categories: {string.Join(",", book.categories)}",
                                    bookColor);
                                Console.WriteLine("------------");
                            });
                    }
                    else
                    {
                        Console.WriteLine("Found authors:");
                        authors.ToList().ForEach(Console.WriteLine);
                    }
                }
                else
                {
                    PrettyPrintLine("Type at least 3 charachters to see list of autors matching or list of books for the single matched author.",
                        suggestionColor);
                }

                FillOutConsole(2);
                Console.WriteLine("Type author name or part of it. Type 'exit' to exit..");
                lineRead = Console.ReadLine();
                NoteCursorPosition();

            } while (!lineRead.ToLower().Contains("exit"));
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

        private static void PrettyPrintLine(string text, ConsoleColor backgroundColor)
        {
            PrettyPrint(text, backgroundColor);
            Console.WriteLine();
        }

        private static int lastCursorPositionBottom = 0;
        private static void NoteCursorPosition()
        {
            lastCursorPositionBottom = Console.CursorTop;
        }

        public static void FillOutConsole(int reserveForPromptMessage = 0)
        {
            var lenghtToFill = LinesToEndOfConsole();
            PrintOutEmptyLines(lenghtToFill - reserveForPromptMessage);
        }


        private static int LinesToEndOfConsole()
        {
            var linesPrinted = Console.CursorTop - lastCursorPositionBottom;
            var linesToEndOfWindow = Console.WindowHeight - linesPrinted;
            return linesToEndOfWindow;
        }

    }
}
