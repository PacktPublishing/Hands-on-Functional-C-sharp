
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

        public static void Main()
        {
            Book selected = Book.Empty;
            while (true)
            {
                Console.WriteLine("\nActions available:");
                Console.WriteLine("1 - Output all books by author (Section 2)");
                Console.WriteLine("2 - Search books by title (Section 3)");
                Console.WriteLine("3 - Search books by category (Section 4)");
                Console.WriteLine("4 - Select a book(Section 5)");
                if (selected != Book.Empty)
                {
                    Console.WriteLine($"5 - Recommend similar to {selected.title}");
                }
                Console.WriteLine("Any other key - Exit");

                var key = Console.ReadLine();
                switch (key)
                {
                    case "1": Output.BooksByAuthor(BooksSource.Read()); break;
                    case "2": DoSearchByTitle(); break;
                    case "3": DoSearchByCategory(); break;
                    case "4": selected = DoSelectABook(); break;
                    case "5": DoRecommend(selected); break;
                    default: return;
                }
            }
        }

        private static void DoRecommend(Book selected)
        {
            Recommend()
        }

        public static void DoSearchByTitle()
        {
            var books = BooksSource.Read();
            DoSearch(
                searchPrompt: "Search by book title or a part of it.",
                searchFunc: searchTerm => Search.ByTitle(books, searchTerm)
                    .Select(b => BookMap.AuthorAndTitle(b)));
        }

        private static void DoSearchByCategory()
        {
            var books = BooksSource.Read();
            DoSearch("Search by book category or a part of it. \n(for example: fic or Fiction or aut or bio or autobiography) \ncomma separated lists acceptable : juv, sci",
                searchTerm => books
                        .SearchByCategories(searchTerm.FromCommaSeparatedList())
                        .Select(b => BookMap.CategoryAuthorAndTitle(b))
                        //.Highlight(searchTerm.FromCommaSeparatedList())
                        );
        }

        private static void DoSearch(string searchPrompt, Func<string, IEnumerable<string>> searchFunc)
        {
            while (true)
            {
                Console.WriteLine($"\n{searchPrompt} \n^^^^Type 'exit' to go back^^^^");
                var searchTerm = Console.ReadLine();
                if (searchTerm == "exit")
                {
                    return;
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var booksAndAuthorResults = searchFunc(searchTerm);

                    if (booksAndAuthorResults.Count() == 0)
                    {
                        Console.WriteLine($"No books found for '{searchTerm}'");
                    }
                    else
                    {
                        foreach (var res in booksAndAuthorResults)
                        {
                            Console.WriteLine(res);
                        }
                    }
                }

                Console.WriteLine("----------------------");
            }
        }

        private static Book DoSelectABook()
        {
            var books = BooksSource.Read();
            var book = Select.ByTitle(Console.WriteLine, Console.ReadLine, s => Search.ByTitle(books, s));
            if (book == Book.Empty)
            {
                Console.WriteLine("No book selected.");
            }
            else
            {
                Console.WriteLine("Selected book is " + BookMap.CategoryAuthorAndTitle(book));
            }
            return book;
        }
    }

    public static class PrintOutExtensions
    {
        public static IEnumerable<string> Highlight(this IEnumerable<string> strings, params string[] toHighlight)
        {
            return strings.SelectMany(s => toHighlight.Select(h => s.Replace(h, h.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase)));
        }
    }

    public static class SearchTermExtensions
    {
        public static string[] FromCommaSeparatedList(this string s)
        {
            return s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
