using Microsoft.EntityFrameworkCore;
using MinimalBookApi.Models;


namespace MinimalBookApi.Data
{
	public class DataContext : DbContext
	{
		private IConfiguration _configuration;
		public DataContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
		{
			_configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(_configuration.GetConnectionString("BookConnection"));
		}

        public DbSet<Book> Books => Set<Book>();
    }
}
