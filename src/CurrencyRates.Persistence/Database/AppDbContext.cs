using CurrencyRates.Domain.Entities.ExchangeRates;
using Microsoft.EntityFrameworkCore;

namespace CurrencyRates.Persistence.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}