using Books.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Books.Tests
{
    public class SelectBook
    {
        [Fact]
        public void SelectABook_WhenASingleBookIsReturnedByTheSearchFunction()
        {
            // arrange
            var outputs = new List<string>();              
            var read = SetupWriteFunc(new[] { "test", "mest" });

            // act
            var selected = Select
                .ByTitle(outputs.Add, read, s => new[] { new Book { title = "Test" } });

            // assert 
            Assert.Equal("Test", selected.title);
        }

        private Func<string> SetupWriteFunc(IEnumerable<string> inputs)
        {
            return () =>
            {
                var res = A.Head(inputs);
                inputs = res.tail;
                return res.head;
            };
        }
    }

    public static class A
    {
        public static (T head, IEnumerable<T> tail) Head<T>(IEnumerable<T> collection)
        {
            var empty = (default(T), default(IEnumerable<T>));
            if (collection == null)
            {
                return empty;
            }

            switch (collection.Count())
            {
                case 0: return empty;
                case 1: return (collection.First(), default(IEnumerable<T>));
                default: return (collection.First(), collection.Skip(1));
            }
        }
    }
}
