using Microsoft.AspNetCore.Mvc;
using SuperConf2024.Entities;
using SuperConf2024.Models;
using SuperConf2024.Services;

namespace SuperConf2024.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly IInscriptionService inscriptionService;

        public InscriptionController(IInscriptionService inscriptionService)
        {
            this.inscriptionService = inscriptionService;
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
            if(!inscriptionService.HasPlacesDisponibles())
            {
                return RedirectToAction("Surcapacite");
            }

            if(!inscriptionService.IsEmailUnique(viewModel.Email))
            {
                ModelState.AddModelError(nameof(InscriptionViewModel.Email), "Cet email est déja inscrit.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var inscription = new Inscription
                    {
                        Email = viewModel.Email,
                        Nbjours = viewModel.NbJours,
                        DemandeParticuliere = viewModel.DemandeParticuliere,
                        DateNaissance = viewModel.DateNaissance,
                        Prenom = viewModel.Prenom,
                        Nom = viewModel.Nom,
                        ChoixRepas = viewModel.ChoixRepas.ToString(),
                        Soiree = viewModel.Soiree
                    };

                    inscriptionService.Enregistrer(inscription);
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
