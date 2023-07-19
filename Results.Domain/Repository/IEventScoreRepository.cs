using Results.Domain.Model;

namespace Results.Domain.Repository
{
    internal interface IEventScoreRepository
    {
        void Insert(IList<EventScore> items);
        IList<EventScore> GetPlayerEvents(int playerId);
    }
}
