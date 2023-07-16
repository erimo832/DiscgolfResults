using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;
using Results.Domain.Model.ReadObjects;

namespace Results.Domain.Repository
{
    public class RoundScoreRepository : IRoundScoreRepository
    {
        private IDatabaseConfiguration Config { get; }

        public RoundScoreRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<RoundScore> items)
        {
            using (var context = new ResultContext(Config))
            {
                foreach (var item in items)
                {
                    context.Add(item);
                }

                context.SaveChanges();
            }
        }

        public IList<RoundScoreRo> GetScoresBy(int playerId, int courseLayoutId)
        {
            using (var context = new ResultContext(Config))
            {
                return context.RoundScore
                    //.Include(x => x.Round)
                    .Join(
                    context.Rounds,
                    rs => rs.RoundId,
                    r => r.RoundId,
                    (rs, r) => new RoundScoreRo
                    {
                        CourseLayoutId = r.CourseLayoutId,
                        EventId = r.EventId,
                        StartTime = r.StartTime,
                        RoundId = rs.RoundId,
                        Division = rs.Division,
                        HoleResults = rs.HoleResults,
                        NumberOfCtps = rs.NumberOfCtps,
                        PlayerId = rs.PlayerId,
                        RoundHcp = rs.RoundHcp,
                        RoundScoreId = rs.RoundScoreId,
                        Score = rs.Score
                    })
                    //.Where(x => x.PlayerId == playerId && x.Round.CourseLayoutId == courseLayoutId).ToList();
                    .If(playerId != -1, q => q.Where(x => x.PlayerId == playerId))
                    .If(courseLayoutId != -1, q => q.Where(x => x.CourseLayoutId == courseLayoutId))
                    .ToList();
            }
        }
    }
}
