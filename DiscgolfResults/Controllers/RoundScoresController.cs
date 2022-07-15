using Microsoft.AspNetCore.Mvc;
using Results.Domain.Model.ReadObjects;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{
    [ApiController]
    public class RoundScoresController : ControllerBase
    {
        public RoundScoresController(IRoundManager roundManager)
        {
            RoundManager = roundManager;
        }

        private IRoundManager RoundManager { get; }

        [HttpGet]
        [Route("api/players/{playerId}/round-scores")]
        public IEnumerable<RoundScoreRo> GetByPlayerId(int playerId)
        {
            int courseLayoutId = 1;
            var result = RoundManager.GetScoresBy(playerId, courseLayoutId);

            return result;
        }
    }
}
