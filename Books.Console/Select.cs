using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Books.ConsoleApp
{
    public class Select
    {
        public static Book ByTitle(Action<string> say, Func<string> hear, IEnumerable<Book> books)
        {
            say("Type title or part of it");
            var searchCriteria = hear();
            var match = searchCriteria.ToLower();
            var booksMatched = books.Where(b => b.title.ToLower().Contains(match));
            var matches = booksMatched.Count();

            if (matches == 0)
            {
                say("No books found by that criteria.");
                return ByTitle(say, hear, books);
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
                say(listBooksAndNumberThemForSelection);
                var idInput = hear();

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
}
