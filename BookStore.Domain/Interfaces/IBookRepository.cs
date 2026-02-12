using BookStore.Domain.Entities;

namespace BookStore.Domain.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> ListAsync();
    Task<Book?> GetByIdAsync(Guid id);
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
}