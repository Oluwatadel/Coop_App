using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AdmissionService.Infrastructure.Persistence.Extensions;

public static class NpgsqlConnectionExtension
{
    public static NpgsqlConnectionStringBuilder GetDbConnectionStringBuilder(this IConfiguration configuration)
    {
        return new NpgsqlConnectionStringBuilder
        {
            Host = configuration.GetValue<string>("DB_HOST"),
            Database = configuration.GetValue<string>("DB_NAME"),
            Password = configuration.GetValue<string>("DB_PASSWORD"),
            Username = configuration.GetValue<string>("DB_USERNAME"),
            Port = configuration.GetValue<int?>("DB_PORT") ?? 5432,

            IncludeErrorDetail = true,
            Pooling = true,

            // REQUIRED for Render PostgreSQL
            SslMode = SslMode.Require,
            TrustServerCertificate = true

        };
    }
}