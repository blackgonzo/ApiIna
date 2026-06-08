using inaApp.Common.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inaApp.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClienteController : Controller
    {
        // GET: clienteController

       [HttpGet]
        public ActionResult Index()
        {
            return Ok("salio bien la prueba");
        }

        // GET: clienteController/Details/5

       
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: clienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: clienteController/Create
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

        // GET: clienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: clienteController/Edit/5
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

        // GET: clienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: clienteController/Delete/5
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
