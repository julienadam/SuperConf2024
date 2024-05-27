using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperConf2024.Controllers
{
    public class InscriptionController : Controller
    {
        // GET: InscriptionController
        public ActionResult Index()
        {
            return Content("Ici le formulaire d'inscription");
            // return View();
        }

        // GET: InscriptionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InscriptionController/Create
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
    }
}
