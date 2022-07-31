using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Extensions;
using MathNet.Numerics.Statistics;
using Results.Domain.Model;
using Results.Domain.Service;

namespace DiscgolfResults.Translators
{
    public class PlayerDetailsTranslator : IPlayerDetailsTranslator
    {
        public PlayerDetailsTranslator(IHcpManager hcpManager)
        {
            HcpManager = hcpManager;
        }
        private IHcpManager HcpManager { get; }

        public PlayerDetailsResponse Translate(Player player)
        {
            var results = new List<PlayerResult>();
            var inHcpCalc = HcpManager.GetEventsIncludedInCalculations(player.PlayerEvents).ToDictionary(x => x.EventId);
            var inHcpAvgCalc = HcpManager.GetEventsIncludedInHcpCalculations(player.PlayerEvents).ToDictionary(x => x.EventId);

            var playedEvent = 1;
            foreach (var ev in player.PlayerEvents.OrderBy(x => x.EventId))
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
                    Score = ev.TotalScore,
                    InHcpAvgCalc = inHcpAvgCalc.ContainsKey(ev.EventId),
                    InHcpCalc = inHcpCalc.ContainsKey(ev.EventId),
                    PlayedEvent = playedEvent++
                });
            }
            
            return new PlayerDetailsResponse
            { 
                FirstName = player.FirstName,
                LastName = player.LastName,
                PlayerId = player.PlayerId,
                PdgaNumber = player.PdgaNumberAsString,
                
                BestScore = results.Min(x => x.Score),
                AvgScore = Math.Round(results.Average(x => x.Score), 2),
                WorstScore = results.Max(x => x.Score),

                StdDeviation = Math.Round(results.Select(x => x.Score).ToList().PopulationStandardDeviation(), 2),

                FirstAppearance = player.PlayerEvents.Select(x => x.Event).Min(x => x.StartTime),
                LastAppearance = player.PlayerEvents.Select(x => x.Event).Max(x => x.StartTime),
                CtpPercentage = (Convert.ToDouble(results.Sum(x => x.NumberOfCtps)) / Convert.ToDouble(results.Count)).ToPercent(2),
                TotalCtps = results.Sum(x => x.NumberOfCtps),
                TotalRounds = results.Count,
                WinPercentage = (Convert.ToDouble(results.Count(x => x.Placement == 1)) / Convert.ToDouble(results.Count())).ToPercent(2),
                WinPercentageHcp = (Convert.ToDouble(results.Count(x => x.PlacementHcp == 1)) / Convert.ToDouble(results.Count())).ToPercent(2),

                EventResults = results.OrderByDescending(x => x.StartTime).ToList(),
                ScoreDistibution = GetDistribution(player.PlayerEvents)
                
            };
        }

        private IList<PlayerScoreDistribution> GetDistribution(IList<PlayerEvent> events)
        {
            var min = events.Min(x => x.TotalScore).ToInt();
            var max = events.Max(x => x.TotalScore).ToInt();

            var result = new List<PlayerScoreDistribution>();

            for (int i = min; i < max +1; i++)
            {
                result.Add(new PlayerScoreDistribution { NumberOfTimes = events.Count(x => x.TotalScore.ToInt() == i), Score = i });
            }

            return result;
        }
    }
}
