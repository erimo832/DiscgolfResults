using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface ICourseManager
    {
        CourseLayout GetLayout(int courseLayoutId);
        IList<CourseLayout> GetAllLayouts();

        IList<Course> GetBy(int courseId = -1, bool includeLayouts = false);

        void Insert(IList<Course> items);
    }
}
