using AutoMapper;
using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.Common.Response;
using inaApp.DTOs.Producto;

using inaApp.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Services
{
    public class ProductoService : IGenericService<ProductoResponseDTO,ProductoCreateDTO,ProductoUpdateDTO>
    {

        //Implementar a inyección de dependecias
        private readonly IProductoRepository<Producto> _producRepository;
        private readonly IMapper _mapper;

        public ProductoService(IProductoRepository<Producto> productRepo, IMapper mapper)
        {
            this._producRepository = productRepo;
            this._mapper=mapper; 
        }

        public async Task<ApiResponse<ProductoResponseDTO>> ActualizarAsync(int id,ProductoUpdateDTO entity)
        {
            //Validar precio
            if (entity.Precio <= 0) throw new InvalidPriceException("El precio debe ser una cantidad positiva");

            //Valida stock
            if (entity.Stock <= 0) throw new InvalidStockException("El stock debe ser una cantidad positiva");

            //Validar nombre (excluyendo el registro actual)
            var productoActual = await _producRepository.ObtenerPorIdAsync(id);
            if (productoActual != null && productoActual.Nombre != entity.Nombre && await _producRepository.validarNombreRepetido(entity.Nombre))
                throw new DuplicateNameException($"El nombre {entity.Nombre} ya se encuentra agregado como producto");

            //Validar que la categoria 
            if (!await _producRepository.ValidarCategoriaProducto(entity.CategoriaId))
            {
                throw new NotFoundException("La categoría no existe o esta se encuentra inactiva.");
            }

          

            //Convertimos el dto a entidad
            Producto producto=_mapper.Map<Producto>(entity);
            //Extraemos el producto
            var resul= await _producRepository.ActualizarAsync(id, producto);

            //Validamos el producto
            if (resul is null)
            {
                throw new NotFoundException($"El producto no puede venir vasio.");
            }

            //Convertimos el producto en dto
            ProductoResponseDTO response= _mapper.Map<ProductoResponseDTO>(resul);
            //Retornamos el response dto
            return new ApiResponse<ProductoResponseDTO>
            {
                Data = _mapper.Map<ProductoResponseDTO>(resul),
                Message = "Producto Actualizado exitozamente",
                Success = true,
            };
        }

        public async Task<ApiResponse<ProductoResponseDTO>> CrearAsync(ProductoCreateDTO entity)
        {
            try
            {
                //Validar precio
                if (entity.Precio <= 0) throw new InvalidPriceException("El precio debe ser una cantidad positiva");

                //Valida stock
                if (entity.Stock <= 0) throw new InvalidStockException("El stock debe ser una cantidad positiva");

                //Validar nombre
                if (await _producRepository.validarNombreRepetido(entity.Nombre)) throw new DuplicateNameException($"El nombre {entity.Nombre} ya se encuentra agregado como producto.");

                //Validar que la categoria exista
                if (!await _producRepository.ValidarCategoriaProducto(entity.CategoriaId))
                {
                    throw new NotFoundException("La categoría no existe o se encuentra inactiva.");
                }

                /*Producto producto=new Producto
        { 
            Nombre=entity.Nombre,
            Precio=entity.Precio,
            Stock=entity.Stock,
            Descripcion=entity.Descripcion,
            Estado=true
        };*/

                //Returnamos la respuesta del repositorio
                Producto producto = _mapper.Map<Producto>(entity);
                producto = await _producRepository.CrearAsync(producto);

                /*ProductoResponseDTO productoResponse=new ProductoResponseDTO
                {
                    Id=producto.Id,
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    Descripcion = producto.Descripcion
                };*/

                return new ApiResponse<ProductoResponseDTO>
                {
                    Data = _mapper.Map<ProductoResponseDTO>(producto),
                    Message = "Producto creado con exito",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ApiResponse<bool>> EliminarAsync(int id)
        {
            //Retornamos si se pudo eliminar
            return new ApiResponse<bool>
            {
                Data = await _producRepository.EliminarAsync(id),
                Message = "Producto eliminado exitosamente",
                Success = true,
            };
        }

        public async Task<ApiResponse<ProductoResponseDTO>> ObtenerPorIdAsync(int id)
        {
            try
            {
                //Extraemos el producto
                var producto = await _producRepository.ObtenerPorIdAsync(id);

                //Validamos el producto
                if (producto is null)
                {
                    throw new NotFoundException($"EL producto con id {id} no existe");
                }

                //Mapeamos
                ProductoResponseDTO response = _mapper.Map<ProductoResponseDTO>(producto);

                //retornamos el response
                return new ApiResponse<ProductoResponseDTO>
                { 
                    Data = _mapper.Map<ProductoResponseDTO>(producto),
                    Message = "Producto Obtenido Exitosamente",
                    Success = true,
                };
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<ApiResponse<List<ProductoResponseDTO>>> obtenerTodosAsync()
        {
            //Extraemos la lista de productos
            List<Producto> list=await _producRepository.obtenerTodosAsync();

            //Mapeamos la lista
            List<ProductoResponseDTO> response = _mapper.Map<List<ProductoResponseDTO>>(list);

            //Retornamos el response
            return new ApiResponse<List<ProductoResponseDTO>>
            { 
                Data= _mapper.Map<List<ProductoResponseDTO>>(list),
                Message ="Productos obtenidos exitosamente",
                Success = true,
            };
        }
    }
}
