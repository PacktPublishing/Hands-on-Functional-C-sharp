using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{
    public static class SearchByCategory
    {
        public static IEnumerable<Book> Search(this IEnumerable<Book> book, params string[] categories)
        {
            var categoriesToLower = categories.Lower().ToArray();

            return book
                .Where(b =>
                {
                    var bookCategoriesToLow = b.categories.Lower();
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
