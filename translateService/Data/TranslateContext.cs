using Microsoft.EntityFrameworkCore;

namespace translateService.Data
{
	public class TranslateContext : DbContext
	{
		public TranslateContext() : base() { }
		public TranslateContext(DbContextOptions<TranslateContext> options) : base(GetOptions()) 
		{ 
			if (Translations == null)
			{
			}
		}

/*
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseInMemoryDatabase("translations");

		}
*/
		private static DbContextOptions<TranslateContext> GetOptions()
		{
			var cbuilder = new ConfigurationBuilder();
			var conf = cbuilder.AddJsonFile("appsettings.json").Build();
			string conn = conf.GetConnectionString("mssql");

			var builder = new DbContextOptionsBuilder<TranslateContext>();
			return builder.UseSqlServer(conn).Options;
		}


		public DbSet<Translation> Translations { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Translation>();
		}
	}
}
