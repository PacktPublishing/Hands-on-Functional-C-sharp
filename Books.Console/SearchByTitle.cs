
using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{

    public class Search
    {
        public static IEnumerable<Book> ByTitle(IEnumerable<Book> books, string titlePartial)
        {
            var titlePartialLowercased = titlePartial.ToLower();
            
            return books
                .Where(b =>
                {
                    var bookTitleLowercased = b.title.ToLower();
                    return bookTitleLowercased.Contains(titlePartialLowercased);
                });
        }
    }
}