using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface IEventRepository
    {
        void Insert(IList<Event> items);
        Event Get(string eventName, int serieId, DateTime time);
        IList<Event> GetAll(bool includeRounds = false, bool includePlayerEvents = false, bool includePlayerhcp = false);
    }
}
