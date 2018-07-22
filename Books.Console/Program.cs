
#region imports
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
#endregion
namespace Books.ConsoleApp
{
    class Program
    {
        private static IBookPersist bookPersist = new BooksJsonPersist();

        // we will use these as local global state
        private static BooksByAuthor[] BooksByAuthorCatalog;
        private static int catalogNextEntryIndex;
        public static void Main()
        {
            Book[] booksAll = bookPersist.Read();

            // catalogue - we would need at most booksAll lenght for the catalog -
            // that would be the case that all books are by a different author 
            BooksByAuthorCatalog = new BooksByAuthor[booksAll.Length];
            catalogNextEntryIndex = 0;

            for (int i = 0; i < booksAll.Length; i++)
            {
                var b = booksAll[i];

                if (!IsAuthorAlreadyCataloged(b))
                {
                    CatalogueNewAuthor(catalogNextEntryIndex, b);
                    // update the next entry index - we want to know which is the next open spot in the catalog
                    catalogNextEntryIndex += 1;
                }
                else
                {
                    var authorCatalogIndex = LocateAuthorAlreadyCataloged(b);
                    // there are some(1 or more) books by this author already found and catalogued
                    AddNewTitleToAuthor(b, authorCatalogIndex);
                }
            }

            // finally we'll remove all extra entities 
            var finalBooksByAuthorCatalog = new BooksByAuthor[catalogNextEntryIndex - 1];
            for (int i = 0; i < finalBooksByAuthorCatalog.Length; i++)
            {
                finalBooksByAuthorCatalog[i] = BooksByAuthorCatalog[i];
            }
            // update the catalog global state - so we can use it below
            BooksByAuthorCatalog = finalBooksByAuthorCatalog;
            // now we have an array that has all the authors catalogued

            OutputBooksByAuthor();

            Console.WriteLine("Finished cataloguing authors. (press a key to exit...)");
            Console.ReadLine();
        }

        private static void OutputBooksByAuthor()
        {
            for (int i = 0; i < BooksByAuthorCatalog.Length; i++)
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
            }                    // what would j be here

            // copy the current book in the last spot 
            newBooksArray[newBooksArray.Length - 1] = b;

            var booksByThisAuthorUpdated = new BooksByAuthor(b.author, newBooksArray);
            BooksByAuthorCatalog[authorCatalogIndex] = booksByThisAuthorUpdated;
        }

        private static void CatalogueNewAuthor(int catalogNextEntryIndex, Book b)
        {
            // there are no book by this author already found and cataloged

            // create a new array with lenght 1
            var newBooksArray = new Book[1];
            // put the book we just found in it
            newBooksArray[0] = b;
            var authorAndBooks = new BooksByAuthor(b.author, newBooksArray);
            // push that to the catalog
            BooksByAuthorCatalog[catalogNextEntryIndex] = authorAndBooks;
        }

