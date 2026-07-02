using Microsoft.EntityFrameworkCore;
using inaApp.Entites;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using static inaApp.Common.Enums.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Data
{
    public class ApplicationDbContext:DbContext
    {
        //Generar contructor del db context
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        //Entidades para la base de datos
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        //fluent api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Relacion de Producto
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);

            //Relación de Categoria
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Productos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);


            base.OnModelCreating(modelBuilder);

            // Seed data - 3 registros por tabla
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Electrónicos", Descripcion = "Productos electrónicos y tecnológicos", Estado = true, Date = new DateTime(2024, 1, 1) },
                new Categoria { Id = 2, Nombre = "Ropa", Descripcion = "Prendas de vestir y accesorios", Estado = true, Date = new DateTime(2024, 1, 1) },
                new Categoria { Id = 3, Nombre = "Hogar", Descripcion = "Artículos para el hogar y decoración", Estado = true, Date = new DateTime(2024, 1, 1) }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Laptop Gamer", Precio = 1200.00m, Stock = 10, Descripcion = "Laptop de alto rendimiento para gaming", Estado = true, CategoriaId = 1 },
                new Producto { Id = 2, Nombre = "Camiseta Algodón", Precio = 25.00m, Stock = 50, Descripcion = "Camiseta de algodón 100% natural", Estado = true, CategoriaId = 2 },
                new Producto { Id = 3, Nombre = "Lámpara LED", Precio = 45.00m, Stock = 30, Descripcion = "Lámpara LED de escritorio con brazo ajustable", Estado = true, CategoriaId = 3 }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { IdCliente = 1, IdTipoIdentificacion = 1, TipoIdentificacion = TipoIdentificacionEnum.CedulaNacional, NumeroIdentificacion = "123456789", Nombre = "Juan", PrimerApellido = "Pérez", SegundoApellido = "García", CorreoElectronico = "juan.perez@email.com", Telefono = "8888-8888", Activo = true, FechaCreacion = new DateTime(2024, 1, 1) },
                new Cliente { IdCliente = 2, IdTipoIdentificacion = 1, TipoIdentificacion = TipoIdentificacionEnum.CedulaNacional, NumeroIdentificacion = "987654321", Nombre = "María", PrimerApellido = "López", SegundoApellido = null, CorreoElectronico = "maria.lopez@email.com", Telefono = "8888-8889", Activo = true, FechaCreacion = new DateTime(2024, 1, 1) },
                new Cliente { IdCliente = 3, IdTipoIdentificacion = 3, TipoIdentificacion = TipoIdentificacionEnum.Pasaporte, NumeroIdentificacion = "ABC123456", Nombre = "Carlos", PrimerApellido = "Mora", SegundoApellido = "Jiménez", CorreoElectronico = "carlos.mora@email.com", Telefono = "8888-8890", Activo = true, FechaCreacion = new DateTime(2024, 1, 1) }
            );
        }
    }
}
