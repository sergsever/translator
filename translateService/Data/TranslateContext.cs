using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace translateService.Data
{
	public class TranslateContext : DbContext
	{
		private readonly IOptions<ConnectionStrings> connectionStrings;
		public TranslateContext(IOptions<ConnectionStrings> connectionStrings) : base(GetOptions(connectionStrings.Value.Mssql)) {
			this.connectionStrings = connectionStrings;
		}
		public TranslateContext(DbContextOptions<TranslateContext> options, IOptions<ConnectionStrings> connectionStrings) : base(GetOptions(connectionStrings.Value.Mssql))
		{ 
			this.connectionStrings = connectionStrings;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(connectionStrings.Value.Mssql);

		}

		private static DbContextOptions<TranslateContext> GetOptions(string connectionString)
		{

			var builder = new DbContextOptionsBuilder<TranslateContext>();
			return builder.UseSqlServer(connectionString).Options;
		}


		public DbSet<Translation> Translations { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Translation>().HasKey(t => new { t.Text, t.Lang });
		}
	}
}
