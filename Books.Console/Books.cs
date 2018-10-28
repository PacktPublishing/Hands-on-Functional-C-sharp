using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Books.ConsoleApp
{
    public class BooksCollection
    {
        public Book[] Books { get; set; }
    }

    public class Book
    {
        internal static readonly Book Empty = new Book();

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
            var categories = string.Join(',', b.categories);
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

    public interface IBooksSource
    {
        Book[] Read();
        void Delete(Book book);
        void Add(Book book);
    }

    public class BooksJsonSource : IBooksSource
    {
        private string booksJsonFile;

        public BooksJsonSource(string booksFile = "books.json")
        {
            booksJsonFile = booksFile;
        }

        public void Add(Book book)
        {
            var books = Read();
            var plusAdded = books.Concat(new[] { book });
            writeBooksToFile(plusAdded);
        }

        public void Delete(Book book)
        {
            var books = Read();
            var removed = books.Where(b => b.title != book.title && b.author != book.author);
            writeBooksToFile(removed);
        }

        private void writeBooksToFile(IEnumerable<Book> books)
        {
            var stringified = JsonConvert.SerializeObject(books, Formatting.Indented);
            File.WriteAllText(booksJsonFile, stringified);
        }

        public Book[] Read()
        {
            var rawJsonBooks = File.ReadAllTextAsync(booksJsonFile)
                // this (the .Result)is blocking the "UI"/"Main" thread - done for simplicity
                // but utterly and very and fundamentaly wrong for production apps!
                .Result;

            return JsonConvert.DeserializeObject<Book[]>(rawJsonBooks);
        }
    }
}
