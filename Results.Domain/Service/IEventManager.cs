using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface IEventManager
    {
        Event Get(string eventName, int serieId, DateTime time);
        void UpdateEventResults();
        void Insert(IList<Event> items);
    }
}
