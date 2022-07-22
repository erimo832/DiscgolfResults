using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {
        public EventsController(IEventManager eventManager, IEventResultTranslator translator, IPlayerManager playerManager)
        {   
            EventManager = eventManager;
            Translator = translator;
            PlayerManager = playerManager;
        }

        private IEventManager EventManager { get; }
        private IEventResultTranslator Translator { get; }
        private IPlayerManager PlayerManager { get; }

        [HttpGet]
        [Route("api/series/event-results")]
        public IEnumerable<EventResultResponse> GetAllResults()
        {
            var data = EventManager.GetBy(includePlayerEvents: true, includePlayerhcp: true);
            var players = PlayerManager.GetBy();

            return Translator.Translate(data, players);
        }

        [HttpGet]
        [Route("api/series/{seriesId}/event-results")]
        public IEnumerable<EventResultResponse> GetEventResultsBySeriesId(int seriesId)
        {
            var data = EventManager.GetBy(seriesId: seriesId, includePlayerEvents: true, includePlayerhcp: true);
            var players = PlayerManager.GetBy();

            return Translator.Translate(data, players);
        }

        [HttpGet]
        [Route("api/players/{playerId}/events")]
        public IEnumerable<EventResponse> GetEventsByPlayerId(int playerId)
        {
            var data = EventManager.GetBy(playerId: playerId, includeRounds: false, includePlayerEvents: true, includePlayerhcp: true);            

            return Translator.Translate(data);
        }
    }
}
