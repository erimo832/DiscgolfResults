using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model.ReadObjects;

namespace DiscgolfResults.Translators
{
    public class HoleResultTranslator : IHoleResultTranslator
    {
        public IList<AverageHoleResultResponse> Translate(IList<HoleResultRo> results)
        {
            return results
                .GroupBy(x => new { x.CourseHoleId, x.HoleNumber, x.Par })
                .Select(x => new AverageHoleResultResponse
                {
                    CourseHoleId = x.Key.CourseHoleId,
                    HoleNumber = x.Key.HoleNumber,
                    Par = x.Key.Par,
                    Hios = x.Where(y => y.Score == 1).Count(),
                    AverageScore = Math.Round(x.Average(y => y.Score), 2),
                    ScoreDistibutions = new List<ScoreDistibution>
                    {
                        new ScoreDistibution { RelativeScore = RelativeScore.Eagle, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.Eagle), TotalPlayed = x.Count() },
                        new ScoreDistibution { RelativeScore = RelativeScore.Birdie, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.Birdie), TotalPlayed = x.Count() },
                        new ScoreDistibution { RelativeScore = RelativeScore.Par, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.Par), TotalPlayed = x.Count() },
                        new ScoreDistibution { RelativeScore = RelativeScore.Bogey, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.Bogey), TotalPlayed = x.Count() },
                        new ScoreDistibution { RelativeScore = RelativeScore.DoubleBogey, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.DoubleBogey), TotalPlayed = x.Count() },
                        new ScoreDistibution { RelativeScore = RelativeScore.PlusBogey, NumberOfScores = x.Count(x => (x.Score - x.Par) >= (int)RelativeScore.PlusBogey), TotalPlayed = x.Count() },
                    }
                }).ToList();
        }
    }
}
