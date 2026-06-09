using inaApp.Entities;

namespace inaApp.Common.Interfaces
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        Task<Producto?> ObtenerPorNombreAsync(string nombre);
    }
}
