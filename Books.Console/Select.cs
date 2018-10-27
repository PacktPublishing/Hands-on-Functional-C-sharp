﻿using System;
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
                return SelectOneOfBooksMatched(write, read, booksMatched, matches);
            }
        }

        ///<summary>       
        /// Build a ordered numbered representation of the matched books:
        /// 1 Huckleberry Finn\n
        /// 2 The Three-Body Proble
        /// 3 Life
        /// And let the user choose one by typing in the number of books
        ///</summary>
        private static Book SelectOneOfBooksMatched(Action<string> write, Func<string> read, IEnumerable<Book> booksMatched, int matches)
        {
            // build a lookup to hold books and their number
            var lookUp = booksMatched.Zip(Enumerable.Range(1, matches), (b, id) => new { id, b });

            // build a string representation for the user
            var listBooksAndNumberThemForSelection = lookUp
                    .Aggregate(new StringBuilder(), (str, next) => str.AppendLine($"{next.id} {next.b.title}"))
                    .ToString();
            // prompt the user for input
            write(listBooksAndNumberThemForSelection);
            var idInput = read();

            // parse input and look-up the book by its number
            int selectedId = 0;
            var parsedSuccessfully = int.TryParse(idInput, out selectedId);
            if (!parsedSuccessfully)
            {
                // return the "Empty book" instead of null
                return Book.Empty;
            }
            else
            {
                var matchedBook = lookUp.FirstOrDefault(l => l.id == selectedId);
                // return the matched book or the "Empty book" if the number is too high
                return matchedBook?.b ?? Book.Empty;
            }
        }
    }
}
