using Books.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Books.Tests
{
    public class SelectBookTests
    {
        [Fact]
        public void WhenASingleBookIsReturnedByTheSearchFunction_ShouldReturnThatBook()
        {
            // arrange
           

            // act
            //var selected = Select.ByTitle(outputs.Add, read, search);

            // assert 
            //Assert.Equal(expectedTitleFound, selected.title);
        }

        [Fact]
        public void WhenASingleBookIsReturnedByTheSearchFunction_ShouldExpectTheFollowingUserInfo()
        {
            // arrange

            // act
            //var selected = Select.ByTitle(outputs.Add, read, search);

            // assert 
            //Assert.Equal(expectedOutputs, outputs);
        }

        private Func<string> ReadFuncReturning(params string[] inputs)
        {
            return () =>
            {
                var (head, tail) = Head(inputs);
                inputs = tail?.ToArray();
                return head;
            };
        }

        private Func<string, IEnumerable<Book>> SearchFuncReturning(params IEnumerable<Book>[] booksToReturn)
        {
            return s =>
            {
                var (head, tail) = Head(booksToReturn);
                booksToReturn = tail?.ToArray();
                return head;
            };
        }

        private (T head, IEnumerable<T> tail) Head<T>(IEnumerable<T> collection)
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
