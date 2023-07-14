using Results.Domain.Model;
using Results.Domain.Proxies.Contracts;

namespace Results.Domain.Proxies
{
    public interface IEventsProxy
    {
        IList<Event> GetEvents(SerieExternal serie);
    }
}
