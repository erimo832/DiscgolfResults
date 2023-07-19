using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Model.ReadObjects;

namespace Results.Domain.Service
{
    internal class HcpManager : IHcpManager
    {
        private IHcpConfiguration Configuration { get; }
        private IPlayerManager PlayerManager { get; }
        private IRoundManager RoundManager { get; }
        private ICourseManager CourseManager { get; }

        public HcpManager(IHcpConfiguration configuration, IPlayerManager playerManager, IRoundManager roundManager, ICourseManager courseManager)
        {
            Configuration = configuration;
            PlayerManager = playerManager;
            RoundManager = roundManager;
            CourseManager = courseManager;
        }

        public double RoundHcp(int score, CourseLayout courseLayout)
        {
            return (score - courseLayout.HcpAdjustedPar) * courseLayout.HcpSlopeFactor;
        }


        public void UpdateHcp(bool hcpPerLayout)
        {
            //Just a dummy one
            IList<CourseLayout> layouts = new[] { new CourseLayout { CourseLayoutId = -1 } };
                
            if(hcpPerLayout)
                layouts = CourseManager.GetAllLayouts();            

            var players = PlayerManager.GetBy();

            var hcps = new List<PlayerCourseLayoutHcp>();

            foreach (var layout in layouts)
            {
                foreach (var player in players)
                {
                    var scores = RoundManager.GetScoresBy(player.PlayerId, layout.CourseLayoutId).OrderBy(x => x.StartTime);
                    var currentPlayerScores = new List<RoundScoreRo>();

                    foreach (var score in scores)
                    {
                        var beforeHcp = GetHcp(currentPlayerScores);
                        currentPlayerScores.Add(score);
                        var afterHcp = GetHcp(currentPlayerScores);

                        var hcp = GetLayoutHcp(beforeHcp, afterHcp, score);
                        hcps.Add(hcp);
                    }
                }
            }

            PlayerManager.InsertPlayerHcps(hcps);
        }


        private PlayerCourseLayoutHcp GetLayoutHcp(double hcpBefore, double hcpAfter, RoundScoreRo currentRound)
        {
            return new PlayerCourseLayoutHcp
            {
                CourseLayoutId = currentRound.CourseLayoutId,
                EventId = currentRound.EventId,
                HcpBefore = hcpBefore,
                HcpAfter = hcpAfter,
                PlayerId = currentRound.PlayerId
            };
        }

        public double GetHcp(List<RoundScoreRo> rounds)
        {
            var lastxRounds = rounds.TakeLast(Configuration.RoundsForHcp);
            var roundsForHcpCnt = TakeCountForAvg(lastxRounds.Count(), Configuration.RoundsForHcp);
            return Math.Round(lastxRounds.OrderBy(x => x.RoundHcp).Take(roundsForHcpCnt).Sum(x => x.RoundHcp) / Convert.ToDouble(roundsForHcpCnt), Configuration.HcpDecimals);
        }

        public int TakeCountForAvg(int numOfRoundsPlayed, int roundsForHcp)
        {
            numOfRoundsPlayed = numOfRoundsPlayed > roundsForHcp ? roundsForHcp : numOfRoundsPlayed;

            var cnt = Math.Ceiling(roundsForHcp / 3.0);
            if (numOfRoundsPlayed < roundsForHcp)
            {
                cnt = Math.Ceiling(numOfRoundsPlayed / 3.0);
            }
            if (cnt == 0)
                cnt = 1;

            return Convert.ToInt32(cnt);
        }

        public IList<EventScore> GetEventsIncludedInCalculations(IList<EventScore> playerEvents)
        {
            return playerEvents.OrderBy(x => x.EventScoreId).TakeLast(Configuration.RoundsForHcp).ToList();
        }

        public IList<EventScore> GetEventsIncludedInHcpCalculations(IList<EventScore> playerEvents)
        {
            var lastEvents = GetEventsIncludedInCalculations(playerEvents);
            var roundsForHcpCnt = TakeCountForAvg(playerEvents.Count(), Configuration.RoundsForHcp);

            return lastEvents.OrderBy(x => x.TotalScore).Take(roundsForHcpCnt).ToList();
        }
    }
}
