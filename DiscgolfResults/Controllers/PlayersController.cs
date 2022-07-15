using Microsoft.AspNetCore.Mvc;
using Results.Domain.Model;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{
    [ApiController]    
    public class PlayersController : ControllerBase
    {
        public PlayersController(IPlayerManager manager, ILogger<PlayersController> logger)
        {
            Manager = manager;
            Logger = logger;
        }

        private IPlayerManager Manager { get; }
        private ILogger<PlayersController> Logger { get; }

        [HttpGet]
        [Route("api/players")]
        public IEnumerable<Player> Get()
        {
            return Manager.GetAll();
        }

        [HttpGet]
        [Route("api/players/{playerId}")]
        public Player Get(int playerId)
        {
            return Manager.Get(playerId);
        }
    }
}
