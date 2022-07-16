using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Transformers
{
    public interface IEventResultTranslator
    {
        IList<EventResultResponse> Translate(IList<Event> events, IList<Player> players);
    }
}
