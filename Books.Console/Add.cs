using System;

namespace Books.ConsoleApp
{
    public class Add
    {
        public static void Book(Func<string, string> Prompt, Action<Book> Persist)
        {
            var newBook = new Book();

            newBook.title = Prompt("Book title: ");
            newBook.author = Prompt("Author: ");
            newBook.categories = Prompt("Categories:")
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var yearMaybe = Prompt("Year: ");
            int year;
            // basic validation
            while (!int.TryParse(yearMaybe, out year))
            {
                yearMaybe = Prompt("Invalid year. Please try to specify year again:");
            }
            newBook.year = year;

            Persist(newBook);
        }
    }
}