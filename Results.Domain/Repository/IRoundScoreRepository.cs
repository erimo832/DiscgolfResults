using Results.Domain.Model;
using Results.Domain.Model.ReadObjects;

namespace Results.Domain.Repository
{
    public interface IRoundScoreRepository
    {
        void Insert(IList<RoundScore> items);
        IList<RoundScoreRo> GetScoresBy(int playerId, int courseLayoutId);
    }
}
