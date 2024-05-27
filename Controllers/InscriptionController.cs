using Microsoft.AspNetCore.Mvc;
using SuperConf2024.Models;

namespace SuperConf2024.Controllers
{
    public class InscriptionController : Controller
    {
        // GET: Inscription
        public ActionResult Index()
        {
            return View(new InscriptionViewModel());
        }

        // POST: Inscription
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InscriptionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return RedirectToAction(nameof(Succes));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // GET : /Inscription/Succes
        public ActionResult Succes() 
        {
            return Content("Inscription enregistrée ! A très bientôt à SuperConf 2024");
        }
    }
}
