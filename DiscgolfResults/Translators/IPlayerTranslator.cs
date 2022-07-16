using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public interface IPlayerTranslator
    {
        IList<PlayerResponse> Translate(IList<Player> players);
    }
}
