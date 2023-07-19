using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    internal class EventScoreRepository : IEventScoreRepository
    {
        private IDatabaseConfiguration Config { get; }

        public EventScoreRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<EventScore> items)
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

        public IList<EventScore> GetPlayerEvents(int playerId)
        {
            using (var context = new ResultContext(Config))
            {
                return context.EventScores.Where(x => x.PlayerId == playerId).ToList();
            }
        }
    }
}
