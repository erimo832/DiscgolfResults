using Microsoft.EntityFrameworkCore;
using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    public class CourseLayoutRepository : ICourseLayoutRepository
    {
        private IDatabaseConfiguration Config { get; }

        public CourseLayoutRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<CourseLayout> items)
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

        public CourseLayout Get(int courseLayoutId)
        {
            using (var context = new ResultContext(Config))
            {
                return context.CourseLayouts
                    .Include(x => x.Holes)
                    .FirstOrDefault(x => x.CourseLayoutId == courseLayoutId);
            }
        }

        public IList<CourseLayout> GetAllLayouts()
        {
            using (var context = new ResultContext(Config))
            {
                return context.CourseLayouts.ToList();
            }
        }
    }
}
