using Results.Domain.Model;
using Results.Domain.Repository;

namespace Results.Domain.Service
{
    internal class EventManager : IEventManager
    {
        private IEventRepository Repository { get; }
        private IPlayerEventRepository PlayerEventRepository { get; }

        public EventManager(IEventRepository repository, IPlayerEventRepository playerEventRepository)
        {
            Repository = repository;
            PlayerEventRepository = playerEventRepository;
        }

        public Event Get(string eventName, int serieId, DateTime time)
        {
            var ev = Repository.Get(eventName, serieId, time);

            if (ev != null)
                return ev;

            return new Event
            {
                EventName = eventName,
                SerieId = serieId,
                StartTime = time
            };
        }

        public void Insert(IList<Event> items)
        {
            Repository.Insert(items);
        }

        public void UpdateEventResults()
        {
            var events = Repository.GetBy(includePlayerhcp: true, includeRounds: true);

            var results = new List<PlayerEvent>();

            foreach (var ev in events)
            {
                var eventResults = new List<PlayerEvent>();

                foreach (var playerHcp in ev.PlayerCourseLayoutHcp)
                {
                    eventResults.Add(new PlayerEvent
                    {
                        EventId = playerHcp.EventId,
                        PlayerId = playerHcp.PlayerId,
                        TotalScore = ev.Rounds.SelectMany(x => x.RoundScores).Where(x => x.PlayerId == playerHcp.PlayerId).Sum(z => z.Score),
                        TotalHcpScore = ev.Rounds.SelectMany(x => x.RoundScores).Where(x => x.PlayerId == playerHcp.PlayerId).Sum(z => z.Score) - playerHcp.HcpBefore, //Is a bug here if multiple rounds on different layouts
                        NumberOfCtp = ev.Rounds.SelectMany(x => x.RoundScores).Where(x => x.PlayerId == playerHcp.PlayerId).Sum(z => z.NumberOfCtps)
                    });
                }

                int pos = 0;
                double lastScore = 0;
                var sorted = eventResults.OrderBy(x => x.TotalScore).ToList();

                for (int i = 0; i < eventResults.Count; i++)
                {
                    if (sorted[i].TotalScore != lastScore)
                        pos = i + 1;

                    sorted[i].Placement = pos;
                    lastScore = sorted[i].TotalScore;
                }

                lastScore = 0;
                var sortedHcp = eventResults.OrderBy(x => x.TotalHcpScore).ToList();

                for (int i = 0; i < eventResults.Count; i++)
                {
                    if (sortedHcp[i].TotalHcpScore != lastScore)
                        pos = i + 1;

                    sortedHcp[i].PlacementHcp = pos;
                    sortedHcp[i].HcpPoints = GetPoints(pos, eventResults.Count);
                    lastScore = sortedHcp[i].TotalHcpScore;
                }


                results.AddRange(eventResults);
            }

            PlayerEventRepository.Insert(results);
        }

        public double GetPoints(int place, int numberOfParticipants)
        {
            var maxScore = 100 + numberOfParticipants * 0.1;

            return maxScore - (place - 1);
        }

        public IList<PlayerEvent> GetPlayerEvents(int playerId)
        {
            return PlayerEventRepository.GetPlayerEvents(playerId);
        }

        public IList<Event> GetBy(int seriesId = -1, int playerId = -1, bool includeRounds = false, bool includePlayerEvents = false, bool includePlayerhcp = false)
        {
            return Repository.GetBy(seriesId, playerId, includeRounds, includePlayerEvents, includePlayerhcp);
        }
    }
}
