using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public interface ICourseDetailsTranslator
    {
        CourseDetailsResponse Translate(Course course, IList<Event> events);
    }
}
