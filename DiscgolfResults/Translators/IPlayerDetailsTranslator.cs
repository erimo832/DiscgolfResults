using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public interface IPlayerDetailsTranslator
    {
        PlayerDetailsResponse Translate(Player player);
    }
}
