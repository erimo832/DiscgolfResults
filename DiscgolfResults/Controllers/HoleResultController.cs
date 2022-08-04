using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{
    [ApiController]
    public class HoleResultController : ControllerBase
    {
        public HoleResultController(IHoleResultManager holeResultManager, IHoleResultTranslator translator)
        {
            HoleResultManager = holeResultManager;
            Translator = translator;
        }

        private IHoleResultManager HoleResultManager { get; }
        private IHoleResultTranslator Translator { get; }

        [HttpGet]
        [Route("api/players/{playerId}/average-hole-results")]
        public IEnumerable<AverageHoleResultResponse> GetAverageByPlayerId(int playerId, int fromEventId = -1, int toEventId = -1)
        {
            var data = HoleResultManager.GetRoBy(playerId: playerId, fromEventId: fromEventId, toEventId: toEventId);

            return Translator.Translate(data);
        }

       
        [HttpGet]
        [Route("api/courses/{courseId}/average-hole-results")]
        public IEnumerable<AverageHoleResultResponse> GetAverageByCourseId(int courseId = 1, int fromEventId = -1, int toEventId = -1)
        {
            var data = HoleResultManager.GetRoBy(fromEventId: fromEventId, toEventId: toEventId);

            return Translator.Translate(data);
        }
    }
}
