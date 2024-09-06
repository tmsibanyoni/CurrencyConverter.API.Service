using CurrencyConverter.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Repository.MySqlRepository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyModel>()
                .HasKey(b => b.Id);
        }

        public DbSet<CurrencyModel> Currency { get; set; }
    }
}