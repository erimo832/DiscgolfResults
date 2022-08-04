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
                    AverageScore = Math.Round(x.Average(y => y.Score), 2)
                }).ToList();
        }
    }
}
