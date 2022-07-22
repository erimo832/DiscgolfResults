using Microsoft.EntityFrameworkCore;
using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    public class EventRepository : IEventRepository
    {
        private IDatabaseConfiguration Config { get; }

        public EventRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<Event> items)
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

        public Event Get(string eventName, int serieId, DateTime time)
        {
            using (var context = new ResultContext(Config))
            {
                return context.Events.FirstOrDefault(x => x.EventName == eventName && x.SerieId == serieId && x.StartTime == time);
            }
        }

        public IList<Event> GetBy(int serieId = -1, int playerId = -1, bool includeRounds = false, bool includePlayerEvents = false, bool includePlayerhcp = false)
        {
            using (var context = new ResultContext(Config))
            {
                return context.Events
                    .If(includeRounds, q => q.Include(x => x.Rounds).ThenInclude(y => y.RoundScores))
                    .If(includePlayerEvents, q => q.Include(x => x.PlayerEvents))
                    .If(includePlayerhcp, q => q.Include(x => x.PlayerCourseLayoutHcp))
                    .If(serieId != -1, q => q.Where(x => x.SerieId == serieId))
                    .If(playerId != -1, q => q
                        .Join(context.Rounds, e => e.EventId, r => r.EventId, (e, r) => new { Event = e, r.RoundId })
                        .Join(context.RoundScore, e => e.RoundId, rs => rs.RoundId, (e, rs) => new { Event = e.Event, rs.PlayerId })
                        .Where(x => x.PlayerId == playerId)
                        .Select(x => x.Event)
                    )
                    .ToList();
            }
        }
    }
}
