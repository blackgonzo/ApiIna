using AutoMapper;
using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.Common.Response;
using inaApp.DTOs.Categoria;
using inaApp.Entites;
using inaApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Services
{
    public class CategoriaService : IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO>
    {
        //Inyección de dependecias
        private readonly CategoriaRepository _repository;
        private readonly IMapper _mapper;
        public CategoriaService(CategoriaRepository repository,IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<CategoriaResponseDTO>> ActualizarAsync(int id, CategoriaUpdateDTO entity)
        {
            //Validar entidad
            if (entity == null) throw new BusinessValidationException("Los datos de la categoria son obligatorios.");

            //Validar id
            if (id <= 0) throw new BusinessValidationException("El id de la categoria no es valida.");

            //Validar nombre de la entidad
            if (string.IsNullOrWhiteSpace(entity.Nombre)) throw new BusinessValidationException("Los del nombre de categoria son obligatorio.");

            //Validar si el nombre ya esta registrado (excluyendo el registro actual)
            var categoriaActual = await _repository.ObtenerPorIdAsync(id);
            if (categoriaActual.Nombre != entity.Nombre && await _repository.validarNombreRepetido(entity.Nombre))
                throw new DuplicateNameException($"El nombre {entity.Nombre} ya se encuentra agregado como categoria.");

            //Mapeamos el request
            Categoria categoria=_mapper.Map<Categoria>(entity);

            //Extraemos el resultado de actulizar
            Categoria result=await _repository.ActualizarAsync(id, categoria);

            //Mapeamos el resultado
            CategoriaResponseDTO response = _mapper.Map<CategoriaResponseDTO>(result);

            //Retornamos el api Reponse
            return new ApiResponse<CategoriaResponseDTO>
            {
                Data=response,
                Message="La categoria fue actulizada exitosamente.",
                Success=true,
            };
        }

        public async Task<ApiResponse<CategoriaResponseDTO>> CrearAsync(CategoriaCreateDTO entity)
        {
            //Validar entidad
            if (entity == null) throw new BusinessValidationException("Los datos de la categoria son obligatorios.");

            //Validar nombre de la entidad
            if (string.IsNullOrWhiteSpace(entity.Nombre)) throw new BusinessValidationException("Los del nombre de categoria son obligatorio");

            //Validar si el nombre ya esta registrado
            if(await _repository.validarNombreRepetido(entity.Nombre)) throw new DuplicateNameException($"El nombre {entity.Nombre} ya se encuentra agregado como categoria.");

            //Mapeamos la entidad
            Categoria categoria = _mapper.Map<Categoria>(entity);

            //Creamos la entidad
            Categoria resul = await _repository.CrearAsync(categoria);

            //Mapeamos el resultado
            CategoriaResponseDTO response = _mapper.Map<CategoriaResponseDTO>(resul);

            //Retornamos el Api response
            return new ApiResponse<CategoriaResponseDTO>
            {
                Data = response,
                Message = "Categoria creada exitosamente",
                Success = true
            };

        }

        public async Task<ApiResponse<bool>> EliminarAsync(int id)
        {
            //Validar id
            if (id <= 0) throw new BusinessValidationException("El id de la categoria no es valida.");

            bool reponse=await _repository.EliminarAsync(id);

            return new ApiResponse<bool>
            {
                Data = reponse,
                Message="Categoria eliminada exitosamente.",
                Success = true
            };
        }

        public async Task<ApiResponse<CategoriaResponseDTO>> ObtenerPorIdAsync(int id)
        {
            //Validar id
            if (id <= 0) throw new BusinessValidationException("El id de la categoria no es valida.");

            //Traemos la categoria
            Categoria categoria=await _repository.ObtenerPorIdAsync(id);

            //Mapeamos la categoria
            CategoriaResponseDTO reponse= _mapper.Map<CategoriaResponseDTO>(categoria);

            //Retornamos el api response
            return new ApiResponse<CategoriaResponseDTO>
            {
                Data = reponse,
                Message = "Categoria obtenida exitosamente",
                Success = true
            };
        }

        public async Task<ApiResponse<List<CategoriaResponseDTO>>> obtenerTodosAsync()
        {
            //Extramemos la lista de categorias
            List<Categoria> list= await _repository.obtenerTodosAsync();

            //Mapeamos la listaa
            List<CategoriaResponseDTO> response=_mapper.Map<List<CategoriaResponseDTO>>(list);

            //Retornamos un api response
            return new ApiResponse<List<CategoriaResponseDTO>>
            {
                Data = response,
                Message = "Categorias obtenidas exitosamente",
                Success = true
            };
        }
    }
}
