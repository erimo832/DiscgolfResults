using Results.Domain.Model;

namespace Results.Domain.Repository
{
    internal interface IPlayerCourseLayoutHcpRepository
    {
        void Insert(IList<PlayerCourseLayoutHcp> layoutHcps);
    }
}
