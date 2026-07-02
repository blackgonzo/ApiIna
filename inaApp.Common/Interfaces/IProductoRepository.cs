using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Common.Interfaces
{
    public interface IProductoRepository<E> : IGenericRepository<E>
    {

        Task<bool> ValidarCategoriaProducto(int id);


    }
}
