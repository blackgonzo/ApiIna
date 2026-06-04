using inaApp.Common.interfaces;
using inaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.services
{
    public class ProductoService : IProductoService
    {

        private readonly IProductoRepository _productoRepo;
        public ProductoService(IProductoRepository productoRepo)
        {
             _productoRepo = productoRepo;
        }


        public Producto ActualizarAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        public Producto CrearAsync(Producto producto)
        {
            throw new NotImplementedException();
        }

        public bool EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Producto ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Producto> ObtenerTodosAsync()
        {
            _productoRepo.ObtenerTodosAsync();


            return null;
        }
    }
}
