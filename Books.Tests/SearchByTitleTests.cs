using Books.ConsoleApp;
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
        public void SearchByTitle_ShouldReturnAllBooksWithMatchingTitles()
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
        public void SearchByTitle_ShouldReturnAllBooksWithMatchingTitles_IgnoringCase()
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
    }
}
