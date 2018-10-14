using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Books.ConsoleApp;

namespace Demos
{
    public class Pure
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }

    public class PureIsh
    {
        public Func<IEnumerable<Book>> ReadBooks { get; }
        public PureIsh(Func<IEnumerable<Book>> readBooks)
        {
            this.ReadBooks = readBooks;
        }

        public Book LendBook(string title)
        {
            var books = ReadBooks();
            return books.FirstOrDefault(b => b.title.Contains(title));
        }
    }

}
