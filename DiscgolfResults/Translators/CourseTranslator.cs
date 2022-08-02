using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public class CourseTranslator : ICourseTranslator
    {
        public IList<CourseResponse> Translate(IList<Course> courses)
        {
            return courses.Select(x => new CourseResponse
            {
                CourseId = x.CourseId,
                Name = x.Name
            }).ToList();
        }
    }
}
