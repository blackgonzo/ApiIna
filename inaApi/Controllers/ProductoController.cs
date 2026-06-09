
using inaApp.Common.Interfaces;
using inaApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductoController : Controller
    {

        private readonly IGenericService<Producto> _productoService;


        public ProductoController(IGenericService<Producto> productoServ)
        {
            _productoService = productoServ;
        }




        // GET: ProductoController
        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {

            var lista = await _productoService.ObtenerTodosAsync();

            return Ok(lista);
        }



        // GET: ProductoController/Details/5
        [HttpGet("getById/{id}")]

        public ActionResult Details(int id)
        {
            return View();
        }


        //METODO PARA ACTUALIZAR UN PRODUCTO


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Producto producto)
        {
            try
            {
                producto.Id = id;
                var result = await _productoService.ActualizarAsync(producto);
                return Ok(new { data = result, message = "ACTUALIZADO" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: ProductoController/Create

        [HttpPost]
        public ActionResult Create([FromBody] Producto producto)
        {

            try
            {
                var result = _productoService.CrearAsync(producto);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
