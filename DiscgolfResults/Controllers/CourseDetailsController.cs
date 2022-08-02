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
        public CourseDetailsResponse GetDetailsByCourseId(int courseId)
        {
            var data = CourseManager.GetBy(courseId: courseId, includeLayouts: true).FirstOrDefault();
            var events = EventManager.GetBy(includePlayerEvents: true);

            return Translator.Translate(data, events);
        }
    }
}
