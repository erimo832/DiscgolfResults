using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model.ReadObjects;

namespace DiscgolfResults.Translators
{
    public class HoleResultTranslator : IHoleResultTranslator
    {
        public IList<AverageHoleResultResponse> Translate(IList<AverageHoleResultRo> results)
        {
            return results.Select(x => new AverageHoleResultResponse
            {
                AverageScore = Math.Round(x.AverageScore, 2),
                CourseHoleId = x.CourseHoleId,
                HoleNumber = x.HoleNumber, 
                Par = x.Par
            }).ToList();
        }
    }
}
