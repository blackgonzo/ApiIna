using inaApp.Common.interfaces;
using inaApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepo;

        public ClienteService(IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public Cliente ActualizarAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public Cliente CrearAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        public bool EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Cliente ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Cliente> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
