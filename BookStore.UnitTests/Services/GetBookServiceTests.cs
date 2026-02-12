using Xunit;
using Moq;
using FluentAssertions;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Application.Services;

namespace BookStore.UnitTests.Services
{
    public class GetBookServiceTests
    {
        private readonly Mock<IBookRepository> _repositoryMock;
        private readonly GetBookService _bookService;

        public GetBookServiceTests()
        {
            _repositoryMock = new Mock<IBookRepository>();
            _bookService = new GetBookService(_repositoryMock.Object);
        }

        [Fact]
        public async Task Should_Return_Book_When_Found()
        {
            var book = new Book("Test", "Author", 2020);

            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(book);

            var result = await _bookService.Execute(Guid.NewGuid());

            result.Should().NotBeNull();
        }
    }
}