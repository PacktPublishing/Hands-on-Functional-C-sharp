using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Books.ConsoleApp
{
    public class Select
    {
        public static Book ByTitle(Action<string> write, Func<string> read, Func<string, IEnumerable<Book>> searchByTitle)
        {
            write("Type title or part of it");
            var searchCriteria = read();
            var booksMatched = searchByTitle(searchCriteria);
            var matches = booksMatched.Count();

            if (matches == 0)
            {
                write("No books found by that criteria.");
                return ByTitle(write, read, searchByTitle);
            }
            else if (matches == 1)
            {
                return booksMatched.First();
            }
            else
            {
                return SelectOneOfMetchedBooks(write, read, booksMatched, matches);
            }
        }

        private static Book SelectOneOfMetchedBooks(Action<string> write, Func<string> read, IEnumerable<Book> booksMatched, int matches)
        {
            var lookUp = booksMatched.Zip(Enumerable.Range(1, matches), (b, id) => new { id, b });
            var listBooksAndNumberThemForSelection = lookUp
                    .Aggregate(new StringBuilder(), (str, next) => str.AppendLine($"{next.id} {next.b.title}"))
                    .ToString();
            write(listBooksAndNumberThemForSelection);
            var idInput = read();

            int selectedId = 0;
            var parsedSuccessfully = int.TryParse(idInput, out selectedId);
            if (!parsedSuccessfully)
            {
                return Book.Empty;
            }
            else
            {
                return lookUp.FirstOrDefault(l => l.id == selectedId)?.b ?? Book.Empty;
            }
        }
    }
}
