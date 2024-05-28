using SuperConf2024.Entities;

namespace SuperConf2024.Services
{
    public class DbInscriptionService : IInscriptionService
    {
        private readonly int capa;
        private readonly SuperconfdbContext context;

        public DbInscriptionService(IConfiguration config, SuperconfdbContext context)
        {
            this.capa = int.Parse(config["Capacite"] ?? "0");
            this.context = context;
        }

        public int Enregistrer(Inscription viewModel)
        {
            context.Inscriptions.Add(viewModel);
            context.SaveChanges();
            return viewModel.InscriptionId;
        }

        public bool HasPlacesDisponibles()
        {
            return PlacesRestantes() > 0;
        }

        public int PlacesRestantes()
        {
            return capa - context.Inscriptions.Count();
        }

        public bool IsEmailUnique(string email)
        {
            if(context.Inscriptions.Any(i => i.Email == email))
            {
                return false;
            }

            return true;
        }
    }
}
