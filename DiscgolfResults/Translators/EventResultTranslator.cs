using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Transformers
{
    public class EventResultTranslator : IEventResultTranslator
    {
        public IList<EventResultResponse> Translate(IList<Event> events, IList<Player> players)
        {
            var result = new List<EventResultResponse>();

            foreach (var ev in events)
            {
                var eventResults = new List<Result>();
                foreach (var pe in ev.PlayerEvents)
                {
                    var hcp = ev.PlayerCourseLayoutHcp.First(x => x.PlayerId == pe.PlayerId && x.EventId == pe.EventId);
                    var player = players.First(x => x.PlayerId == pe.PlayerId);

                    eventResults.Add(new Result
                    {
                        FullName = player.FullName,                        
                        NumberOfCtps = -1,
                        Placement = pe.Placement,
                        PlacementHcp = pe.PlacementHcp,
                        HcpBefore = hcp.HcpBefore,
                        HcpAfter = hcp.HcpAfter,
                        PlayerId = pe.PlayerId,
                        Points = pe.HcpPoints,
                        Score = pe.TotalScore,
                        HcpScore = pe.TotalHcpScore,
                    });
                } 

                result.Add(new EventResultResponse
                {
                    EventId = ev.EventId,
                    EventName = ev.EventName,
                    StartTime = ev.StartTime,
                    Results = eventResults.OrderBy(x => x.PlacementHcp).ToList()
                });
            }

            return result.OrderByDescending(x => x.StartTime).ToList();
        }
    }
}
