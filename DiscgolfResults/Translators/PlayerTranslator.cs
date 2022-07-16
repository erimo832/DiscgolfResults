using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public class PlayerTranslator : IPlayerTranslator
    {
        public IList<PlayerResponse> Translate(IList<Player> players)
        {
            return players.Select(x => new PlayerResponse
            { 
                FirstName = x.FirstName,
                LastName = x.LastName,
                PdgaNumber = x.PdgaNumberAsString,
                PlayerId = x.PlayerId
            }).ToList();
        }
    }
}
