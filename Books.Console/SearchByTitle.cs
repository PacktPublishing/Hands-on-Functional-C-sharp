
using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{

    public class Search
    {
        public static IEnumerable<Book> ByTitle(IEnumerable<Book> books, string titlePartial)
        {
            var titlePartialToLower = titlePartial.ToLower();
            
            return books
                .Where(b =>
                {
                    var authorToLower = b.author.ToLower();
                    return authorToLower.Contains(titlePartial);
                });
        }
    }
}