using inaApp.Common.interfaces;
using inaApp.Common.Interfaces;
using inaApp.Entities;
using inaApp.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.services
{
    public class ProductoService : IGenericService<Producto>
    {

        private readonly IProductoRepository _productoRepo;
        public ProductoService(IProductoRepository productoRepo)
        {
             _productoRepo = productoRepo;
        }

        public async Task<Producto> ActualizarAsync(Producto entity)
        {
            if (entity.Precio <= 0)
                throw new InvalidPriceException();

            if (entity.Stock <= 0)
                throw new InvalidStockException();

            var existente = await _productoRepo.ObtenerPorNombreAsync(entity.Nombre);
            if (existente != null && existente.Id != entity.Id)
                throw new DuplicateProductNameException();

            return await _productoRepo.ActualizarAsync(entity);
        }

        public async Task<Producto> CrearAsync(Producto entity)
        {
            if (entity.Precio <= 0)
                throw new InvalidPriceException();

            if (entity.Stock <= 0)
                throw new InvalidStockException();

            var existente = await _productoRepo.ObtenerPorNombreAsync(entity.Nombre);
            if (existente != null)
                throw new DuplicateProductNameException();

            return await _productoRepo.CrearAsync(entity);
        }

        public Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Producto> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {

            return await _productoRepo.ObtenerTodosAsync();

         
        }
    }
}
