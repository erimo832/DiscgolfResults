using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface IHcpManager
    {
        double RoundHcp(int score, CourseLayout courseLayout);
        void UpdateHcp();

        IList<PlayerEvent> GetEventsIncludedInCalculations(IList<PlayerEvent> playerEvents);
        IList<PlayerEvent> GetEventsIncludedInHcpCalculations(IList<PlayerEvent> playerEvents);
    }
}
