using CoopApplication.Persistence.Repository.Implementations;
using CoopApplication.Persistence.Repository.Interfaces;
using CoopApplication.Services.Implementations;
using CoopApplication.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CoopApplication.Services.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Add Services

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAssociationService, AssociationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ILoanTypeService, LoanTypeService>();
            services.AddScoped<ILoanTakenService, LoanTakenService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<ILoanRepaymentService, LoanRepaymentService>();
            return services;
        }
    }
}
