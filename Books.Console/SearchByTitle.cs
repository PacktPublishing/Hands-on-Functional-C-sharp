
using System;
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

        public static IEnumerable<Book> SuggestRandom(IEnumerable<Book> books, int count = 5)
        {
            return books
                // it is now explicit what the intent is - order the books randomly
                .OrderBy(Random)
                .Take(count);
        }

        private static Guid Random(Book _)
        {
            // and the implementation is right here should it need to be reviewed/updated etc.
            return Guid.NewGuid();
        }
    }
}