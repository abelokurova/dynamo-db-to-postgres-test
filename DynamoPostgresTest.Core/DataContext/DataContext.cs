using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DynamoPostgresTest.WebApi;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string? server = Environment.GetEnvironmentVariable("PGSQL_SERVER");
        string? port = Environment.GetEnvironmentVariable("PGSQL_PORT");
        string? database = Environment.GetEnvironmentVariable("PGSQL_DATABASE");
        string? username = Environment.GetEnvironmentVariable("PGSQL_USERNAME");
        string? password = Environment.GetEnvironmentVariable("PGSQL_PASSWORD");

        var haveEnvConnectionString = !string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(port) && !string.IsNullOrEmpty(database) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);

        var connectionString = string.Format("Host={0};Port={1};Database={2};Username={3};Password={4};Include Error Detail=true;", server, port, database, username, password);
        
        // connect to postgres with connection string from app settings

        options.UseNpgsql(haveEnvConnectionString ? connectionString : Configuration.GetConnectionString("PgsqlConnection"))
        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HistoryDocument>()
            .Property(p => p.Changes)
            .HasColumnType("jsonb");

        modelBuilder.Entity<HistoryDocument>()
            .Property(p => p.Medication)
            .HasColumnType("jsonb");

        modelBuilder.Entity<HistoryDocument>()
            .Property(p => p.ChangeComment)
            .IsRequired(false);

        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
    }

    public DbSet<HistoryDocument> Documents { get; set; }
}