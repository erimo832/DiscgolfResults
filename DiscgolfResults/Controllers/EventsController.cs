using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Transformers;
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
        [Route("api/series/{seriesId}/events")]
        public IEnumerable<EventResultResponse> GetEventBySeriesId(int seriesId)
        {
            var data = EventManager.GetBy(seriesId: seriesId, includePlayerEvents: true, includePlayerhcp: true);
            var players = PlayerManager.GetAll();

            return Translator.Translate(data, players);
        }
    }
}
