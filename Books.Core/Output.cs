using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Core
{
    public static class Output
    {
        public static void BooksByAuthor(IEnumerable<Book> books, Action<string> output)
        {
            var BooksByAuthorCatalog = new List<BooksByAuthor>();

            foreach(var book in books)
            {
                if (AuthorIsAlreadyCataloged(BooksByAuthorCatalog, book.author))
                {
                    // there are some(1 or more) books by this author already found and catalogued
                    var authorAndBooks = LocateAuthorAlreadyCataloged(BooksByAuthorCatalog, book.author);
                    authorAndBooks.Books.Add(book);
                }
                else
                {
                    BooksByAuthorCatalog.Add(CatalogueNewAuthor(book));
                }
            }

            // now we have an list that has all the authors catalogued
            OutputBooksByAuthor(BooksByAuthorCatalog, output);
        }

        private static bool AuthorIsAlreadyCataloged(List<BooksByAuthor> BooksByAuthorCatalog, string author)
        {
            return BooksByAuthorCatalog.Any(ba => ba.Author == author);
        }

        private static BooksByAuthor LocateAuthorAlreadyCataloged(List<BooksByAuthor> BooksByAuthorCatalog, string author)
        {
            return BooksByAuthorCatalog.First(ba => ba.Author == author);
        }

        private static BooksByAuthor CatalogueNewAuthor(Book b)
        {
            // there are NONE books by this author already found and cataloged

            var newBooksList = new List<Book> { b };
            return new BooksByAuthor(b.author, newBooksList);
        }


        private static void OutputBooksByAuthor(List<BooksByAuthor> BooksByAuthorCatalog, Action<string> output)
        {
            foreach(var ba in BooksByAuthorCatalog)
            {
                output(string.Format("Author: {0,-28} Books: ", ba.Author));
                foreach (var book in ba.Books)
                {
                    output(book.title + ", ");
                }
                output(Environment.NewLine);
            }
        }
    }

    public class BooksByAuthor
    {
        public readonly string Author;
        public readonly List<Book> Books;

        public BooksByAuthor(string author, List<Book> books)
        {
            Author = author;
            Books = books;
        }
    }
}
