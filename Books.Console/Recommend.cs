using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{
    public class Recommend
    {
        public static IEnumerable<Book> ByCategoryAndYear(
            IEnumerable<Book> books, IEnumerable<string> categories, int count = 10)
        {
            return books
                // similar in category
                // order by most instersecting categories
                .OrderByDescending(b => b.categories.Intersect(categories).Count())
                // if same categories - order by year - newest first
                .ThenByDescending(b => b.year)
                .Take(count);
        }
    }
}
