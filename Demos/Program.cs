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

            var authorsLongNamesUppercased = from a in authors
                    where a.Length > 10
                    orderby a.Length descending
                    select a.ToUpper()
                    ;







            //IEnumerable<string> authors = new string[100];

            //var authorsShortNamesLowercased = authors
            //        .Where(a => a.Length <= 10)
            //        .OrderByDescending(a => a.Length)
            //        .Select(a => a.ToLower());

        }
    }
}
