
#region imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion
namespace Books.ConsoleApp
{
    class Program
    {
        private static IBooksSource BooksSource = new BooksJsonSource();

        // we will use this as global state
        private static List<BooksByAuthor> BooksByAuthorCatalog;

        public static void Main()
        {
            //Book[] books = BooksSource.Read();
            IEnumerable<Book> books = BooksSource.Read();

            BooksByAuthorCatalog = new List<BooksByAuthor>();
            
            //for (var i = 0; i < books.Length; i++)
            //{
            //    var book = books[i];
            IEnumerator<Book> booksEnumerator = books.GetEnumerator();
            while (booksEnumerator.MoveNext())
            {
                var book = booksEnumerator.Current;

                if (AuthorIsAlreadyCataloged(book.author))
                {
                    // there are some(1 or more) books by this author already found and catalogued
                    var authorCatalogIndex = LocateAuthorAlreadyCataloged(book.author);

                    var existingBooks = BooksByAuthorCatalog[authorCatalogIndex].Books;
                    existingBooks.Add(book);
                }
                else
                {
                    CatalogueNewAuthor(book);
                }
            }

            // now we have an list that has all the authors catalogued
            OutputBooksByAuthor();

            Console.WriteLine("Finished cataloguing authors. (press a key to exit...)");
            Console.ReadLine();
        }

        private static bool AuthorIsAlreadyCataloged(string author)
        {
            var authorAlreadyCatalogued = false;

            // we'll iterate over the cataloge to find the author - if author's already been cataloged
            for (int j = 0; j < BooksByAuthorCatalog.Count; j++)
            {
                var entry = BooksByAuthorCatalog[j];
                if (entry.Author == author)
                {
                    authorAlreadyCatalogued = true;
                    break;
                }
            }

            return authorAlreadyCatalogued;
        }

        private static int LocateAuthorAlreadyCataloged(string author)
        {
            var authorCatalogIndex = 0;

            // we'll iterate over the cataloge to find the author's index
            for (int j = 0; j < BooksByAuthorCatalog.Count; j++)
            {
                var entry = BooksByAuthorCatalog[j];
                if (entry.Author == author)
                {
                    authorCatalogIndex = j;
                    break;
                }
            }

            return authorCatalogIndex;
        }

        private static void CatalogueNewAuthor(Book b)
        {
            // there are NONE books by this author already found and cataloged

            var newBooksList = new List<Book> { b };
            var authorAndBooks = new BooksByAuthor(b.author, newBooksList);

            BooksByAuthorCatalog.Add(authorAndBooks);
        }


        private static void OutputBooksByAuthor()
        {
            for (int i = 0; i < BooksByAuthorCatalog.Count; i++)
            {
                BooksByAuthor ba = BooksByAuthorCatalog[i];
                Console.Write("Author: {0,-28} Books: ", ba.Author);
                for (int j = 0; j < ba.Books.Count; j++)
                {
                    Console.Write(ba.Books[j].title + ", ");
                }
                Console.Write(Environment.NewLine);
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
