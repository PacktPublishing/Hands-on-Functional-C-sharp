using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Books.ConsoleApp
{
    public static class Select
    {
        public static Book ByTitle(Action<string> write,
            Func<string> read,
            Func<string, IEnumerable<Book>> searchByTitle)
        {
            write("Type title or part of it");
            var searchCriteria = read();
            var booksMatched = searchByTitle(searchCriteria);

            switch (booksMatched.Count())
            {
                case 0:
                    write("No books found by that criteria.");
                    return ByTitle(write, read, searchByTitle);
                case 1:
                    return booksMatched.First();
                default:
                    return SelectOneOfBooksMatched(write, read, booksMatched);
            }
        }

        ///<summary>       
        /// Build a numbered representation of the matched books:
        /// 1 One Hundred Years of Solitude
        /// 2 Hunger
        /// 3 The Adventures of Huckleberry Finn
        /// And let the user choose one by typing in the number of the book they want
        ///</summary>
        private static Book SelectOneOfBooksMatched(Action<string> write,
            Func<string> read,
            IEnumerable<Book> booksMatched)
        {
            booksMatched
                .Zip(Enumerable.Range(1, booksMatched.Count()), (book, id) => new { id, book })
                .Aggregate(new StringBuilder(), (str, next) => str.AppendLine($"{next.id} {next.book.title}"))
                .ToString()
                .Invoke(write);

            return read()
                .TryParseInt()
                .Select(i => i != null
                    ? booksMatched.Skip(i.Value - 1).Take(1).FirstOrDefault() ?? Book.Empty
                    : Book.Empty
                );
        }
    }
    internal static class Extensions
    {
        internal static void Invoke(this string str, Action<string> action)
        {
            action(str);
        }

        internal static T Select<T>(this int? num, Func<int?, T> selector)
        {
            return selector(num);
        }

        internal static int? TryParseInt(this string str)
        {
            return int.TryParse(str, out int x) ? x : default(int?);
        }
    }

}
