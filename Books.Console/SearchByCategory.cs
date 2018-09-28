using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    var numberOfCategoriesIntersecting = bookCategoriesToLow.Intersect(categoriesToLower).Count();
                    return numberOfCategoriesIntersecting > 0;
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
