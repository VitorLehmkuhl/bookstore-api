using BookStore.Application.DTOs;
using BookStore.Application.Helpers;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace BookStore.Application.Services
{
    public class CreateBookService
    {
        private readonly IBookRepository _repository;
        private readonly S3 _s3;

        public CreateBookService(IBookRepository repository, IOptions<S3> s3)
        {
            _repository = repository;
            _s3 = s3.Value;
        }

        public async Task<Guid> Execute(CreateBookRequest request)
        {
            var coverImage = await UploadImageToS3(request.CoverImage);
            var book = new Book(request.Title, request.Author, request.Year, coverImage);
            await _repository.AddAsync(book);
            return book.Id;
        }

        public async Task<string> UploadImageToS3(string image)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                if (!string.IsNullOrEmpty(image))
                {
                    string imageToUpload = image;
                    // Verifica se é uma URL de imagem para converter em base 64
                    // vamos salvar a imagens para o acaso da imagem original 
                    // ser apagada ou a url ficar indiponivel
                    if ((image.StartsWith("http://") || image.StartsWith("https://")) &&
                        (image.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                        image.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        image.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        image.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                        image.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                        image.EndsWith(".webp", StringComparison.OrdinalIgnoreCase)))
                    {
                        byte[] imageBytes = await httpClient.GetByteArrayAsync(image);
                        imageToUpload = Convert.ToBase64String(imageBytes);
                    }
                    // Upload para S3
                    var (code, fileName) = await S3Helper.UploadFileAsync(imageToUpload, _s3);
                    if (code == 500)
                        throw new Exception($"Erro ao subir imagem do produto: {fileName}");
                    return fileName;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update book: " + ex.Message);
            }
        }
    }
}