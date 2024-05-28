using SuperConf2024.Entities;

namespace SuperConf2024.Services
{
    public class FakeInscriptionService : IInscriptionService
    {
        private readonly int capa;
        private readonly HashSet<string> emails = [];

        public FakeInscriptionService(IConfiguration config)
        {
            this.capa = int.Parse(config["Capacite"] ?? "0");
        }

        public int Enregistrer(Inscription viewModel)
        {
            emails.Add(viewModel.Email);
            return emails.Count;
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
