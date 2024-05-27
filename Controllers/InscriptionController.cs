using Microsoft.AspNetCore.Mvc;
using SuperConf2024.Models;
using SuperConf2024.Services;

namespace SuperConf2024.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly IInscription inscription;

        public InscriptionController(IInscription inscription)
        {
            this.inscription = inscription;
        }

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
            if(!inscription.HasPlacesDisponibles())
            {
                return RedirectToAction("Surcapacite");
            }

            if(!inscription.IsEmailUnique(viewModel.Email))
            {
                ModelState.AddModelError(nameof(InscriptionViewModel.Email), "Cet email est déja inscrit.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    inscription.Enregistrer(viewModel);
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

        public ActionResult Surcapacite()
        {
            return Content("Toutes les places ont été réservées, veuillez tenter votre chance plus tard !");
        }
    }
}
