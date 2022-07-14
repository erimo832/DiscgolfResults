using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private IDatabaseConfiguration Config { get; }

        public PlayerRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public IList<Player> GetAll()
        {
            using (var context = new ResultContext(Config))
            {
                return context.Players.ToList();
            }
        }

        public void Insert(IList<Player> players)
        {
            using (var context = new ResultContext(Config))
            {
                foreach (var player in players)
                {
                    context.Add(player);
                }

                context.SaveChanges();
            }
        }

        public void Update(Player player)
        {
            using (var context = new ResultContext(Config))
            {
                var result = context.Players.SingleOrDefault(x => x.PlayerId == player.PlayerId);

                if (result != null)
                {
                    result.FirstName = player.FirstName;
                    result.LastName = player.LastName;
                    result.PdgaNumber = player.PdgaNumber;
                    context.SaveChanges();
                }
            }
        }
    }
}
