using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface ICourseManager
    {
        CourseLayout GetLayout(int courseLayoutId);
        IList<CourseLayout> GetAllLayouts();

        void Insert(IList<Course> items);
    }
}
