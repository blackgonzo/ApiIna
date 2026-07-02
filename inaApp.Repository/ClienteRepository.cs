using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.Data;
using inaApp.Entites;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace inaApp.Repository
{
    public class ClienteRepository : IGenericRepository<Cliente>
    {

        //Inyección de dependecias de contex
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        //Metodo para actualizar un cliente
        //Metodo para actualizar un cliente
        public async Task<Cliente> ActualizarAsync(int id, Cliente entity)
        {
            try
            {
                //Buscamos el cliente activo por id
                Cliente clienteExistente = await _context.Cliente
                    .FirstOrDefaultAsync(x => x.IdCliente == id && x.Activo == true);

                //Validamos si el cliente existe
                if (clienteExistente == null)
                {
                    throw new NotFoundException($"No se encontro un cliente activo con la el id {id}");
                }

                //Actualizamos el id del tipo de identificación
                clienteExistente.IdTipoIdentificacion = entity.IdTipoIdentificacion;

                //Actualizamos el tipo de identificación
                clienteExistente.TipoIdentificacion = entity.TipoIdentificacion;

                //Actualizamos el número de identificación
                clienteExistente.NumeroIdentificacion = entity.NumeroIdentificacion;

                //Actualizamos el nombre
                clienteExistente.Nombre = entity.Nombre;

                //Actualizamos el primer apellido
                clienteExistente.PrimerApellido = entity.PrimerApellido;

                //Actualizamos el segundo apellido
                clienteExistente.SegundoApellido = entity.SegundoApellido;

                //Actualizamos el correo electrónico
                clienteExistente.CorreoElectronico = entity.CorreoElectronico;

                //Actualizamos el teléfono
                clienteExistente.Telefono = entity.Telefono;

                //Guardamos los cambios
                await _context.SaveChangesAsync();

                //Retornamos el cliente actualizado
                return clienteExistente;
            }
            catch (DbUpdateException ex) when (
            //Validar cuando ocurran errores de duplicados
            ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                if (sqlEx.Message.Contains("IX_tbCliente_TipoIdentificacion_NumeroIdentificacion"))
                {
                    throw new DuplicateClientIdentificationException(
                        "Ya existe un cliente con ese tipo y número de identificación."
                    );
                }

                if (sqlEx.Message.Contains("IX_tbCliente_CorreoElectronico"))
                {
                    throw new ClientEmailAlreadyExistsException(
                        "Ya existe un cliente con ese correo electrónico."
                    );
                }

                throw new ConflictException("Ya existe un cliente con datos duplicados.");
            }
            catch (Exception)
            {
                throw;
            }
        }
   
        public async Task<Cliente> CrearAsync(Cliente cliente)
        {
            try
            {
                //Agregamos el cliente a la tabla
                await _context.Cliente.AddAsync(cliente);

                //Guardmos los cambios
                await _context.SaveChangesAsync();

                //Devolver el cliente
                return cliente;
            }
            catch (DbUpdateException ex) when (
            //Validar cuando ocurran errores de duplicados
            ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                //Error de tipo de Número de identificación duplicado
                if (sqlEx.Message.Contains("IX_tbCliente_TipoIdentificacion_NumeroIdentificacion"))
                {
                    throw new DuplicateClientIdentificationException(
                        "Ya existe un cliente con ese tipo y número de identificación."
                    );
                }

                //Errror de tipo de correo electrónico duplicado
                if (sqlEx.Message.Contains("IX_tbCliente_CorreoElectronico"))
                {
                    throw new ClientEmailAlreadyExistsException(
                        "Ya existe un cliente con ese correo electrónico."
                    );
                }

                throw new ConflictException("Ya existe un cliente con datos duplicados.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo para eliminar un cliente
        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                //Retornamos si hubo fila afectada
                var result = await _context.Cliente
                    .Where(x => x.IdCliente == id && x.Activo == true)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(c => c.Activo, false)
                    ) > 0;

                //Validamos si ninguna fila fue afectada
                if (!result)
                {
                    throw new NotFoundException($"No se encontro ningun cliente activo con el id {id}");
                }

                //Retornamos resultado
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo para obtener un  cliente por su id
        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            try
            {
                //Buscamos y extraemos el cliente
                var cliente = await _context.Cliente.AsNoTracking()
                    .Where(x => x.IdCliente == id && x.Activo == true)
                    .SingleOrDefaultAsync();

                //Validar el cliente
                if (cliente is null)
                {
                    throw new NotFoundException($"No se encontro ningun cliente activo con el id {id}");
                }

                //Retornamos el cliente
                return cliente;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Metodo para obtener todos los clientes
        public async Task<List<Cliente>> obtenerTodosAsync()
        {
            try
            {
                //Extraer todos los clientes de la bd
                return await _context.Cliente.AsNoTracking()
                    .Where(x => x.Activo == true)
                    .ToListAsync();
            }
            catch (Exception )
            {
                throw;
            }
        }

        //Metodo para validar si lo nombres son iguales
        public async Task<bool> validarNombreRepetido(string nombre)
        {
            try
            {
                //Validar si los nombre son iguales
                return await _context.Cliente
                    .AnyAsync(x => x.Nombre == nombre && x.Activo == true);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}