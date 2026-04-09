using Inventario.Domain.Interfaces;
using Inventario.Infrastructure.Context;
using Inventario.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventario.Infrastructure.Registration
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigurationManager.Configuration = configuration;
            services.AddDbContext<InventoryDBContext>(
            options => options.UseSqlServer(
                 ConfigurationManager.DataBase,
                 x =>
                 {
                     x.EnableRetryOnFailure(5);
                 }
                )
            );
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

            return services;
        }
    }
}
