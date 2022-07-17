using Results.Domain.Model;
using Results.Domain.Model.ReadObjects;

namespace Results.Domain.Service
{
    public interface IRoundManager
    {
        IList<Round> GetBy(int eventId);
        IList<RoundScoreRo> GetScoresBy(int playerId = -1, int courseLayoutId = -1);
    }
}
