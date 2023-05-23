using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalBookApi.Data;
using MinimalBookApi.Models;

namespace MinimalBookApi.Endpoints
{
	public class BookEndpoints
	{
        public BookEndpoints(){}

		public void MapEndpoints(WebApplication webApplication)
		{
			webApplication.MapGet("/books", async (DataContext dataContext) =>
			{
				return await dataContext.Books.ToListAsync();
			});

			webApplication.MapGet("/book/{id}", async (int id, DataContext dataContext) =>
			{
				return await dataContext.Books.FindAsync(id) is Book book ? Results.Ok(book) : Results.NotFound("Sorry, this book doesn't exist. :(");
			});

			webApplication.MapPost("/book", async (Book book, DataContext dataContext) =>
			{
				dataContext.Books.Add(book);
				await dataContext.SaveChangesAsync();
				return Results.Ok( await dataContext.Books.ToListAsync());
			});

			webApplication.MapPut("/book/{id}", async (int id, Book updatedBook, DataContext dataContext) =>
			{
				var book = await dataContext.Books.FindAsync(id);
				if (book is null)
				{
					return Results.NotFound("Sorry, this book doesn't exist. :(");
				}
				else
				{
					book.Title = updatedBook.Title;
					book.Author = updatedBook.Author;
					await dataContext.SaveChangesAsync();
					return Results.Ok(await dataContext.Books.ToListAsync());
				}
			});

			webApplication.MapDelete("/book/{id}", async (int id, DataContext dataContext) =>
			{
				var book = await dataContext.Books.FindAsync(id);

				if (book is null)
				{
					return Results.NotFound("Sorry, this book doesn't exist. :(");
				}
				else
				{
					dataContext.Books.Remove(book);
					await dataContext.SaveChangesAsync();
					return Results.Ok(await dataContext.Books.ToListAsync());
				}
			});
		}
	}
}
