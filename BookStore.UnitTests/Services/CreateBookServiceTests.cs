using Xunit;
using Moq;
using FluentAssertions;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Application.Services;
using BookStore.Application.DTOs;

namespace BookStore.UnitTests.Services
{
    public class CreateBookServiceTests
    {
        private readonly Mock<IBookRepository> _repositoryMock;
        private readonly CreateBookService _bookService;

        public CreateBookServiceTests()
        {
            _repositoryMock = new Mock<IBookRepository>();
            _bookService = new CreateBookService(_repositoryMock.Object);
        }

        [Fact]
        public async Task Should_Create_Book_And_Return_Id()
        {
            var request = new CreateBookRequest("DDD", "Evans", 2003);

            var id = await _bookService.Execute(request);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Once);

            id.Should().NotBeEmpty();
        }
    }
}