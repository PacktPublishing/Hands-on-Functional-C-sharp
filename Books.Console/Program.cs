
#region imports
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion
namespace Books.ConsoleApp
{
    class Program
    {
        private static IBookPersist bookPersist = new BooksJsonPersist();

        // we will use these as local global state
        private static List<BooksByAuthor> BooksByAuthorCatalog;

        public static void Main()
        {
            Book[] booksAll = bookPersist.Read();

            // catalogue - we would need at most booksAll lenght for the catalog -
            // that would be the case that all books are by a different author
            BooksByAuthorCatalog = new List<BooksByAuthor>();

            // look at booksArray from the perspective of IEnumerable - it is still an array but we choose to
            // only expose the IEnumerable interface through the booksEnumerable
            IEnumerable<Book> booksEnumerable = booksAll;

            // the 'only' thing we can do with an enumerable is - get its enumerator
            foreach (var b in booksEnumerable)
            {
                var authorAlreadyCataloguedIndex = BooksByAuthorCatalog
                    .FindIndex(entry => entry.author == b.author);
                // if not such index if found then authorAlreadyCataloguedIndex would be -1
                var authorAlreadyCatalogued = authorAlreadyCataloguedIndex > -1;

                if (authorAlreadyCatalogued)
                {
                    AddNewTitleToAuthor(b, authorAlreadyCataloguedIndex);
                }
                else
                {
                    CatalogueNewAuthor(b);
                }
            }

            // now we have a list that has all the authors catalogued
            OutputBooksByAuthor();

            Console.WriteLine("Finished cataloguing authors. (press a key to exit...)");
            Console.ReadLine();
        }

        private static void OutputBooksByAuthor()
        {
            foreach (var ba in BooksByAuthorCatalog)
            {
                Console.Write("Author: {0,-28} Books: ", ba.author);
                foreach (var book in ba.books)
                {
                    Console.Write(book.title + ", ");
                }
                Console.Write(Environment.NewLine);
            }
        }

        private static void AddNewTitleToAuthor(Book b, int authorCatalogIndex)
        {
            var booksByThisAutorList = BooksByAuthorCatalog[authorCatalogIndex];
            // take the current books by this author and add the next
            booksByThisAutorList.books.Add(b);
        }

        private static void CatalogueNewAuthor(Book b)
        {
            // there are NONE books by this author already found and cataloged
            var newBookList = new List<Book> { b };
            var authorAndBooks = new BooksByAuthor(b.author, newBookList);
            // push that to the catalog
            BooksByAuthorCatalog.Add(authorAndBooks);
        }
    }

    public class BooksByAuthor
    {
        public readonly string author;
        public readonly List<Book> books;

        public BooksByAuthor(string author, List<Book> books)
        {
            this.author = author;
            this.books = books;
        }
    }
}
