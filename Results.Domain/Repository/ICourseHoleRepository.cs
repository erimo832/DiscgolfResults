using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface ICourseHoleRepository
    {
        void Insert(IList<CourseHole> items);
    }
}
