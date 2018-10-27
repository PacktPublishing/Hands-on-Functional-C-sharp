
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
        public static void Main()
        {
            IBooksSource BooksSource = new BooksJsonSource();
            var books = BooksSource.Read();

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
                    Console.WriteLine($"5 - Recommend similar to {selected.title} (Section 5)");
                }
                Console.WriteLine("Any other key - Exit");

                var key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1': Output.BooksByAuthor(books); break;
                    case '2': DoSearchByTitle(books); break;
                    case '3': DoSearchByCategory(books); break;
                    case '4': selected = DoSelect(books); break;
                    case '5': DoRecommend(books, selected); break;
                    default: return;
                }
            }
        }

        private static Book DoSelect(Book[] books)
        {
            return Select.ByTitle(Console.WriteLine,
                Console.ReadLine,
                searchStr => Search.ByTitle(books, searchStr));
        }

        private static void DoRecommend(Book[] books, Book selected)
        {
            throw new NotImplementedException();
        }

        public static void DoSearchByTitle(IEnumerable<Book> books)
        {
            DoSearch(
                searchPrompt: "Search by book title or a part of it.", 
                searchFunc: searchTerm => Search.ByTitle(books, searchTerm)
                    .Select(b => BookMap.AuthorAndTitle(b)));
        }

        private static void DoSearchByCategory(IEnumerable<Book> books)
        {
            DoSearch("Search by book category or a part of it. \n" +
                "(for example: fic or Fiction or aut or bio or autobiography) \n" +
                "comma separated lists acceptable : juv, sci",
                searchTerm => books
                        .SearchByCategories(searchTerm.FromCommaSeparatedList())
                        .Select(b => BookMap.CategoryAuthorAndTitle(b))
                        .Highlight(searchTerm.FromCommaSeparatedList())
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
