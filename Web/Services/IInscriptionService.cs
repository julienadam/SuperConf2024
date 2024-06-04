using SuperConf2024.Entities;

namespace SuperConf2024.Services
{
    public interface IInscriptionService
    {
        int Enregistrer(Inscription inscription);
        bool IsEmailUnique(string email);
        bool HasPlacesDisponibles();
        int PlacesRestantes();
    }
}
