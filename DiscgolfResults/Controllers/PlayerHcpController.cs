using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{

    [ApiController]
    public class PlayerHcpController : ControllerBase
    {
        public PlayerHcpController(IPlayerManager playerManager, IPlayerHcpTranslator translator)
        {
            Translator = translator;
            PlayerManager = playerManager;
        }

        public IPlayerHcpTranslator Translator { get; }
        public IPlayerManager PlayerManager { get; }

        [HttpGet]
        [Route("api/players/handicaps")]
        public IEnumerable<PlayerHcpResponse> GetAll()
        {
            var players = PlayerManager.GetBy(includeCourseHcps: true);

            return Translator.Translate(/*data,*/ players).OrderBy(x => x.CurrentHcp);
        }
    }
}
