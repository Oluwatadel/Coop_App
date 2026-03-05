using CoopApplication.Services.Implementations;
using CoopApplication.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopApplication.Services.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Add Services

            services.AddScoped<IRoleService, RoleService>();
            return services;
        }
    }
}
