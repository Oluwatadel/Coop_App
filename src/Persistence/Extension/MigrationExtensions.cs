
using System.Globalization;
using CoopApplication.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace AdmissionService.Infrastructure.Persistence.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigration(IServiceProvider serviceProvider, bool ensureDbCreated = false)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CoopDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>()
        .CreateLogger("MigrationExtensions");

        try
        {
            logger.LogInformation("Starting database migration for {Database}", context.Database.GetDbConnection().Database);
            if (ensureDbCreated)
            {
                EnsureDbCreation(context, logger);
            }
            context.Database.Migrate();
            logger.LogInformation("Database migration completed successfully for {Database}", context.Database.GetDbConnection().Database);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying migration for {Database}", context.Database.GetDbConnection().Database);
            throw;
        }
    }
    private static void EnsureDbCreation(CoopDbContext context, ILogger logger)
    {
        var databaseName = context.Database.GetDbConnection().Database;
        if (context.Database.CanConnect())
        {
            logger.LogInformation("Database {Database} already exists and is accessible.", databaseName);
            return;
        }

        logger.LogWarning("Database {Database} does not exist. Attempting to create it.", databaseName);

        var connectionString = context.Database.GetConnectionString()!.Replace($"Database={databaseName};", "", true, CultureInfo.InvariantCulture);

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        using var cmd = connection.CreateCommand();
#pragma warning disable SCS0002
        cmd.CommandText = $"CREATE DATABASE \"{databaseName}\"";
        cmd.ExecuteNonQuery();
#pragma warning restore SCS0002

        logger.LogInformation("Database {Database} created successfully.", databaseName);
    }
}