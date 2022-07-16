using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public class SerieLeaderboardTranslator : ISerieLeaderboardTranslator
    {
        public IList<SerieLeaderboardResponse> Translate(IList<Serie> series, IList<Player> players)
        {
            var result = new List<SerieLeaderboardResponse>();

            foreach (var serie in series)
            {
                result.Add(new SerieLeaderboardResponse
                {
                    SerieId = serie.SerieId,
                    SerieName = serie.Name,
                    RoundsToCount = serie.RoundsToCount,
                    CtpResults = GetCtpResults(serie, players),
                    HcpResults = GetHcpResults(serie, players),
                    ScoreResults = GetScoreResults(serie, players)
                });
            }

            return result;
        }

        private IList<HcpResult> GetHcpResults(Serie serie, IList<Player> players)
        {
            var dictionary = new Dictionary<int, HcpResult>();

            foreach (var ev in serie.Events)
            {
                foreach (var item in ev.PlayerEvents)
                {
                    if (!dictionary.ContainsKey(item.PlayerId))
                    {
                        dictionary.Add(item.PlayerId, new HcpResult { PlayerId = item.PlayerId, FullName = players.First(x => x.PlayerId == item.PlayerId).FullName, NumberOfEvents = 0, EventPoints = new List<double>(), Placement = 0 });
                    }

                    dictionary[item.PlayerId].EventPoints.Add(item.HcpPoints);
                    dictionary[item.PlayerId].NumberOfEvents++;
                }
            }

            var result = dictionary.Select(x => x.Value).ToList();

            foreach (var item in result)
            {
                //reduce to max numer of rounds
                item.EventPoints = item.EventPoints.OrderByDescending(x => x).Take(serie.RoundsToCount).ToList();
                item.NumberOfEvents = item.NumberOfEvents > serie.RoundsToCount ? serie.RoundsToCount : item.NumberOfEvents;
                item.AvgPoints = item.EventPoints.Average();
                item.MaxPoints = item.EventPoints.Max(x => x);
                item.MinPoints = item.EventPoints.Min(x => x);
                item.TotalPoints = item.EventPoints.Sum(x => x);
                item.TotalHcpScore = 0;
                item.AvgHcpScore = 0;
                throw new NotImplementedException();
            }

            result = result.OrderByDescending(x => x.TotalPoints).ToList();

            var lastTotalPoints = 0.0;
            var lastPlace = 0;
            for (int i = 0; i < result.Count(); i++)
            {
                if (lastTotalPoints == result[i].TotalPoints)
                {
                    result[i].Placement = lastPlace;
                }
                else
                {
                    result[i].Placement = i + 1;
                    lastPlace = i + 1;
                    lastTotalPoints = result[i].TotalPoints;
                }
            }

            return result;
        }

        private IList<ScoreResult> GetScoreResults(Serie serie, IList<Player> players)
        {
            var dictionary = new Dictionary<int, ScoreResult>();

            foreach (var ev in serie.Events)
            {
                foreach (var item in ev.PlayerEvents)
                {
                    if (!dictionary.ContainsKey(item.PlayerId))
                    {
                        dictionary.Add(item.PlayerId, new ScoreResult { PlayerId = item.PlayerId, FullName = players.First(x => x.PlayerId == item.PlayerId).FullName, NumberOfEvents = 0, EventScores = new List<double>(), Placement = 0 });
                    }

                    dictionary[item.PlayerId].EventScores.Add(item.TotalScore);
                    dictionary[item.PlayerId].NumberOfEvents++;
                }
            }

            var result = dictionary.Select(x => x.Value).ToList();

            foreach (var item in result)
            {
                //reduce to max numer of rounds
                item.EventScores = item.EventScores.OrderBy(x => x).Take(serie.RoundsToCount).ToList();
                item.NumberOfEvents = item.NumberOfEvents > serie.RoundsToCount ? serie.RoundsToCount : item.NumberOfEvents;
                item.AvgScore = item.EventScores.Average();
                item.MaxScore = item.EventScores.Max(x => x);
                item.MinScore = item.EventScores.Min(x => x);
                item.TotalScore = item.EventScores.Sum(x => x);
            }

            result = result.OrderByDescending(x => x.NumberOfEvents).ThenBy(x => x.AvgScore).ToList();

            var lastTotalPoints = 0.0;
            var lastPlace = 0;

            //Sort by rounds, then by Avgscore for each distinct # rounds
            for (int i = 0; i < result.Count(); i++)
            {
                if (lastTotalPoints == result[i].AvgScore)
                {
                    result[i].Placement = lastPlace;
                }
                else
                {
                    result[i].Placement = i + 1;
                    lastPlace = i + 1;
                    lastTotalPoints = result[i].AvgScore;
                }
            }

            return result;
        }

        private IList<CtpResult> GetCtpResults(Serie serie, IList<Player> players)
        {
            var dictionary = new Dictionary<int, CtpResult>();

            foreach (var ev in serie.Events)
            {
                foreach (var item in ev.PlayerEvents)
                {
                    if (!dictionary.ContainsKey(item.PlayerId))
                    {
                        dictionary.Add(item.PlayerId, new CtpResult { PlayerId = item.PlayerId, FullName = players.First(x => x.PlayerId == item.PlayerId).FullName, NumberOfCtps = 0, NumberOfEvents = 0, Placement = 0 });
                    }

                    dictionary[item.PlayerId].NumberOfCtps += item.NumberOfCtp;
                    dictionary[item.PlayerId].NumberOfEvents++;
                }
            }

            var result = dictionary.Select(x => x.Value).OrderByDescending(x => x.NumberOfCtps).ToList();

            var lastNumCtp = -1;
            var lastPlace = 0;
            for (int i = 0; i < result.Count(); i++)
            {
                if (lastNumCtp == result[i].NumberOfCtps)
                {
                    result[i].Placement = lastPlace;
                }
                else
                {
                    result[i].Placement = i + 1;
                    lastPlace = i + 1;
                    lastNumCtp = result[i].NumberOfCtps;
                }
            }

            return result;
        }
    }
}
