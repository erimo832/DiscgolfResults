using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface IHcpManager
    {
        double RoundHcp(int score, CourseLayout courseLayout);
        void UpdateHcp(bool hcpPerLayout);

        IList<EventScore> GetEventsIncludedInCalculations(IList<EventScore> playerEvents);
        IList<EventScore> GetEventsIncludedInHcpCalculations(IList<EventScore> playerEvents);
    }
}
