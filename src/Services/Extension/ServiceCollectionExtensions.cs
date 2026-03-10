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
            services.AddScoped<ILoanTypeService, LoanTypeService>();
            return services;
        }
    }
}
