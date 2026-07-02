using AutoMapper;
using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.DTOs.Categoria;
using inaApp.DTOs.Producto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using ProyectoINAApp.Models.Producto;

namespace ProyectoINAApp.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO> _productoService;
        private readonly IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> _categoriaService;
        private readonly IMapper _mapper;

        public ProductoController(
            IGenericService<ProductoResponseDTO, ProductoCreateDTO, ProductoUpdateDTO> productoService,
            IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> categoriaService,
            IMapper mapper)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        private async Task<List<SelectListItem>> CargarCategoriasAsync()
        {
            var categorias = await _categoriaService.obtenerTodosAsync();
            return categorias.Data.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre
            }).ToList();
        }

        public async Task<ActionResult> Index()
        {

            try
            {

                //requiero ir a todos los productos y mostrarlos en la vista
                var lista = await _productoService.obtenerTodosAsync();

                var productos = _mapper.Map<List<ProductoIndexViewModel>>(lista.Data);

                return View(productos);
            }
            catch (NotFoundException)
            {
                //model: pasar datos, dtos, ntities, viewModels, listados, etc
                //viewBag: pasar datos simples, mensajes, etc
                //viewData: pasar datos simples, mensajes, etc
                ViewBag.Message = "No hay productos disponibles.";
                return View();

            }
            catch (Exception ex) {

                ViewBag.Message = "Ocurrió un error al obtener los productos: " + ex.Message;
                return View();
            }

        }



        // GET: ProductoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var response = await _productoService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            var productoVM = _mapper.Map<ProductoIndexViewModel>(response.Data);
            return View(productoVM);
        }

        public async Task<ActionResult> Create()
        {
            var vm = new ProductoCreateViewModel();
            vm.Categorias = await CargarCategoriasAsync();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductoCreateViewModel productoVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    productoVM.Categorias = await CargarCategoriasAsync();
                    return View(productoVM);
                }

                var productoDTO = _mapper.Map<ProductoCreateDTO>(productoVM);
                var response = await _productoService.CrearAsync(productoDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    productoVM.Categorias = await CargarCategoriasAsync();
                    return View(productoVM);
                }

                TempData["Message"] = "Producto creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateNameException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidPriceException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidStockException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrio un error al crear el producto.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var response = await _productoService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            var productoVM = _mapper.Map<ProductoEditViewModel>(response.Data);
            productoVM.CategoriaId = response.Data.Categoria.Id;
            productoVM.Categorias = await CargarCategoriasAsync();
            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProductoEditViewModel productoVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    productoVM.Categorias = await CargarCategoriasAsync();
                    return View(productoVM);
                }

                var productoDTO = _mapper.Map<ProductoUpdateDTO>(productoVM);
                var response = await _productoService.ActualizarAsync(id, productoDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    productoVM.Categorias = await CargarCategoriasAsync();
                    return View(productoVM);
                }

                TempData["Message"] = "Producto actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateNameException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidPriceException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidStockException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrio un error al actualizar el producto.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await _productoService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            var productoVM = _mapper.Map<ProductoIndexViewModel>(response.Data);
            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _productoService.EliminarAsync(id);
                if (!response.Success)
                {
                    TempData["Error"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }

                TempData["Message"] = "Producto eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                TempData["Error"] = "El producto no existe.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrio un error al eliminar el producto.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
