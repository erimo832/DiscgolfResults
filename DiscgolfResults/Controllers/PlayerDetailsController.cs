using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{

    [ApiController]
    public class PlayerDetailsController : ControllerBase
    {
        public PlayerDetailsController(IPlayerManager playerManager, ICourseManager courseManager, IPlayerDetailsTranslator translator)
        {
            PlayerManager = playerManager;
            CourseManager = courseManager;
            Translator = translator;
        }

        private IPlayerManager PlayerManager { get; }
        private ICourseManager CourseManager { get; }
        private IPlayerDetailsTranslator Translator { get; }

        [HttpGet]
        [Route("api/players/{playerId}/details")]
        public PlayerDetailsResponse? GetDetails(int playerId, int fromEventId = -1, int toEventId = -1)
        {
            var player = PlayerManager.GetBy(playerId: playerId, fromEventId: fromEventId, toEventId: toEventId, includePlayerEvents: true, includeRoundScores: false, includeCourseHcps: true).FirstOrDefault();
            var courses = CourseManager.GetBy(includeLayouts: true);
            
            return Translator.Translate(player, courses);
        }
    }
}
