using SuperConf2024.Controllers;
using SuperConf2024.Services;
using SuperConf2024.Models;
using Microsoft.AspNetCore.Mvc;
using SuperConf2024.Entities;

namespace Tests;

public class InscriptionControllerTests
{
    private const string TEST_EMAIL = "foo@example.com";

    [Fact]
    public void Quand_plus_de_places_dispos_rediriger_vers_page_de_surcapacite()
    {
        var fakeInscription = new FakeInscriptionService(0);
        InscriptionController controller = new InscriptionController(fakeInscription);
        var result = controller.Index(new InscriptionViewModel());
        
        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(InscriptionController.Surcapacite), redirect.ActionName);
    }

    [Fact]
    public void Si_email_deja_inscrit_afficher_erreur_sur_le_champ_email()
    {
        var fakeInscription = new FakeInscriptionService(2);
        fakeInscription.Enregistrer(new Inscription { Email = TEST_EMAIL });
        InscriptionController controller = new InscriptionController(fakeInscription);
        var model = new InscriptionViewModel { Email = TEST_EMAIL };
        var result = controller.Index(model);

        var view = Assert.IsType<ViewResult>(result);
        Assert.True(view.ViewData.ModelState.TryGetValue(nameof(InscriptionViewModel.Email), out var errors));
        var error = Assert.Single(errors.Errors);
        Assert.Equal("Cet email est déja inscrit.", error.ErrorMessage);
    }
}