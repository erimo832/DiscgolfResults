﻿using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model.ReadObjects;

namespace DiscgolfResults.Translators
{
    public class HoleResultTranslator : IHoleResultTranslator
    {
        public IList<AverageHoleResultResponse> Translate(IList<HoleResultRo> results)
        {
            var data =  results
                .GroupBy(x => new { x.CourseLayoutId, x.LayoutName })
                .Select(x => new AverageHoleResultResponse
                { 
                    CourseLayoutId = x.Key.CourseLayoutId,
                    LayoutName = x.Key.LayoutName,
                    ScoreDistributions = GetScoreDistributions(x.ToList()),
                    HoleResults = new HoleResultResponse[0]
                }).ToList();

            foreach (var row in data) 
            {
                row.HoleResults =
                    results
                        .Where(x => x.CourseLayoutId == row.CourseLayoutId)
                        .GroupBy(x => new { x.CourseHoleId, x.HoleNumber, x.Par })
                        .Select(x => new HoleResultResponse
                        {
                            CourseHoleId = x.Key.CourseHoleId,
                            HoleNumber = x.Key.HoleNumber,
                            Par = x.Key.Par,
                            Hios = x.Where(y => y.Score == 1).Count(),
                            AverageScore = Math.Round(x.Average(y => y.Score), 2),
                            ScoreDistibutions = new List<HoleScoreDistribution>
                            {
                                new HoleScoreDistribution { RelativeScore = RelativeScore.Eagle, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.Eagle), TotalPlayed = x.Count() },
                                new HoleScoreDistribution { RelativeScore = RelativeScore.Birdie, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.Birdie), TotalPlayed = x.Count() },
                                new HoleScoreDistribution { RelativeScore = RelativeScore.Par, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.Par), TotalPlayed = x.Count() },
                                new HoleScoreDistribution { RelativeScore = RelativeScore.Bogey, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.Bogey), TotalPlayed = x.Count() },
                                new HoleScoreDistribution { RelativeScore = RelativeScore.DoubleBogey, NumberOfScores = x.Count(x => (x.Score - x.Par) == (int)RelativeScore.DoubleBogey), TotalPlayed = x.Count() },
                                new HoleScoreDistribution { RelativeScore = RelativeScore.PlusBogey, NumberOfScores = x.Count(x => (x.Score - x.Par) >= (int)RelativeScore.PlusBogey), TotalPlayed = x.Count() },
                            }
                        }).ToArray();

            }

            return data;
        }

        private ScoreDistribution[] GetScoreDistributions(IList<HoleResultRo> data)
        {
            var scores = data
                .GroupBy(x => new { x.EventId, x.PlayerId })
                .Select(x => new { Score = x.Sum(y => y.Score) });


            var min = scores.Min(x => x.Score);
            var max = scores.Max(x => x.Score);

            var result = new List<ScoreDistribution>();

            for (int i = min; i < max + 1; i++)
            {
                result.Add(new ScoreDistribution { NumberOfTimes = scores.Count(x => x.Score == i), Score = i });
            }

            return result.ToArray();
        }
    }
}
