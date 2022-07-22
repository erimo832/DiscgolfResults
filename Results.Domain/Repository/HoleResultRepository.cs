using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;
using Results.Domain.Model.ReadObjects;
using Microsoft.EntityFrameworkCore;
using Results.Domain.Common.Extensions;

namespace Results.Domain.Repository
{
    public class HoleResultRepository : IHoleResultRepository
    {
        private IDatabaseConfiguration Config { get; }

        public HoleResultRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<HoleResult> items)
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

        public IList<HoleResultRo> GetRoBy(int playerId = -1, int eventId = -1, int courseHoleId = -1)
        {
            using (var context = new ResultContext(Config))
            {
                return context.HoleResultRoView.FromSqlInterpolated($@"select 	
	                    se.SerieId,
	                    s.PlayerId, 
	                    e.EventId, 
	                    e.EventName, 
	                    ch.Number as HoleNumber,
	                    ch.Par, 	
	                    hs.* 
                    from HoleResult hs
                    inner join CourseHoles ch on ch.CourseHoleId=hs.CourseHoleId
                    inner join RoundScore s on s.RoundScoreId=hs.RoundScoreId
                    inner join Rounds r on r.RoundId=s.RoundId
                    inner join Events e on e.EventId=r.EventId
                    inner join Series se on se.SerieId=e.SerieId
                    where playerid={playerId}")
                    .ToList();
            }
        }

        public IList<AverageHoleResultRo> GetAverageRoBy(int playerId = -1, int fromEventId = -1, int toEventId = -1)
        {
            using (var context = new ResultContext(Config))
            {
                return context.HoleResult
                    .Join(context.CourseHoles, hr => hr.CourseHoleId, ch => ch.CourseHoleId, (hr, ch) => new { hr.CourseHoleId, hr.Score, ch.Par, ch.Number, hr.RoundScoreId })
                    .Join(context.RoundScore, x => x.RoundScoreId, rs => rs.RoundScoreId, (x, rs) => new { x.CourseHoleId, x.Score, x.Par, x.Number, rs.PlayerId, rs.RoundId })
                    .Join(context.Rounds, x => x.RoundId, rs => rs.EventId, (x, rs) => new { x.CourseHoleId, x.Score, x.Par, x.Number, x.PlayerId, rs.EventId })
                    .If(playerId != -1, q => q.Where(x => x.PlayerId == playerId))
                    .If(fromEventId != -1, q => q.Where(x => x.EventId >= fromEventId))
                    .If(toEventId != -1, q => q.Where(x => x.EventId <= toEventId))
                    .GroupBy(x => new { x.CourseHoleId, x.Par, x.Number, x.PlayerId })
                    .Select(x => new AverageHoleResultRo
                    {
                        CourseHoleId = x.Key.CourseHoleId,
                        HoleNumber = x.Key.Number,
                        Par = x.Key.Par,
                        PlayerId = x.Key.PlayerId,
                        AverageScore = x.Average(x => x.Score)
                    }).ToList();

                /*
                return
                    (from hr in context.HoleResult
                    join ch in context.CourseHoles on hr.CourseHoleId equals ch.CourseHoleId
                    join rs in context.RoundScore on hr.RoundScoreId equals rs.RoundScoreId
                    join r in context.Rounds on rs.RoundId equals r.RoundId
                    where rs.PlayerId == playerId
                    group hr by new { rs.PlayerId, ch.Number, ch.Par, hr.CourseHoleId } into g
                    select new AverageHoleResultRo
                    {
                        CourseHoleId = g.Key.CourseHoleId,
                        HoleNumber = g.Key.Number,
                        Par = g.Key.Par,
                        PlayerId = g.Key.PlayerId,
                        AverageScore = g.Average(x => x.Score)
                    }).ToList();
                */
            }
        }
    }
}
