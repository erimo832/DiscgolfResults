using Microsoft.EntityFrameworkCore;
using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    public class RoundRepository : IRoundRepository
    {
        private IDatabaseConfiguration Config { get; }

        public RoundRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<Round> items)
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

        public IList<Round> GetBy(int eventId)
        {
            using (var context = new ResultContext(Config))
            {
                return context.Rounds.Include(x => x.RoundScores).Where(x => x.EventId == eventId).ToList();
            }
        }
    }
}
