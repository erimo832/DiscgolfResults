using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface IEventRepository
    {
        void Insert(IList<Event> items);
        Event Get(string eventName, int serieId, DateTime time);
        IList<Event> GetBy(int serieId = -1, int playerId = -1, int fromEventId = -1, int toEventId = -1, bool includeRounds = false, bool includePlayerEvents = false, bool includePlayerhcp = false);
    }
}
