using Microsoft.EntityFrameworkCore;
using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    public class SerieRepository : ISerieRepository
    {
        private IDatabaseConfiguration Config { get; }

        public SerieRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<Serie> items)
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

        public IList<Serie> GetBy(int seriesId = -1, bool includeEvents = false, bool includePlayerEvents = false, bool includePlayerhcp = false)
        {
            using (var context = new ResultContext(Config))
            {
                var series = context.Series
                    .If(includeEvents, q => q.Include(x => x.Events))
                    .If(seriesId != -1, q => q.Where(x => x.SerieId == seriesId))
                    .ToList();

                //Do it manually
                if (includeEvents)
                {
                    foreach (var serie in series)
                    {
                        foreach (var ev in serie.Events)
                        {
                            ev.PlayerEvents = includePlayerEvents ? context.PlayerEvents.Where(x => x.EventId == ev.EventId).ToList() : new List<PlayerEvent>();
                            ev.PlayerCourseLayoutHcp = includePlayerhcp ? context.PlayerCourseLayoutHcps.Where(x => x.EventId == ev.EventId).ToList() : new List<PlayerCourseLayoutHcp>();
                        }
                    }
                }

                return series;
            }
        }
    }
}
