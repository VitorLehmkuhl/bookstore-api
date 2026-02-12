using Xunit;
using Moq;
using FluentAssertions;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Application.Services;
using BookStore.Application.DTOs;

namespace BookStore.UnitTests.Services
{
    public class UpdateBookServiceTests
    {
        private readonly Mock<IBookRepository> _repositoryMock;
        private readonly UpdateBookService _bookService;

        public UpdateBookServiceTests()
        {
            _repositoryMock = new Mock<IBookRepository>();
            _bookService = new UpdateBookService(_repositoryMock.Object);
        }

        [Fact]
        public async Task Should_Update_Book_When_Exists()
        {
            var book = new Book("Old", "Author", 2000);

            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(book);

            var request = new UpdateBookRequest("New", "New Author", 2022);

            await _bookService.Execute(Guid.NewGuid(), request);

            _repositoryMock.Verify(r => r.UpdateAsync(book), Times.Once);
        }
    }
}