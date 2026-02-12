using BookStore.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace BookStore.UnitTests.Domain
{
    public class BookTests
    {
        [Fact]
        public void Should_Create_Book_With_Valid_Data()
        {
            var book = new Book("Clean Code", "Robert Martin", 2008);

            book.Title.Should().Be("Clean Code");
            book.Author.Should().Be("Robert Martin");
            book.Year.Should().Be(2008);
        }

        [Fact]
        public void Should_Throw_When_Title_Is_Invalid()
        {
            Action act = () => new Book("", "Author", 2020);

            act.Should().Throw<ArgumentException>()
            .WithMessage("Title cannot be empty");
        }

        [Fact]
        public void Should_Update_Book()
        {
            var book = new Book("Old", "Author", 2000);

            book.Update("New", "New Author", 2020);

            book.Title.Should().Be("New");
            book.Author.Should().Be("New Author");
            book.Year.Should().Be(2020);
        }
    }
}