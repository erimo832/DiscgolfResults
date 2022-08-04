using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;
using Results.Domain.Model.ReadObjects;

namespace DiscgolfResults.Translators
{
    public interface IHoleResultTranslator
    {
        IList<AverageHoleResultResponse> Translate(IList<HoleResultRo> results);
    }
}
