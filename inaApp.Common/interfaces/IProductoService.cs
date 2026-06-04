using inaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Common.interfaces
{
    public interface IProductoService
    {
       

        List<Producto> ObtenerTodosAsync();
        Producto ObtenerPorId(int id);
        Producto CrearAsync(Producto producto);
        Producto ActualizarAsync(Producto producto);
        bool EliminarAsync(int id);

    }
}
