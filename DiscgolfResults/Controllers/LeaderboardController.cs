using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        public LeaderboardController(ISerieManager serieManager, ISerieLeaderboardTranslator translator, IPlayerManager playerManager)
        {
            SerieManager = serieManager;
            Translator = translator;
            PlayerManager = playerManager;
        }

        private ISerieManager SerieManager { get; }
        private ISerieLeaderboardTranslator Translator { get; }
        private IPlayerManager PlayerManager { get; }

        [HttpGet]
        [Route("api/series/leaderboards")]
        public IList<SerieLeaderboardResponse> GetAll()
        {
            var data = SerieManager.GetBy(includeEvents: true, includePlayerEvents: true, includePlayerhcp: false);
            var players = PlayerManager.GetBy();

            return Translator.Translate(data, players).OrderByDescending(x => x.SerieId).ToList();
        }

        [HttpGet]
        [Route("api/series/{seriesId}/leaderboards")]
        public SerieLeaderboardResponse? GetBySeriesId(int seriesId)
        {
            var data = SerieManager.GetBy(seriesId: seriesId, includeEvents: true, includePlayerEvents: true, includePlayerhcp: false);
            var players = PlayerManager.GetBy();

            return Translator.Translate(data, players).FirstOrDefault();
        }
    }
}
