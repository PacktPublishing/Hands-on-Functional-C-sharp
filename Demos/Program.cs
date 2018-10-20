using System;
using System.IO;
using System.Linq;
using Books.ConsoleApp;

namespace Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            var books = new BooksJsonSource(Path.Combine(Directory.GetCurrentDirectory(), "..", "Books.Console", "books.json"))
                .Read();

            var huckFinn = books.Where(b => b.title.Contains("Finn")).First();
            var rest = books.Where(b => b.title != huckFinn.title);

            huckFinn.categories = huckFinn.categories.Take(3).ToArray();

            System.Console.WriteLine(BookMap.CategoryAuthorAndTitle(huckFinn));
            System.Console.WriteLine("------------");

            Recommend
                .ByCategoryAndYear(rest, huckFinn.categories, 3)
                .Select(BookMap.CategoryAuthorAndTitle)
                .ToList()
                .ForEach(System.Console.WriteLine);
        }
    }
}
