using Amazon.S3;
using BookStore.Application.Helpers;
using BookStore.Application.Services;
using BookStore.Domain.Interfaces;
using BookStore.Infrastructure.Context;
using BookStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<CreateBookService>();
builder.Services.AddScoped<ListBooksService>();
builder.Services.AddScoped<GetBookService>();
builder.Services.AddScoped<UpdateBookService>();
builder.Services.AddScoped<DeleteBookService>();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.Configure<S3>(
    builder.Configuration.GetSection("S3"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

// Migration automática
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao aplicar migration:");
        Console.WriteLine(ex.Message);
    }
}

app.Run();