
using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{

    public class Search
    {
        public static IEnumerable<Book> ByTitle(IEnumerable<Book> books, string titlePartial)
        {
            var titlePartialLower = titlePartial.ToLower();
            
            return books
                .Where(b =>
                {
                    var titleLower = b.title.ToLower();
                    return titleLower.Contains(titlePartialLower);
                });
        }
    }
}