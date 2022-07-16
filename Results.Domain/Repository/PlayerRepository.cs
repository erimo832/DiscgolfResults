using Microsoft.EntityFrameworkCore;
using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.Context;

namespace Results.Domain.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private IDatabaseConfiguration Config { get; }

        public PlayerRepository(IDatabaseConfiguration configuration)
        {
            Config = configuration;
        }

        public Player Get(int playerId)
        {
            using (var context = new ResultContext(Config))
            {
                return context.Players.FirstOrDefault(x => x.PlayerId == playerId);
            }
        }

        public void Insert(IList<Player> players)
        {
            using (var context = new ResultContext(Config))
            {
                foreach (var player in players)
                {
                    context.Add(player);
                }

                context.SaveChanges();
            }
        }

        public void Update(Player player)
        {
            using (var context = new ResultContext(Config))
            {
                var result = context.Players.SingleOrDefault(x => x.PlayerId == player.PlayerId);

                if (result != null)
                {
                    result.FirstName = player.FirstName;
                    result.LastName = player.LastName;
                    result.PdgaNumber = player.PdgaNumber;
                    context.SaveChanges();
                }
            }
        }

        public IList<Player> GetBy(int playerId = -1, bool includePlayerEvents = false, bool includeRoundScores = false, bool includeCourseHcps = false)
        {
            using (var context = new ResultContext(Config))
            {
                var players = context.Players
                    //.If(includePlayerEvents, q => q.Include(x => x.PlayerEvents).ThenInclude(y => y.Event))
                    //.If(includeRoundScores, q => q.Include(x => x.RoundScores).ThenInclude(y => y.Round))
                    //.If(includeCourseHcps, q => q.Include(x => x.PlayerCourseLayoutHcp))
                    .If(playerId != -1, q => q.Where(x => x.PlayerId == playerId))
                    .ToList();

                //Do it manually
                foreach (var player in players)
                {
                    player.PlayerEvents = includePlayerEvents ? context.PlayerEvents.Where(x => x.PlayerId == player.PlayerId).Include(q => q.Event).ToList() : new List<PlayerEvent>();
                    player.PlayerCourseLayoutHcp = includeCourseHcps ? context.PlayerCourseLayoutHcps.Where(x => x.PlayerId == player.PlayerId).ToList() : new List<PlayerCourseLayoutHcp>();
                    player.RoundScores = includeRoundScores ? context.RoundScore.Where(x => x.PlayerId == player.PlayerId).ToList() : new List<RoundScore>();
                }

                return players;
            }
        }
    }
}
