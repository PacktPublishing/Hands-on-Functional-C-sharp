using System;
using System.Collections.Generic;
using System.Linq;

namespace Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] authors = new string[] {
                "Kurt Vonnegut",
                "Stephen King",
                "J.K. Rowling",
                "George R.R. Martin",
                "Dan Brown",
                "Mark Twain",
                "Homer"
            };

            var longNamedAuthors = FindLongNames(authors);
            var shortNamedAuthors = FindShortNamedAuthors(authors);

            PrintOut(authors, "All Authors");
            PrintOut(longNamedAuthors, "Long Named authors UPPERCASED");
            PrintOut(shortNamedAuthors, "Short Named authors lowercased");

            Console.ReadLine();
        }

        private static IEnumerable<string> FindLongNames(string[] authors)
        {
            var authorsLongNamesUppercased = from a in authors
                                             where a.Length > 10
                                             orderby a.Length descending
                                             select a.ToUpper();

            return authorsLongNamesUppercased;
        }

        private static IEnumerable<string> FindShortNamedAuthors(string[] authors)
        {
            var authorsShortNamedLowercased = authors
                .Where(a => a.Length <= 10)
                .OrderByDescending(a => a.Length)
                .Select(a => a.ToLower());

            return authorsShortNamedLowercased;
        }

        private static void PrintOut(IEnumerable<string> strings, string title)
        {
            Console.WriteLine(title);
            strings.ToList().ForEach(s => Console.WriteLine(s));
            Console.WriteLine();
        }
    }
}
