using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public interface IPlayerHcpTranslator
    {
        IList<PlayerHcpResponse> Translate(IList<Player> players);
    }
}
