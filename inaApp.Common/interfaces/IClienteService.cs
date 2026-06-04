using inaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Common.interfaces
{
    public interface IClienteService
    {
        List<Cliente> ObtenerTodosAsync();
        Cliente ObtenerPorId(int id);
        Cliente CrearAsync(Cliente cliente);
        Cliente ActualizarAsync(Cliente cliente);
        bool EliminarAsync(int id);
    }
}
