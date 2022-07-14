using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    internal class PlayerCourseLayoutHcpRepository : IPlayerCourseLayoutHcpRepository
    {
        private IDatabaseConfiguration Config { get; }

        public PlayerCourseLayoutHcpRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public void Insert(IList<PlayerCourseLayoutHcp> items)
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
    }
}
