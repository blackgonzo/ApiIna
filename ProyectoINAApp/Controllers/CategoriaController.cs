using AutoMapper;
using inaApp.Common.Exceptions;
using inaApp.Common.Interfaces;
using inaApp.DTOs.Categoria;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ProyectoINAApp.Models.Categoria;

namespace ProyectoINAApp.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(
            IGenericService<CategoriaResponseDTO, CategoriaCreateDTO, CategoriaUpdateDTO> categoriaService,
            IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var lista = await _categoriaService.obtenerTodosAsync();
                var categorias = _mapper.Map<List<CategoriaIndexViewModel>>(lista.Data);
                return View(categorias);
            }
            catch (NotFoundException)
            {
                ViewBag.Message = "No hay categorias disponibles.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrio un error al obtener las categorias: " + ex.Message;
                return View();
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            var response = await _categoriaService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            var categoriaVM = _mapper.Map<CategoriaIndexViewModel>(response.Data);
            return View(categoriaVM);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoriaCreateViewModel categoriaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(categoriaVM);
                }

                var categoriaDTO = _mapper.Map<CategoriaCreateDTO>(categoriaVM);
                var response = await _categoriaService.CrearAsync(categoriaDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(categoriaVM);
                }

                TempData["Message"] = "Categoria creada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateNameException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessValidationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrio un error al crear la categoria.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var response = await _categoriaService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            var categoriaVM = _mapper.Map<CategoriaEditViewModel>(response.Data);
            return View(categoriaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoriaEditViewModel categoriaVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(categoriaVM);
                }

                var categoriaDTO = _mapper.Map<CategoriaUpdateDTO>(categoriaVM);
                var response = await _categoriaService.ActualizarAsync(id, categoriaDTO);

                if (!response.Success)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(categoriaVM);
                }

                TempData["Message"] = "Categoria actualizada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateNameException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessValidationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                TempData["Error"] = "La categoria no existe.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrio un error al actualizar la categoria.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await _categoriaService.ObtenerPorIdAsync(id);
            if (!response.Success)
            {
                TempData["Error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            var categoriaVM = _mapper.Map<CategoriaIndexViewModel>(response.Data);
            return View(categoriaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _categoriaService.EliminarAsync(id);
                if (!response.Success)
                {
                    TempData["Error"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }

                TempData["Message"] = "Categoria eliminada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                TempData["Error"] = "La categoria no existe.";
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessValidationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrio un error al eliminar la categoria.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
