using inaApp.Common.interfaces;
using inaApp.Common.Interfaces;
using inaApp.Data;
using inaApp.Entities;
using inaApp.Repository;
using inaApp.services;
using Microsoft.EntityFrameworkCore;



namespace inaApp.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IGenericService<Producto>, ProductoService>();
            services.AddScoped<IGenericRepository<Producto>, ProductoRepository>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
                
            return services;
        }
    }
}