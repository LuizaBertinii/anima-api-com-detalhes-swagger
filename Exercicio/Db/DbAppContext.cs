using Microsoft.EntityFrameworkCore;
using Exercicio.Entities;

namespace Exercicio.Db;

public class DbAppContext : DbContext
{
    // estratégia 3, passando a dependencia do banco de dados via construtor
    public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
    {
    }

    public DbAppContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseMySql(configuration.GetConnectionString("cnn"), 
                new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }

    public DbSet<Vehicle> Vehicles { get; set; } = default!;
}
