using DiscgolfResults.Contracts.Responses;
using Results.Domain.Model;

namespace DiscgolfResults.Translators
{
    public interface ICourseTranslator
    {
        IList<CourseResponse> Translate(IList<Course> courses);
    }
}
