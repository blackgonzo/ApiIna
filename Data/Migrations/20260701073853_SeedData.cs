using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace inaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbCategoria",
                columns: new[] { "Id", "Date", "Descripcion", "Estado", "Nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Productos electrónicos y tecnológicos", true, "Electrónicos" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prendas de vestir y accesorios", true, "Ropa" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Artículos para el hogar y decoración", true, "Hogar" }
                });

            migrationBuilder.InsertData(
                table: "tbCliente",
                columns: new[] { "IdCliente", "Activo", "CorreoElectronico", "FechaCreacion", "IdTipoIdentificacion", "Nombre", "NumeroIdentificacion", "PrimerApellido", "SegundoApellido", "Telefono", "TipoIdentificacion" },
                values: new object[,]
                {
                    { 1, true, "juan.perez@email.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Juan", "123456789", "Pérez", "García", "8888-8888", 1 },
                    { 2, true, "maria.lopez@email.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "María", "987654321", "López", null, "8888-8889", 1 },
                    { 3, true, "carlos.mora@email.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Carlos", "ABC123456", "Mora", "Jiménez", "8888-8890", 3 }
                });

            migrationBuilder.InsertData(
                table: "tbProducto",
                columns: new[] { "Id", "CategoriaId", "Descripcion", "Estado", "Nombre", "Precio", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Laptop de alto rendimiento para gaming", true, "Laptop Gamer", 1200.00m, 10 },
                    { 2, 2, "Camiseta de algodón 100% natural", true, "Camiseta Algodón", 25.00m, 50 },
                    { 3, 3, "Lámpara LED de escritorio con brazo ajustable", true, "Lámpara LED", 45.00m, 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbCliente",
                keyColumn: "IdCliente",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbCliente",
                keyColumn: "IdCliente",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbCliente",
                keyColumn: "IdCliente",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbProducto",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbProducto",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbProducto",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tbCategoria",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tbCategoria",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tbCategoria",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
