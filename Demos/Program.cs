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
            var books = new BooksJsonSource().Read();

            var huckFinn = books.First(b => b.title.Contains("Finn"));
            var rest = books.Where(b => b.title != huckFinn.title);

            huckFinn.categories = huckFinn.categories.Take(4).ToArray();

            Console.WriteLine(BookMap.CategoryAuthorAndTitle(huckFinn));
            Console.WriteLine("------------");

            Recommend
                .ByCategoryAndYear(rest, huckFinn.categories, 5)
                .ToAuthorTitleCategoriesYearString()
                .ToList()
                .ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
