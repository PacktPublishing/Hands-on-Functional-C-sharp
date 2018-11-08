using Books.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Books.Infrastructure
{

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
            var rawJsonBooks = File.ReadAllText(booksJsonFile);

            return JsonConvert.DeserializeObject<Book[]>(rawJsonBooks);
        }
    }
}
