using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{

    [ApiController]
    public class CourseDetailsController : ControllerBase
    {
        public CourseDetailsController(ICourseManager courseManager, ICourseDetailsTranslator translator, IEventManager eventManager)
        {
            CourseManager = courseManager;
            Translator = translator;
            EventManager = eventManager;
        }

        private ICourseManager CourseManager { get; }
        private ICourseDetailsTranslator Translator { get; }
        private IEventManager EventManager { get; }

        [HttpGet]
        [Route("api/courses/{courseId}/details")]
        public CourseDetailsResponse GetDetailsByCourseId(int courseId, int fromEventId = -1, int toEventId = -1)
        {
            var data = CourseManager.GetBy(courseId: courseId, includeLayouts: true).FirstOrDefault();
            var events = EventManager.GetBy(fromEventId: fromEventId, toEventId: toEventId, includePlayerEvents: true);

            return Translator.Translate(data, events);
        }
    }
}
