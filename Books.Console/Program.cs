
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
            var books = bookPersist.Read();

            string userInput = string.Empty;
            Console.WriteLine("Search by author.");
            do
            {
                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    var authors = SearchByAuthor.MatchAutors(books, userInput);
                    if (authors.Count() == 0)
                    {
                        PrintAuthorIn("No author with name like ", userInput, " found. Try:");

                        SearchByAuthor.SuggestAuthors(books)
                            .ToList()
                            .ForEach(a => PrettyPrintLine(a, authorColor));
                    }
                    else if (authors.Count() == 1)
                    {
                        PrintAuthorIn("Books found by ", authors.First(), ":");

                        SearchByAuthor.Search(books, authors.First())
                            .ToList()
                            .ForEach(PrintBook);
                    }
                    else
                    {
                        Console.WriteLine("Found multiple authors. Specify one of them:");
                        authors.ToList().ForEach(a => PrettyPrintLine(a, authorColor));
                    }
                }
                
                FillOutConsole(4);
                Console.WriteLine("Type author name or part of it. Type 'exit' to exit..");
                userInput = Console.ReadLine();
                NoteCursorPosition();

            } while (!userInput.ToLower().Contains("exit"));
        }

        private static void PrintAuthorIn(string start, string authorName, string end)
        {
            Console.Write(start);
            PrettyPrint(authorName, authorColor);
            Console.WriteLine(end);
        }

        private static void PrintBook(Book book)
        {
            PrettyPrintLine(
                $"{book.title}, of year {book.year}, pages {book.pages} in {book.language } [{book.country}]",
                bookColor);
            PrettyPrintLine($"Categories: {string.Join(",", book.categories)}", bookColor);
            Console.WriteLine("------------");
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
