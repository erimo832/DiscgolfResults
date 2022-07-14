using Microsoft.AspNetCore.Mvc;
using Results.Domain.Model;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public IEnumerable<Player> Get()
        {
            return Manager.GetAll();
        }
    }
}
