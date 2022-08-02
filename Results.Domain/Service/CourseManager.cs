using Results.Domain.Model;
using Results.Domain.Repository;

namespace Results.Domain.Service
{
    internal class CourseManager : ICourseManager
    {
        private ICourseRepository CourseRepository { get; }
        private ICourseLayoutRepository CourseLayoutRepository { get; }

        public CourseManager(ICourseRepository courseRepository, ICourseLayoutRepository courseLayoutRepository)
        {
            CourseRepository = courseRepository;
            CourseLayoutRepository = courseLayoutRepository;
        }

        public CourseLayout GetLayout(int courseLayoutId)
        {
            return CourseLayoutRepository.Get(courseLayoutId);
        }

        public IList<CourseLayout> GetAllLayouts()
        {
            return CourseLayoutRepository.GetAllLayouts();
        }

        public void Insert(IList<Course> items)
        {
            CourseRepository.Insert(items);
        }

        public IList<Course> GetBy(int courseId = -1, bool includeLayouts = false)
        {
            return CourseRepository.GetBy(courseId, includeLayouts);
        }
    }
}
