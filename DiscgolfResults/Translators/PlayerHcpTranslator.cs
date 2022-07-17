using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public class PlayerHcpTranslator : IPlayerHcpTranslator
    {
        public IList<PlayerHcpResponse> Translate(IList<Player> players)
        {
            var result = new List<PlayerHcpResponse>();

            foreach (var player in players)
            {
                var latest = player.PlayerCourseLayoutHcp.OrderByDescending(x => x.EventId).First();

                result.Add(new PlayerHcpResponse 
                {
                    PlayerId = player.PlayerId,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    PdgaNumber = player.PdgaNumberAsString,                    
                    CurrentHcp = latest.HcpAfter,
                    //RoundHcps = new List<PlayerHcpResponse.RoundHcp>() can add details later
                });
            }
            

            return result;
        }
    }
}
