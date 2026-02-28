using Hypesoft.Domain.Repositories;
using Hypesoft.Infrastructure.Data;
using Hypesoft.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Hypesoft.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString, string databaseName)
        {
            var mongoContext = new MongoContext(connectionString, databaseName);
            services.AddSingleton(mongoContext);

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            
            services.AddScoped<MongoSeeder>();

            return services;
        }
    }
}