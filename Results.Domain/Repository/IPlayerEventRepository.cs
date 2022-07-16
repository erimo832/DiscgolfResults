using Results.Domain.Model;

namespace Results.Domain.Repository
{
    internal interface IPlayerEventRepository
    {
        void Insert(IList<PlayerEvent> items);
        IList<PlayerEvent> GetPlayerEvents(int playerId);
    }
}
