using Results.Domain.Model;

namespace Results.Domain.Repository
{
    public interface IPlayerRepository
    {
        IList<Player> GetAll();

        void Insert(IList<Player> items);
        void Update(Player player);
    }
}