        private static bool IsAuthorAlreadyCataloged(Book b)
        {
            var authorAlreadyCatalogued = false;

            // we'll iterate over the cataloge to find the author - if he's/she's already been cataloged
            for (int j = 0; j < BooksByAuthorCatalog.Length; j++)
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
            for (int j = 0; j < BooksByAuthorCatalog.Length; j++)
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

    // do we use this?
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

    #region used for one-time enriching the books - old main

    class ProgramOld
    {
        //static void OldMain(string[] args)
        //{
        //    var file = System.IO.File.ReadAllText("books1.json");
        //    //var books = Newtonsoft.Json.JsonConvert.DeserializeObject<Book[]>(file).Reverse().ToArray();

        //    //var booksTitleAndAuthorCommaSeparated = books
        //    //    .Select(b => b.title + " " + b.author)
        //    //    .Aggregate(
        //    //        // seed
        //    //        new { current = 0, queries = new List<string> { string.Empty } },
        //    //        // accumulator function
        //    //        (acc, next) =>
        //    //        {
        //    //            acc.queries[acc.current] = string.Join(",", acc.queries[acc.current], next);
        //    //            var current = acc.queries[acc.current].Length > 500 ? acc.current + 1 : acc.current;
        //    //            if(current > acc.current)
        //    //            {
        //    //                acc.queries.Add(string.Empty);
        //    //            }

        //    //            return new { current, acc.queries };
        //    //        },
        //    //        // result selector
        //    //        r => r.queries);

        //    //Console.WriteLine($"Total queries is {booksTitleAndAuthorCommaSeparated.Count}");
        //    //Console.ReadLine();
        //    //Dictionary<string, string[]> categoriesDistinctByTitleFromGoogle;

        //    using (var client = new System.Net.Http.HttpClient())
        //    {
        //        //categoriesDistinctByTitleFromGoogle = booksTitleAndAuthorCommaSeparated.SelectMany(query =>                {

        //        //    var googleBooks = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(res);

        //        //    return googleBooks.items
        //        //        .Where(item => item != null && item.volumeInfo != null && item.volumeInfo.categories != null)
        //        //        .Select(item => new { item.volumeInfo.title, item.volumeInfo.categories })
        //        //        .ToLookup(x => x.title)
        //        //        .Select(group => new { title = group.Key, categoriesDistinct = group.SelectMany(g => g.categories).Distinct().ToArray() })
        //        //        .ToDictionary(x => x.title, x => x.categoriesDistinct);
        //        //})
        //        //.ToDictionary(x => x.Key, x => x.Value);

        //        for (int i = 0; i < books.Length; i++)
        //        {
        //            if (books[i].categories == null)
        //            {
        //                var url = $"https://www.googleapis.com/books/v1/volumes?q={books[i].title}";
        //                var res = client.GetStringAsync(url).Result;
        //                var googleBooks = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(res);
        //                var book = books[i];

        //                var categories = googleBooks.items
        //                    .Where(item => item.volumeInfo != null && item.volumeInfo.categories != null)
        //                    .SelectMany(item => item.volumeInfo.categories)
        //                    .Distinct()
        //                    .ToArray();

        //                book.categories = categories;

        //                // update the file with up to as many as we've downloaded
        //                System.IO.File.WriteAllText("books1.json", Newtonsoft.Json.JsonConvert.SerializeObject(books, Newtonsoft.Json.Formatting.Indented));
        //                Console.WriteLine($"{book.title} : {string.Join(",", book.categories)}");

        //                Task.Delay(30 * 1000).Wait();
        //            }
        //        }
        //    }

        //    //books.ToList().ForEach(b => b.categories = categoriesDistinctByTitleFromGoogle.GetValueOrDefault(b.title));


        //    Console.WriteLine("Titles of books printed. Press key to end....");
        //    Console.ReadLine();
        //}
    }


    public class Rootobject
    {
        public string kind { get; set; }
        public int totalItems { get; set; }
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public Volumeinfo volumeInfo { get; set; }
        public Saleinfo saleInfo { get; set; }
        public Accessinfo accessInfo { get; set; }
        public Searchinfo searchInfo { get; set; }
    }

    public class Volumeinfo
    {
        public string title { get; set; }
        public string[] authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }
        public Industryidentifier[] industryIdentifiers { get; set; }
        public Readingmodes readingModes { get; set; }
        public int pageCount { get; set; }
        public string printType { get; set; }
        public string[] categories { get; set; }
        public string maturityRating { get; set; }
        public bool allowAnonLogging { get; set; }
        public string contentVersion { get; set; }
        public Panelizationsummary panelizationSummary { get; set; }
        public Imagelinks imageLinks { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
        public string description { get; set; }
        public string subtitle { get; set; }
        public float averageRating { get; set; }
        public int ratingsCount { get; set; }
    }

    public class Readingmodes
    {
        public bool text { get; set; }
        public bool image { get; set; }
    }

    public class Panelizationsummary
    {
        public bool containsEpubBubbles { get; set; }
        public bool containsImageBubbles { get; set; }
    }

    public class Imagelinks
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
    }

    public class Industryidentifier
    {
        public string type { get; set; }
        public string identifier { get; set; }
    }

    public class Saleinfo
    {
        public string country { get; set; }
        public string saleability { get; set; }
        public bool isEbook { get; set; }
        public Listprice listPrice { get; set; }
        public Retailprice retailPrice { get; set; }
        public string buyLink { get; set; }
        public Offer[] offers { get; set; }
    }

    public class Listprice
    {
        public float amount { get; set; }
        public string currencyCode { get; set; }
    }

    public class Retailprice
    {
        public float amount { get; set; }
        public string currencyCode { get; set; }
    }

    public class Offer
    {
        public int finskyOfferType { get; set; }
        public Listprice1 listPrice { get; set; }
        public Retailprice1 retailPrice { get; set; }
        public bool giftable { get; set; }
    }

    public class Listprice1
    {
        public float amountInMicros { get; set; }
        public string currencyCode { get; set; }
    }

    public class Retailprice1
    {
        public float amountInMicros { get; set; }
        public string currencyCode { get; set; }
    }

    public class Accessinfo
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public bool embeddable { get; set; }
        public bool publicDomain { get; set; }
        public string textToSpeechPermission { get; set; }
        public Epub epub { get; set; }
        public Pdf pdf { get; set; }
        public string webReaderLink { get; set; }
        public string accessViewStatus { get; set; }
        public bool quoteSharingAllowed { get; set; }
    }

    public class Epub
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class Pdf
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    public class Searchinfo
    {
        public string textSnippet { get; set; }
    }
    #endregion

}
