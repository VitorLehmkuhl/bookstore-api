using Xunit;
using Moq;
using FluentAssertions;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Application.Services;

namespace BookStore.UnitTests.Services
{
    public class DeleteBookServiceTests
    {
        private readonly Mock<IBookRepository> _repositoryMock;
        private readonly DeleteBookService _bookService;

        public DeleteBookServiceTests()
        {
            _repositoryMock = new Mock<IBookRepository>();
            _bookService = new DeleteBookService(_repositoryMock.Object);
        }

        [Fact]
        public async Task Should_Delete_Book_When_Exists()
        {
            var book = new Book("Test", "Author", 2020);

            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(book);

            await _bookService.Execute(Guid.NewGuid());

            _repositoryMock.Verify(r => r.DeleteAsync(book), Times.Once);
        }
    }
}