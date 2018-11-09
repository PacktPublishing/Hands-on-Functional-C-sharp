using Books.Core;
using Books.Infrastructure;
using System;
using System.Collections.Generic;

namespace Books.ConsoleApp
{
    public interface IReadWrite
    {
        string Read();
        void Write(string output);
    }

    public class SelectWithDI
    {
        private readonly IBooksSource bookSource;
        private readonly Action<string> write;
        private readonly Func<string> read;

        public SelectWithDI(IBooksSource bookSource, IReadWrite readWrite)
        {
            this.bookSource = bookSource;
            this.write = readWrite.Write;
            this.read = readWrite.Read;
        }

        public Book DoSelect()
        {
            var books = bookSource.Read();

            Func<string, IEnumerable<Book>> searchByTitle = 
                s => Search.ByTitle(books, s);

            return Select.ByTitle(write, read, searchByTitle);
        }
    }


    public class DependencyInjection
    {
        public static void Setup(dynamic container)
        {
            container.Register<IBooksSource>(new BooksJsonSource());
            
            container.Register<IReadWrite>(new ReadWrite());
        }
    }
}
