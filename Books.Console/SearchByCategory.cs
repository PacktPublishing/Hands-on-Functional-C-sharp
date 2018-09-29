using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{
    public static class SearchByCategory
    {
        public static IEnumerable<Book> SearchByCategories(this IEnumerable<Book> book, params string[] categories)
        {
            var categoriesToLower = categories.Lower().ToArray();

            return book
                .Where(b =>
                {
                    var bookCategoriesToLow = b.categories.Lower();
                    return bookCategoriesToLow.ContainsOneOf(categoriesToLower);
                });
        }
    }

    public static class MyClass
    {
        public static IEnumerable<string> Lower(this IEnumerable<string> strings)
        {
            return strings.Select(s => s.ToLowerInvariant());
        }

        public static bool ContainsOneOf(this IEnumerable<string> strings, IEnumerable<string> these)
        {
            return strings.Any(s => these.Any(t => s.Contains(t)));
        }
    }
}
