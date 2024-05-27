using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperConf2024.Models
{
    public class InscriptionViewModel
    {
        [DisplayName("Adresse e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Nom de famille")]
        public string Nom { get; set; }

        [DisplayName("Prénom")]
        public string Prenom { get; set; }

        [DisplayName("Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; } = new DateTime(2000, 01, 01);

        [DisplayName("Nombre de jours de conférence (2 ou 3")]
        [Range(2, 3)]
        [DefaultValue(3)]
        public int NbJours { get; set; } = 3;

        [DisplayName("Choix du type de repas")]
        public OptionsRepas ChoixRepas { get; set; }

        [DisplayName("Demande particulière pour les repas")]
        [DataType(DataType.MultilineText)]
        [StringLength(180)]
        public string? DemandeParticuliere { get; set; }

        [DisplayName("Je souhaite participer à la soirée du 2ème jour")]
        public bool Soiree { get; set; } = true;

        public static IEnumerable<SelectListItem> GetOptionsRepas()
        {
            yield return new SelectListItem("Standard", OptionsRepas.Standard.ToString(), true);
            yield return new SelectListItem("Végétarien", OptionsRepas.Vegetarien.ToString(), false);
            yield return new SelectListItem("Vegan", OptionsRepas.Vegan.ToString(), false);
        }
    }

    public enum OptionsRepas
    {
        Standard,
        Vegetarien,
        Vegan
    }
}
