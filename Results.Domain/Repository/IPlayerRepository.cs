using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface IPlayerRepository
    {
        IList<Player> GetBy(int playerId = -1, bool includePlayerEvents = false, bool includeRoundScores = false, bool includeCourseHcps = false);
        Player Get(int playerId);

        void Insert(IList<Player> items);
        void Update(Player player);
    }
}
