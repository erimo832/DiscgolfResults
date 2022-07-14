using Results.Domain.Model;

namespace Results.Domain.Proxies
{
    public interface IEventsProxy
    {
        IList<Event> GetEvents(Serie serie);
    }
}
