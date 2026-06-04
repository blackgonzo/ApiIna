using inaApp.Entities;
using inaApp.Common.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static inaApp.Repository.ProductoRepository;

namespace inaApp.Repository
{
    public class ProductoRepository : IProductoRepository
    {

      

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Producto> CrearAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        public async Task<Producto> ActualizarAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}