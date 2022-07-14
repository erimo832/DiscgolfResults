using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface ICourseLayoutRepository
    {
        void Insert(IList<CourseLayout> items);
        CourseLayout Get(int courseLayoutId);
        IList<CourseLayout> GetAllLayouts();
    }
}
