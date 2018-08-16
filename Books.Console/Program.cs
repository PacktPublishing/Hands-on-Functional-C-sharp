
#region imports
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion
namespace Books.ConsoleApp
{
    class Program
    {
        private static IBooksSource BooksSource = new BooksJsonSource();

        // we will use these as local global state
        private static List<BooksByAuthor> BooksByAuthorCatalog;
        private static int catalogNextEntryIndex;
        public static void Main()
        {
            Book[] books = BooksSource.Read();

            // catalogue - we would need at most books.lenght for the catalog -
            // that would be the case that all books are by a different author
            BooksByAuthorCatalog = new List<BooksByAuthor>();
            
            for (int i = 0; i < books.Length; i++)
            {
                var book = books[i];

                if (AuthorIsAlreadyCataloged(book))
                {
                    // there are some(1 or more) books by this author already found and catalogued
                    var authorCatalogIndex = LocateAuthorAlreadyCataloged(book);
                    AddNewTitleToAuthor(book, authorCatalogIndex);
                }
                else
                {
                    CatalogueNewAuthor(book);
                }
            }

            // now we have an array that has all the authors catalogued

            OutputBooksByAuthor();

            Console.WriteLine("Finished cataloguing authors. (press a key to exit...)");
            Console.ReadLine();
        }

        private static void OutputBooksByAuthor()
        {
            for (int i = 0; i < BooksByAuthorCatalog.Count; i++)
            {
                BooksByAuthor ba = BooksByAuthorCatalog[i];
                Console.Write("Author: {0,-28} Books: ", ba.author);
                for (int j = 0; j < ba.books.Length; j++)
                {
                    Console.Write(ba.books[j].title + ", ");
                }
                Console.Write(Environment.NewLine);
            }
        }

        private static void AddNewTitleToAuthor(Book b, int authorCatalogIndex)
        {
            // take the current books by this author
            Book[] books = BooksByAuthorCatalog[authorCatalogIndex].books;
            // create a new array with lenght greater with 1
            var newBooksArray = new Book[books.Length + 1];
            // copy the books in the new array
            for (int j = 0; j < books.Length; j++)
            {
                newBooksArray[j] = books[j];
            }// what would j be here

            // copy the current book in the last spot
            newBooksArray[newBooksArray.Length - 1] = b;

            var booksByThisAuthorUpdated = new BooksByAuthor(b.author, newBooksArray);
            BooksByAuthorCatalog[authorCatalogIndex] = booksByThisAuthorUpdated;
        }

        private static void CatalogueNewAuthor(Book b)
        {
            // there are NONE books by this author already found and cataloged

            // create a new array with lenght 1
            var newBooksArray = new Book[1];
            // put the book we just found in it
            newBooksArray[0] = b;
            var authorAndBooks = new BooksByAuthor(b.author, newBooksArray);
            // push that to the catalog
            BooksByAuthorCatalog.Add(authorAndBooks);
        }

        private static bool AuthorIsAlreadyCataloged(Book b)
        {
            var authorAlreadyCatalogued = false;

            // we'll iterate over the cataloge to find the author - if author's already been cataloged
            for (int j = 0; j < BooksByAuthorCatalog.Count; j++)
            {
                var entry = BooksByAuthorCatalog[j];
                if (entry != null && entry.author == b.author)
                {
                    authorAlreadyCatalogued = true;
                    break;
                }
            }

            return authorAlreadyCatalogued;
        }

        private static int LocateAuthorAlreadyCataloged(Book b)
        {
            var authorCatalogIndex = 0;

            // we'll iterate over the cataloge to find the author's index
            for (int j = 0; j < BooksByAuthorCatalog.Count; j++)
            {
                var entry = BooksByAuthorCatalog[j];
                if (entry != null && entry.author == b.author)
                {
                    authorCatalogIndex = j;
                    break;
                }
            }

            return authorCatalogIndex;
        }
    }

    public class BooksByAuthor
    {
        public readonly string author;
        public readonly Book[] books;

        public BooksByAuthor(string author, Book[] books)
        {
            this.author = author;
            this.books = books;
        }
    }
}
