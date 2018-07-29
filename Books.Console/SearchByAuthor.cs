using System;
using System.Collections.Generic;
using System.Linq;

namespace Books.ConsoleApp
{
    // a helper type to avoid the long and non-descriptive type Dictionary ...
    // this type has a name that suggests what it is - a catalogue by author
    using AuthorCatalogue = IDictionary<string, IEnumerable<Book>>;

    public static class SearchByAuthor
    {
        public static IEnumerable<string> MatchAutors(IEnumerable<Book> inputBooks, 
            string searchInput)
        {
            return inputBooks
                .ExtractAuthors()
                .ListByPartOfName(searchInput);
        }

        public static IEnumerable<string> SuggestAuthors(IEnumerable<Book> books, 
            int suggestionLength = 5)
        {
            return books
                 .ExtractAuthors()
                 .OrderBy(_ => Guid.NewGuid())
                 .Take(suggestionLength);
        }

        public static IEnumerable<Book> FindBooks(IEnumerable<Book> inputBooks, string author)
        {
            return inputBooks
                .CatalogueByAuthor()
                .Search(author);
        }
    }

    internal static class SearchByAuthorBooksLinqExtensions
    {
        public static AuthorCatalogue CatalogueByAuthor(this IEnumerable<Book> books)
        {
           return books
                .ToLookup(b => b.author)
                .ToDictionary(authorGroup => authorGroup.Key, authorGroup => authorGroup as IEnumerable<Book>);
        }

        public static IEnumerable<Book> Search(this AuthorCatalogue catalogue, string author)
        {
            return catalogue.ContainsKey(author)
                ? catalogue[author]
                : Enumerable.Empty<Book>();
        }

        public static IEnumerable<string> ExtractAuthors(this IEnumerable<Book> books)
        {
            return books.Select(b => b.author).Distinct();
        }

        public static IEnumerable<string> ListByPartOfName(this IEnumerable<string> authors, string partOfName)
        {
            var partOfNameLower = partOfName.ToLower();
            return authors.Select(a => a).Where(a => a.ToLower().Contains(partOfNameLower));
        }
    }
}
