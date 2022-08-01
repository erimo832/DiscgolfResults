using Results.Domain.Configuration.External;
using Results.Domain.Model;

namespace Results.Domain.Service
{
    public interface IPlayerManager
    {
        Player GetPlayer(string pdgaNr, string firstName, string lastName, IDuplicatePlayerConfiguration duplicatePlayers);
        IList<Player> GetBy(int playerId = -1, int fromEventId = -1, int toEventId = -1, bool includePlayerEvents = false, bool includeRoundScores = false, bool includeCourseHcps = false);
        Player Get(int playerId);

        void InsertPlayerHcps(IList<PlayerCourseLayoutHcp> layoutHcps);
    }
}
