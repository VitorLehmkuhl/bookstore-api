using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services
{
    public class DeleteBookService
    {
        private readonly IBookRepository _repository;

        public DeleteBookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                throw new Exception("Book not found");

            await _repository.DeleteAsync(book);
        }
    }
}