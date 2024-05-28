using System;
using System.Collections.Generic;

namespace SuperConf2024.Entities;

public partial class Inscription
{
    public int InscriptionId { get; set; }

    public string Email { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public DateTime DateNaissance { get; set; }

    public int Nbjours { get; set; }

    public string ChoixRepas { get; set; } = null!;

    public string? DemandeParticuliere { get; set; }

    public bool Soiree { get; set; }
}
