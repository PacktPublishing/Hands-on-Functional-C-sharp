using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Books.ConsoleApp
{
    public delegate Book BookSelect(Action<string> echo, Func<string, string> prompt, IEnumerable<Book> books);

    public class Select
    {
        public static BookSelect Prompt(Func<string, string> promptFunc)
        {
            return ByTitle;
            //            var selection = promptFunc(@"Select book by:
            //1 - Author
            //2 - Title");

            //            switch (selection)
            //            {
            //                case "1": return ByAuthor;
            //                case "2": return ByTitle;
            //            }

            //            return (_, __, ___) => Book.Empty;
        }

        private static Book ByTitle(Action<string> echo, Func<string, string> prompt, IEnumerable<Book> books)
        {
            var searchCriteria = prompt("Type title or part of it");
            var match = searchCriteria.ToLower();
            var booksMatched = books.Where(b => b.title.ToLower().Contains(match));
            var matches = booksMatched.Count();

            if (matches == 0)
            {
                echo("No books found by that criteria.");
                return ByTitle(echo, prompt, books);
            }
            else if (matches == 1)
            {
                return booksMatched.First();
            }
            else
            {
                var lookUp = booksMatched.Zip(Enumerable.Range(1, matches), (b, id) => new { id, b });
                var listBooksAndNumberThemForSelection = lookUp
                        .Aggregate(new StringBuilder(), (str, next) => str.AppendLine($"{next.id} {next.b.title}"))
                        .ToString();
                var idInput = prompt(listBooksAndNumberThemForSelection);

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

        private static Book ByAuthor(Action<string> echo, Func<string, string> prompt, IEnumerable<Book> books)
        {
            return Book.Empty;
        }
    }
}
