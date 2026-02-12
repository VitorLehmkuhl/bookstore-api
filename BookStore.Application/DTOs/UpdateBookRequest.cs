using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.DTOs
{
    public record UpdateBookRequest(string Title, string Author, int Year, string CoverImage);

}