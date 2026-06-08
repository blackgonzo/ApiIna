using inaApp.Common.interfaces;
using inaApp.Common.Interfaces;
using inaApp.Entities;
using inaApp.Repository;
using inaApp.services;



namespace inaApp.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IGenericService<Producto>, ProductoService>();
            services.AddScoped<IGenericRepository<Producto>, ProductoRepository>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
                
            return services;
        }
    }
}