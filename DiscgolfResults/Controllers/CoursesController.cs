using DiscgolfResults.Contracts.Responses;
using DiscgolfResults.Translators;
using Microsoft.AspNetCore.Mvc;
using Results.Domain.Service;

namespace DiscgolfResults.Controllers
{
    [ApiController]
    public class CoursesController : ControllerBase
    {
        public CoursesController(ICourseManager courseManager, ICourseTranslator courseTranslator)
        {
            CourseManager = courseManager;
            CourseTranslator = courseTranslator;
        }

        private ICourseManager CourseManager { get; }
        private ICourseTranslator CourseTranslator { get; }

        [HttpGet]
        [Route("api/courses")]
        public IEnumerable<CourseResponse> GetAll()
        {
            var data = CourseManager.GetBy(includeLayouts: true);

            return CourseTranslator.Translate(data);
        }

        [HttpGet]
        [Route("api/courses/{courseId}")]
        public IEnumerable<CourseResponse> GetByCourseId(int courseId)
        {
            var data = CourseManager.GetBy(courseId: courseId, includeLayouts: true);

            return CourseTranslator.Translate(data);
        }
    }
}
