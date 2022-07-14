using Results.Domain.Model;

namespace Results.Domain.Proxies
{
    public interface ICoursesProxy
    {
        IList<Course> GetCourses();
    }
}
