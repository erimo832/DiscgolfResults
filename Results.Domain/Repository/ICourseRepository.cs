using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface ICourseRepository
    {
        void Insert(IList<Course> items);
        IList<Course> GetAll();
    }
}
