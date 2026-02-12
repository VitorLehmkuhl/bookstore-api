using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services
{
    public class GetBookService
    {
        private readonly IBookRepository _repository;

        public GetBookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<Book?> Execute(Guid id)
            => await _repository.GetByIdAsync(id);
    }
}