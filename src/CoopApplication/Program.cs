using AdmissionService.Infrastructure.Persistence.Extensions;
using CoopApplication.api.Extension;
using CoopApplication.api.Middleware;
using CoopApplication.Persistence.Extension;
using CoopApplication.Services.Extension;
using Dayspring_Backend.Extension;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDatabaseService(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer()
    .AddRepositories()
    .AddServices()
    .ConfigureApiVersioning()
    .AddSwagger();
builder.Services.ConfigureMvc();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureExceptionHandler();
app.ConfigureCORS();
app.ConfigureSwagger();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrationExtensions.ApplyMigration(app.Services, ensureDbCreated: true);

app.Run();
