using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Model.ReadObjects;
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
        [Route("api/players/{playerId}/hole-results")]
        public IEnumerable<HoleResultRo> GetByPlayerId(int playerId)
        {
            var data = HoleResultManager.GetRoBy(playerId: playerId);
            //var players = PlayerManager.GetBy();

            //return Translator.Translate(data, players);

            return data;
        }


        [HttpGet]
        [Route("api/players/{playerId}/average-hole-results")]
        public IEnumerable<AverageHoleResultResponse> GetAverageByPlayerId(int playerId, int fromEventId = -1, int toEventId = -1)
        {
            var data = HoleResultManager.GetAverageRoBy(playerId: playerId, fromEventId: fromEventId, toEventId: toEventId);

            return Translator.Translate(data);
        }
    }
}
