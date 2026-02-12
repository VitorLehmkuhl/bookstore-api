using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services
{
    public class ListBooksService
    {
        private readonly IBookRepository _repository;

        public ListBooksService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> Execute()
            => await _repository.ListAsync();
    }
}