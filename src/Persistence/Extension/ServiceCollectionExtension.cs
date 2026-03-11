using AdmissionService.Infrastructure.Persistence.Extensions;
using CoopApplication.Persistence.Context;
using CoopApplication.Persistence.Repository.Implementations;
using CoopApplication.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoopApplication.Persistence.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetDbConnectionStringBuilder().ConnectionString;
            return services
                .AddDbContext<CoopDbContext>(options =>
                    options.UseNpgsql(connectionString,
                        action =>
                            action.MigrationsAssembly(typeof(CoopDbContext).Assembly.FullName)
                                .EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //Add Repository
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAssociationRepository, AssociationRepository>();
            services.AddScoped<ILoanTakenRepository, LoanTakenRepository>();
            services.AddScoped<ILoanRepaymentRepository, LoanRepaymentRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ILoanTypeRepository, LoanTypeRepository>();
            services.AddScoped<IUnitofWork, UnitOfWork>();
            return services;
        }
    }
}
