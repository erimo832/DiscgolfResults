using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface ICourseRepository
    {
        void Insert(IList<Course> items);
        IList<Course> GetAll();
        IList<Course> GetBy(int courseId = -1, bool includeLayouts = false);
    }
}
