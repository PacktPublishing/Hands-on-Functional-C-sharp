using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{
    public class SearchByCategory
    {
        public static IEnumerable<Book> Search(IEnumerable<Book> book, params string[] categories)
        {
            var categoriesToLower = MyClass.Lower(categories).ToArray();

            return book
                .Where(b =>
                {
                    var bookCategoriesToLow = MyClass.Lower(b.categories);
                    var match = bookCategoriesToLow.Any(c => categoriesToLower.Any(cat => c.Contains(cat)));

                    return match;
                });
        }
    }

    public static class MyClass
    {
        public static IEnumerable<string> Lower(this IEnumerable<string> strings)
        {
            return strings.Select(s => s.ToLowerInvariant());
        }
    }
}
