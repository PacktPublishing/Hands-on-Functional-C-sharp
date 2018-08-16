
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

        // we will use this as global state
        private static List<BooksByAuthor> BooksByAuthorCatalog;

        public static void Main()
        {
            Book[] books = BooksSource.Read();
            
            BooksByAuthorCatalog = new List<BooksByAuthor>();
            
            for (int i = 0; i < books.Length; i++)
            {
                var book = books[i];

                if (AuthorIsAlreadyCataloged(book))
                {
                    // there are some(1 or more) books by this author already found and catalogued
                    var authorCatalogIndex = LocateAuthorAlreadyCataloged(book);

                    BooksByAuthorCatalog[authorCatalogIndex].Books.Add(book);
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

       

        private static void CatalogueNewAuthor(Book b)
        {
            // there are NONE books by this author already found and cataloged
           
            var newBooksList = new List<Book> { b };
            var authorAndBooks = new BooksByAuthor(b.author, newBooksList);
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
                if (entry != null && entry.Author == b.author)
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
                if (entry != null && entry.Author == b.author)
                {
                    authorCatalogIndex = j;
                    break;
                }
            }

            return authorCatalogIndex;
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
