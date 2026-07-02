using inaApp.Common.Interfaces;
using inaApp.Data;
using inaApp.DTOs.Categoria;
using inaApp.DTOs.cliente;
using inaApp.DTOs.Producto;
using inaApp.Entites;
using inaApp.Repository;
using inaApp.Services;
using inaApp.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace inaApp.Api.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplicaServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Inyecciones de baseDatos-dbContex
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //profile auto mapper
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            //Inyecciones de dependencia de servicios
            services.AddScoped<IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO>, ProductoService>();
            services.AddScoped<IGenericService<ClienteResponseDTO, ClienteCreateDTO, ClienteUpdateDTO>, ClienteService>();
            services.AddScoped<IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO>, CategoriaService>();

            //Inyección directa de servicios
            //Esto permite inyectar CategoriaService directamente en el CategoriaController
            services.AddScoped<CategoriaService>();

            //Inyección de dependencia de repostorios
            services.AddScoped<IProductoRepository<Producto>, ProductoRespository>();
            services.AddScoped<IGenericRepository<Cliente>, ClienteRepository>();
            services.AddScoped<IGenericRepository<Categoria>, CategoriaRepository>();

            //Inyección directa de repositorios
            //Esto permite inyectar CategoriaRepository directamente en CategoriaService
            services.AddScoped<CategoriaRepository>();

            return services;
        }
    }
}