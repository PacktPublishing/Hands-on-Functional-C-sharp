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
            var outputs = new List<string>();
            var read = ReadFuncReturning();
            var search = SearchFuncReturning(new[] { new Book { title = "Test" } });
            const string expectedTitleFound = "Test";

            // act
            var selected = Select
                .ByTitle(outputs.Add, read, search);

            // assert 
            Assert.Equal(expectedTitleFound, selected.title);
        }

        [Fact]
        public void WhenASingleBookIsReturnedByTheSearchFunction_ShouldExpectTheFollowingUserInfo()
        {
            // arrange
            var read = ReadFuncReturning();
            var search = SearchFuncReturning(new[] { new Book { title = "Test" } });
            var outputs = new List<string>();
            var expectedOutputs = new[] {
                "Type title or part of it"
            };

            // act
            var selected = Select
                .ByTitle(outputs.Add, read, search);

            // assert 
            Assert.Equal(expectedOutputs, outputs);
        }

        [Fact]
        public void WhenZeroBooksFoundAndThenASingleBookIsFoundByTheSearchFunction_ShouldReturnThatBook()
        {
            // arrange
            var outputs = new List<string>();
            var read = ReadFuncReturning();
            var search = SearchFuncReturning(
                new Book[0],
                new[] { new Book { title = "Test" } });
            var expectedTitleFound = "Test";

            // act
            var selected = Select
                .ByTitle(outputs.Add, read, search);

            // assert 
            Assert.Equal(expectedTitleFound, selected.title);
        }

        [Fact]
        public void WhenZeroBooksFoundAndThenASingleBookIsFoundByTheSearchFunction_ShouldExpectUserInfo()
        {
            // arrange
            var read = ReadFuncReturning();
            var search = SearchFuncReturning(new Book[0], new[] { new Book { title = "Test" } });
            var outputs = new List<string>();
            var expectedOutputs = new[] {
                "Type title or part of it",
                "No books found by that criteria.",

                // second attempt
                "Type title or part of it",
            };

            // act
            var selected = Select
                .ByTitle(outputs.Add, read, search);

            // assert 
            Assert.Equal(expectedOutputs, outputs);
        }

        [Fact]
        public void WhenMultipleBooksFound_ShouldExpectUserInfo()
        {
            // arrange
            var outputs = new List<string>();
            // the second input actually matters so we'll input 1
            var read = ReadFuncReturning("", "1");
            var search = SearchFuncReturning(
                new[] { new Book { title = "Test" } }, new[] { new Book { title = "Test1" } });
            var expectedUserInfo = new[] {
                "Type title or part of it",
                "1 Test",
                "2 Test1"
            };

            // act
            var selected = Select
                .ByTitle(outputs.Add, read, search);

            // assert 
            Assert.Equal(expectedUserInfo, outputs);
        }

        [Fact]
        public void WhenMultipleBooksFound_ShouldReturnTheBookUserSelectedByNumber()
        {
            // arrange
            var outputs = new List<string>();
            // the second input actually matters so we'll input 1
            var read = ReadFuncReturning("", "1");
            var search = SearchFuncReturning(
                new[] { new Book { title = "Test" } }, new[] { new Book { title = "Test1" } });
            var expectedTitleFound = "Test";

            // act
            var selected = Select
                .ByTitle(outputs.Add, read, search);

            // assert 
            Assert.Equal(expectedTitleFound, selected.title);
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
