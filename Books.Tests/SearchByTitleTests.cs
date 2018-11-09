using Books.ConsoleApp;
using Books.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Books.Tests
{
    
    public class SearchByTitleTests
    {
        [Fact]
        public void WhenMatchingTitlesFound_ShouldReturnAllBooksWithMatchingTitles()
        {
            //arrange
            var books = new[] {
                new Book { title = "One match"},
                new Book { title = "Second not"},
                new Book { title = "Third match"},
            };

            IEnumerable<string> expectedTitlesMatched = new[] { "One match", "Third match" };

            //act
            var result = Search.ByTitle(books, "match");

            //assert
            Assert.Equal(expectedTitlesMatched, result.Select(b=> b.title));
        }

        [Fact]
        public void WhenMatchingTitlesWithDifferentCasing_ShouldReturnAllBooksWithMatchingTitlesIgnoringCase()
        {
            //arrange
            var books = new[] {
                new Book { title = "One Match"},
                new Book { title = "Second not"},
                new Book { title = "Third maTch"},
            };

            IEnumerable<string> expectedTitlesMatched = new[] { "One Match", "Third maTch" };

            //act
            var result = Search.ByTitle(books, "match");

            //assert
            Assert.Equal(expectedTitlesMatched, result.Select(b => b.title));
        }

        [Fact]
        public void WhenNoBooksMatch_ShouldReturnTheEmptyCollection()
        {
            //arrange
            var books = new[] {
                new Book { title = "One Match"},
                new Book { title = "Second not"},
                new Book { title = "Third maTch"},
            };

            var expectedTitlesMatchedLenght = 0;

            //act
            var result = Search.ByTitle(books, "Dinner");

            //assert
            Assert.Equal(expectedTitlesMatchedLenght, result.Count());
        }
    }
}
