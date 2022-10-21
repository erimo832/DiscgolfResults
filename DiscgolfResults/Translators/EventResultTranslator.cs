using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
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
                        NumberOfCtps = pe.NumberOfCtp,
                        Placement = pe.Placement,
                        PlacementHcp = pe.PlacementHcp,
                        HcpBefore = hcp.HcpBefore,
                        HcpAfter = hcp.HcpAfter,
                        PlayerId = pe.PlayerId,
                        Points = pe.HcpPoints,
                        Score = pe.TotalScore,
                        HcpScore = pe.TotalHcpScore,
                        Division = pe.Division
                    });
                }

                var divisionResults = eventResults.GroupBy(x => x.Division).Select(x => new Division
                {
                    Name = x.Key,
                    Results = x.Select(y => y).ToList().OrderBy(x => x.PlacementHcp).ToList()
                }).ToList();

                result.Add(new EventResultResponse
                {
                    EventId = ev.EventId,
                    EventName = ev.EventName,
                    StartTime = ev.StartTime,
                    Divisions = divisionResults
                });
            }

            return result.OrderByDescending(x => x.StartTime).ToList();
        }

        public IList<EventResponse> Translate(IList<Event> events)
        {
            return events.Select(x => new EventResponse
            {
                EventId = x.EventId,
                EventName = x.EventName,
                SerieId = x.SerieId,
                StartTime = x.StartTime
            }).ToList();
        }
    }
}
