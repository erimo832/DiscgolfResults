using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface IPlayerRepository
    {
        IList<Player> GetAll();
        Player Get(int playerId);

        void Insert(IList<Player> items);
        void Update(Player player);
    }
}
