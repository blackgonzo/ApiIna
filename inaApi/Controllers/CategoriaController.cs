using inaApp.Common.Exceptions;
using inaApp.DTOs.Categoria;
using inaApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace inaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        //Inyección de dependencias
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        // GET: CategoriaController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                //Retornamos todas las categorias
                return Ok(await _service.obtenerTodosAsync());
            }
            catch (Exception)
            {
                return BadRequest("Error al mostrar todas las categorias");
            }
        }

        // GET: CategoriaController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                //Obtener categoria
                return Ok(await _service.ObtenerPorIdAsync(id));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Error al buscar la categoria con el id {id}.");
            }
        }

        // POST: CategoriaController/Create
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoriaCreateDTO categoria)
        {
            try
            {
                //Retornamos respuesta del services
                return Ok(await _service.CrearAsync(categoria));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Error al crear la categoria");
            }
        }

        // POST: CategoriaController/Edit/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] CategoriaUpdateDTO categoria)
        {
            try
            {
                //Actualizamos la categoria
                return Ok(await _service.ActualizarAsync(id, categoria));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DuplicateNameException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("Error al actualizar la categoria");
            }
        }

        // DELETE: CategoriaController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                //Eliminamos la categoria
                return Ok(await _service.EliminarAsync(id));
            }
            catch (BusinessValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest("No se pudo eliminar esta categoria");
            }
        }
    }
}