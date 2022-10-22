using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public class SerieLeaderboardTranslator : ISerieLeaderboardTranslator
    {
        private const int _numDecimalsAvg = 2;
        private const int _numDecimalsSum = 1;

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
                    DivisionResults = GetDivisionResults(serie, players)
                });
            }

            return result;
        }

        private IList<DivisionResult> GetDivisionResults(Serie serie, IList<Player> players)
        {
            var divisions = serie.Events.SelectMany(x => x.PlayerEvents).Select(x => x.Division).Distinct();

            var result = new List<DivisionResult>();

            foreach (var division in divisions)
            {
                result.Add(new DivisionResult 
                {
                    Division = division,
                    HcpResults = GetHcpResults(serie, players, division).OrderBy(x => x.Placement).ToList(),
                    ScoreResults = GetScoreResults(serie, players, division).OrderBy(x => x.Placement).ToList(),
                    CtpResults = GetCtpResults(serie, players, division)
                });
            }

            return result;
        }

        private IList<HcpResult> GetHcpResults(Serie serie, IList<Player> players, string division)
        {
            var dictionary = new Dictionary<int, HcpResult>();

            foreach (var ev in serie.Events)
            {
                foreach (var item in ev.PlayerEvents.Where(x => x.Division == division))
                {
                    if (!dictionary.ContainsKey(item.PlayerId))
                    {
                        dictionary.Add(item.PlayerId, new HcpResult { PlayerId = item.PlayerId, FullName = players.First(x => x.PlayerId == item.PlayerId).FullName, NumberOfEvents = 0, EventPoints = new List<HcpResult.EventPoint>(), Placement = 0 });
                    }

                    dictionary[item.PlayerId].EventPoints.Add(new HcpResult.EventPoint { Points = item.HcpPoints, HcpScore = item.TotalHcpScore });
                    dictionary[item.PlayerId].NumberOfEvents++;
                }
            }

            var result = dictionary.Select(x => x.Value).ToList();

            foreach (var item in result)
            {
                //reduce to max numer of rounds
                item.EventPoints = item.EventPoints.OrderByDescending(x => x.Points).Take(serie.RoundsToCount).ToList();
                item.NumberOfEvents = item.NumberOfEvents > serie.RoundsToCount ? serie.RoundsToCount : item.NumberOfEvents;
                item.AvgPoints = Math.Round(item.EventPoints.Select(x => x.Points).Average(), _numDecimalsAvg);
                item.MaxPoints = item.EventPoints.Select(x => x.Points).Max(x => x);
                item.MinPoints = item.EventPoints.Select(x => x.Points).Min(x => x);
                item.TotalPoints = Math.Round(item.EventPoints.Sum(x => x.Points), _numDecimalsSum);
                item.TotalHcpScore = Math.Round(item.EventPoints.Sum(x => x.HcpScore), _numDecimalsSum);
                item.AvgHcpScore = Math.Round(item.EventPoints.Select(x => x.HcpScore).Average(), _numDecimalsAvg);
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

        private IList<ScoreResult> GetScoreResults(Serie serie, IList<Player> players, string division)
        {
            var dictionary = new Dictionary<int, ScoreResult>();

            foreach (var ev in serie.Events)
            {
                foreach (var item in ev.PlayerEvents.Where(x => x.Division == division))
                {
                    if (!dictionary.ContainsKey(item.PlayerId))
                    {
                        dictionary.Add(item.PlayerId, new ScoreResult { PlayerId = item.PlayerId, FullName = players.First(x => x.PlayerId == item.PlayerId).FullName, NumberOfEvents = 0, EventScores = new List<ScoreResult.EventScore>(), Placement = 0 });
                    }

                    dictionary[item.PlayerId].EventScores.Add(new ScoreResult.EventScore { Score = item.TotalScore });
                    dictionary[item.PlayerId].NumberOfEvents++;
                }
            }

            var result = dictionary.Select(x => x.Value).ToList();

            foreach (var item in result)
            {
                //reduce to max numer of rounds
                item.EventScores = item.EventScores.OrderBy(x => x.Score).Take(serie.RoundsToCount).ToList();
                item.NumberOfEvents = item.NumberOfEvents > serie.RoundsToCount ? serie.RoundsToCount : item.NumberOfEvents;
                item.AvgScore = Math.Round(item.EventScores.Select(x => x.Score).Average(), _numDecimalsAvg);
                item.MaxScore = item.EventScores.Select(x => x.Score).Max(x => x);
                item.MinScore = item.EventScores.Select(x => x.Score).Min(x => x);
                item.TotalScore = Math.Round(item.EventScores.Sum(x => x.Score), _numDecimalsSum);
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

        private IList<CtpResult> GetCtpResults(Serie serie, IList<Player> players, string division)
        {
            var dictionary = new Dictionary<int, CtpResult>();

            foreach (var ev in serie.Events)
            {
                foreach (var item in ev.PlayerEvents.Where(x => x.Division == division))
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
