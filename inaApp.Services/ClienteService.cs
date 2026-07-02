using AutoMapper;
using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.Common.Response;
using inaApp.DTOs.cliente;
using static inaApp.Common.Enums.Enumeradores;

namespace inaApp.Services
{
    public class ClienteService : IGenericService<ClienteResponseDTO, ClienteCreateDTO, ClienteUpdateDTO>
    {
        private readonly IGenericRepository<Cliente> _clienteRepo;
        private readonly IMapper _mapper;

        public ClienteService(IGenericRepository<Cliente> clienteRepo, IMapper mapper)
        {
            _clienteRepo = clienteRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ClienteResponseDTO>> CrearAsync(ClienteCreateDTO cliente)
        {
            //Validar si el cliente viene nulo
            if (cliente == null)
            {
                throw new BusinessValidationException("Los datos del cliente son obligatorios.");
            }

            //Validar si el nombre esta vacío o es nulo
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                throw new BusinessValidationException("El nombre del cliente es obligatorio.");
            }

            //Validar si el numero de identificación esta vacío o es nulo
            if (string.IsNullOrWhiteSpace(cliente.NumeroIdentificacion))
            {
                throw new BusinessValidationException("El número de identificación es obligatorio.");
            }

            //Validar si el correo viene nulo o vacio
            if (string.IsNullOrWhiteSpace(cliente.CorreoElectronico))
            {
                throw new BusinessValidationException("El correo electrónico es obligatorio.");
            }

            //Convertir entidad
            Cliente entity = _mapper.Map<Cliente>(cliente);

            //Validar si el enum es permitido
            if (!Enum.IsDefined(typeof(TipoIdentificacionEnum), (TipoIdentificacionEnum)entity.IdTipoIdentificacion))
            {
                throw new BusinessValidationException("El tipo de identificación no es permitido");
            }

            //Pasamos el tipo de identificación
            entity.TipoIdentificacion = (TipoIdentificacionEnum)entity.IdTipoIdentificacion;

            //Extraemos el resul
            Cliente result = await _clienteRepo.CrearAsync(entity);
            //Mapeamos el cliente
            ClienteResponseDTO response = _mapper.Map<ClienteResponseDTO>(result);

            //Retornamos el response
            return new ApiResponse<ClienteResponseDTO>
            {
                Data = response,
                Message = "Cliente creado exitosamente",
                Success = true
            };
        }

        public async Task<ApiResponse<ClienteResponseDTO>> ActualizarAsync(int id, ClienteUpdateDTO cliente)
        {
            //Validar si el id es permitido
            if (id <= 0)
            {
                throw new BusinessValidationException("El id del cliente no es válido.");
            }

            //Validar si el cliente no es nulo
            if (cliente == null)
            {
                throw new BusinessValidationException("Los datos del cliente son obligatorios.");
            }

            //Validar si el nombre es nulo o viene vacio
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                throw new BusinessValidationException("El nombre del cliente es obligatorio.");
            }

            //Validar si el correo viene nulo o vacio
            if (string.IsNullOrWhiteSpace(cliente.CorreoElectronico))
            {
                throw new BusinessValidationException("El correo electrónico es obligatorio.");
            }

            //Validar si el tipo de identificación viene nulo
            if (!cliente.TipoIdentificacion.HasValue)
            {
                throw new BusinessValidationException("El tipo de identificación es obligatorio.");
            }

            //Validar si el enum es permitido
            if (!Enum.IsDefined(typeof(TipoIdentificacionEnum), cliente.TipoIdentificacion.Value))
            {
                throw new BusinessValidationException("El tipo de identificación no es permitido");
            }

            //Mapeamos el DTO a la entidad Cliente
            Cliente clienMapper = _mapper.Map<Cliente>(cliente);

            //Le pasamos el tipo de identificación
            clienMapper.TipoIdentificacion = cliente.TipoIdentificacion.Value;

            //Le pasamos el id del tipo de identificación
            clienMapper.IdTipoIdentificacion = (int)cliente.TipoIdentificacion.Value;

            //Actualizamos el cliente
            Cliente result = await _clienteRepo.ActualizarAsync(id, clienMapper);

            //Mapeamos la entidad actualizada al DTO de respuesta
            ClienteResponseDTO response = _mapper.Map<ClienteResponseDTO>(result);

            return new ApiResponse<ClienteResponseDTO>
            {
                Data = response,
                Message = "Cliente actualizado exitosamente",
                Success = true
            };
        }

        public async Task<ApiResponse<bool>> EliminarAsync(int id)
        {
            //Validar si el id es permitdo
            if (id <= 0)
            {
                throw new BusinessValidationException("El id del cliente no es válido.");
            }

            bool response = await _clienteRepo.EliminarAsync(id);

            return new ApiResponse<bool>
            {
                Data = response,
                Message = "Cliente eliminado exitosamente",
                Success = true
            };
        }

        public async Task<ApiResponse<ClienteResponseDTO>> ObtenerPorIdAsync(int id)
        {
            //Valiar si id es permitido
            if (id <= 0)
            {
                throw new BusinessValidationException("El id del cliente no es válido.");
            }

            //Extraer cliente
            Cliente cliente = await _clienteRepo.ObtenerPorIdAsync(id);
            //Mapeamos el cliente
            ClienteResponseDTO response = _mapper.Map<ClienteResponseDTO>(cliente);
            //Retornmos el reponse
            return new ApiResponse<ClienteResponseDTO>
            {
                Data = response,
                Message = "Cliente obtenido exitosamente",
                Success = true
            };
        }

        public async Task<ApiResponse<List<ClienteResponseDTO>>> obtenerTodosAsync()
        {
            List<Cliente> list = await _clienteRepo.obtenerTodosAsync();

            List<ClienteResponseDTO> response = _mapper.Map<List<ClienteResponseDTO>>(list);

            return new ApiResponse<List<ClienteResponseDTO>>
            {
                Data = response,
                Message = "Clientes obtenidos exitosamente",
                Success = true
            };

        }
    }
}