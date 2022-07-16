using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public interface ISerieLeaderboardTranslator
    {
        IList<SerieLeaderboardResponse> Translate(IList<Serie> series, IList<Player> players);
    }
}
