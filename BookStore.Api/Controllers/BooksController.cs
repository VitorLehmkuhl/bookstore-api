using BookStore.Application.DTOs;
using BookStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] ListBooksService bookService)
        => Ok(await bookService.Execute());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, [FromServices] GetBookService bookService)
    {
        var book = await bookService.Execute(id);
        return book == null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromBody] CreateBookRequest request, 
        [FromServices] CreateBookService bookService)
    {
        var id = await bookService.Execute(request);
        return CreatedAtAction(nameof(GetById), new { id }, request);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id,
        [FromBody] UpdateBookRequest request,
        [FromServices] UpdateBookService bookService)
    {
        await bookService.Execute(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id,
        [FromServices] DeleteBookService bookService)
    {
        await bookService.Execute(id);
        return NoContent();
    }
}