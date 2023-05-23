using MinimalBookApi.Data;
using MinimalBookApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

new BookEndpoints().MapEndpoints(app);

/*var books = new List<Book> {
	new Book { Id = 1, Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams"},
	new Book { Id = 2, Title = "1984", Author = "George Orwell" },
	new Book { Id = 3, Title = "Ready Player One", Author = "Ernest Cline" },
	new Book { Id = 4, Title = "The Martian", Author = "Andy Weir" }
};*/


app.Run();

