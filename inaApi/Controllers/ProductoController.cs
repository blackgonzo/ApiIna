    using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.Common.Response;
using inaApp.DTOs.Producto;
using inaApp.Entites;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace inaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IGenericService<ProductoResponseDTO,ProductoCreateDTO,ProductoUpdateDTO> _productoService;

        public ProductoController(IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO> productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _productoService.obtenerTodosAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var response = await _productoService.ObtenerPorIdAsync(id);

                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductoCreateDTO producto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _productoService.CrearAsync(producto);
                return Ok(response);
            }
            catch (InvalidPriceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidStockException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {

                return BadRequest("Error al crear el producto");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] ProductoUpdateDTO producto)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id incorrecto");
                }

                var response = await _productoService.ActualizarAsync(id, producto);


                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Producto no encontrado");
            }
            catch (DuplicateNameException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new ApiResponse<bool>
                    {
                        Data = false,
                        Message = "Error al eliminar, id incorrecto",
                        Success = false
                    });
                }

                var response = await _productoService.EliminarAsync(id);

                return response.Data
                    ? Ok(response)
                    : BadRequest(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ApiResponse<bool>
                {
                    Data = false,
                    Message = "Error interno del servidor",
                    Success = false
                });
            }
        }
    }
}