using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public class PlayerDetailsTranslator : IPlayerDetailsTranslator
    {
        public PlayerDetailsResponse Translate(Player player)
        {
            var results = new List<PlayerResult>();

            foreach (var ev in player.PlayerEvents)
            {
                var hcp = player.PlayerCourseLayoutHcp.First(x => x.EventId == ev.EventId && x.PlayerId == ev.PlayerId);
                

                results.Add(new PlayerResult 
                {
                    EventId = ev.EventId,
                    EventName = ev.Event.EventName,
                    StartTime = ev.Event.StartTime,
                    HcpAfter = hcp.HcpAfter,
                    HcpBefore = hcp.HcpBefore,
                    HcpScore = ev.TotalHcpScore,
                    NumberOfCtps = ev.NumberOfCtp,
                    Placement = ev.Placement,
                    PlacementHcp = ev.PlacementHcp,
                    Points = ev.HcpPoints,
                    Score = ev.TotalScore
                });
            }

            return new PlayerDetailsResponse
            { 
                FirstName = player.FirstName,
                LastName = player.LastName,
                PlayerId = player.PlayerId,
                PdgaNumber = player.PdgaNumberAsString,
                
                BestScore = results.Max(x => x.Score),
                AvgScore = results.Average(x => x.Score),
                WorstScore = results.Max(x => x.Score),

                FirstAppearance = player.PlayerEvents.Select(x => x.Event).Min(x => x.StartTime),
                LastAppearance = player.PlayerEvents.Select(x => x.Event).Max(x => x.StartTime),
                CtpPercentage = Convert.ToDouble(results.Sum(x => x.NumberOfCtps)) / Convert.ToDouble(results.Count),
                TotalCtps = results.Sum(x => x.NumberOfCtps),
                TotalRounds = results.Count,
                
                EventResults = results.OrderByDescending(x => x.StartTime).ToList()
            };
        }
    }
}
