using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    internal class PlayerEventRepository : IPlayerEventRepository
    {
        private IDatabaseConfiguration Config { get; }

        public PlayerEventRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<PlayerEvent> items)
        {
            using (var context = new ResultContext(Config))
            {
                foreach (var item in items)
                {
                    context.Add(item);
                }

                context.SaveChanges();
            }
        }

        public IList<PlayerEvent> GetPlayerEvents(int playerId)
        {
            using (var context = new ResultContext(Config))
            {
                return context.PlayerEvents.Where(x => x.PlayerId == playerId).ToList();
            }
        }
    }
}
