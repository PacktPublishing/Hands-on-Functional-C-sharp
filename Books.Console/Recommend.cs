using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{
    public static class Recommend
    {
        public static IEnumerable<Book> ByCategoryAndYear(
            IEnumerable<Book> books, IEnumerable<string> categories, int count = 10)
        {
            return books
                .SortByMatchigCategoriesDescending(categories)
                // if same categories - order by year - newest first
                .ThenByDescending(b => b.year)
                .Take(count);
        }

        private static IOrderedEnumerable<Book> SortByMatchigCategoriesDescending(this IEnumerable<Book> books,
            IEnumerable<string> categories)
        {
            // order by most instersecting categories
            return books.OrderByDescending(b => b.categories.Intersect(categories).Count());
        }
    }
}
