using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Books.Core
{
    public class BooksCollection
    {
        public Book[] Books { get; set; }
    }

    public class Book
    {
        public static readonly Book Empty = new Book();

        public string[] categories;
        public string author { get; set; }
        public string country { get; set; }
        public string imageLink { get; set; }
        public string language { get; set; }
        public string link { get; set; }
        public int pages { get; set; }
        public string title { get; set; }
        public int year { get; set; }
    }

    public static class BookMap
    {
        public static string AuthorAndTitle(Book b)
        {
            return $"{b.author}: {b.title}";
        }

        public static string CategoryAuthorAndTitle(Book b)
        {
            var categories = string.Join(",", b.categories);
            return $"{AuthorAndTitle(b)} [{categories}]";
        }

        public static string CategoryAuthorTitleYear(Book b)
        {
            return $"{CategoryAuthorAndTitle(b)} published:{b.year}";
        }

        public static IEnumerable<string> ToAuthorTitleCategoriesYearString(this IEnumerable<Book> books)
        {
            return books.Select(CategoryAuthorTitleYear);
        }
    }
}
