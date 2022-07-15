using Results.Domain.Configuration.External;
using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface IPlayerManager
    {
        Player GetPlayer(string pdgaNr, string firstName, string lastName, IDuplicatePlayerConfiguration duplicatePlayers);
        IList<Player> GetAll();
        Player Get(int playerId);

        void InsertPlayerHcps(IList<PlayerCourseLayoutHcp> layoutHcps);
    }
}
