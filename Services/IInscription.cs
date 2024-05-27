using SuperConf2024.Models;

namespace SuperConf2024.Services
{
    public interface IInscription
    {
        bool Enregistrer(InscriptionViewModel viewModel);
        bool IsEmailUnique(string email);
        bool HasPlacesDisponibles();
        int PlacesRestantes();
    }

    public class FakeInscription : IInscription
    {
        private readonly int capa;
        private readonly HashSet<string> emails = [];

        public FakeInscription(int capa)
        {
            this.capa = capa;
        }

        public bool Enregistrer(InscriptionViewModel viewModel)
        {
            emails.Add(viewModel.Email);
            return true;
        }

        public bool HasPlacesDisponibles()
        {
            return PlacesRestantes() > 0;
        }

        public int PlacesRestantes()
        {
            return capa - emails.Count;
        }

        public bool IsEmailUnique(string email)
        {
            if(emails.Contains(email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
