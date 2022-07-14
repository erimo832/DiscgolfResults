using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private IDatabaseConfiguration Config { get; }

        public CourseRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<Course> items)
        {
            using (var context = new ResultContext(Config))
            {
                foreach (var item in items)
                {
                    context.Add(item);
                }

                context.SaveChanges();
            }
        }

        public IList<Course> GetAll()
        {
            using (var context = new ResultContext(Config))
            {
                return context.Courses.ToList();
            }
        }
    }
}
