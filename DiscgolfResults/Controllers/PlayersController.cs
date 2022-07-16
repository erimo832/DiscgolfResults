using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{
    [ApiController]    
    public class PlayersController : ControllerBase
    {
        public PlayersController(IPlayerManager manager, ILogger<PlayersController> logger, IPlayerTranslator translator)
        {
            Manager = manager;
            Logger = logger;
            Translator = translator;
        }

        private IPlayerManager Manager { get; }
        private ILogger<PlayersController> Logger { get; }
        private IPlayerTranslator Translator { get; }

        [HttpGet]
        [Route("api/players")]
        public IEnumerable<PlayerResponse> GetAll()
        {
            var data = Manager.GetBy();

            return Translator.Translate(data);
        }

        [HttpGet]
        [Route("api/players/{playerId}")]
        public PlayerResponse? Get(int playerId)
        {
            var data = Manager.Get(playerId);

            return Translator.Translate(new[] { data }).FirstOrDefault();
        }
    }
}
