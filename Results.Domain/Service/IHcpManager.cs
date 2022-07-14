using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface IHcpManager
    {
        double RoundHcp(int score, CourseLayout courseLayout);
        void UpdateHcp();
    }
}
