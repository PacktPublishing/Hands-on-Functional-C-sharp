using System;

namespace Books.Core
{
    public class Add
    {
        public static void Book(Func<string, string> prompt, Action<Book> add)
        {
            var newBook = new Book();
            newBook.title = prompt("Book title: ");
            newBook.author = prompt("Author: ");
            newBook.categories = prompt("Categories:")
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var yearMaybe = prompt("Year: ");
            int year;
            // basic validation
            while(!int.TryParse(yearMaybe, out year))
            {
                yearMaybe = prompt("Invalid year. Please try again to specify year:");
            }
            newBook.year = year;

            add(newBook);
        }
    }
}