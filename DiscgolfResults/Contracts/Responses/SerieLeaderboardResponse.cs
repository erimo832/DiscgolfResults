namespace DiscgolfResults.Contracts.Responses
{
    public class SerieLeaderboardResponse
    {
        public int SerieId { get; set; }
        public string SerieName { get; set; } = "";
        public int RoundsToCount { get; set; }

        public IList<HcpResult> HcpResults { get; set; } = new List<HcpResult>();
        public IList<ScoreResult> ScoreResults { get; set; } = new List<ScoreResult>();
        public IList<CtpResult> CtpResults { get; set; } = new List<CtpResult>();
    }

    public class CtpResult
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; } = "";
        public int Placement { get; set; }
        public int NumberOfCtps { get; set; }
        public int NumberOfEvents { get; set; }
    }

    public class ScoreResult
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; } = "";
        public int Placement { get; set; }
        public double AvgScore { get; set; }
        public double TotalScore { get; set; }
        public double MinScore { get; set; }
        public double MaxScore { get; set; }
        public int NumberOfEvents { get; set; }

        public List<EventScore> EventScores { get; set; } = new List<EventScore>();

        public class EventScore
        {
            public double Score { get; set; }
        }
    }

    public class HcpResult
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; } = "";
        public int Placement { get; set; }
        public double TotalPoints { get; set; }
        public double AvgPoints { get; set; }
        public double AvgHcpScore { get; set; }
        public double TotalHcpScore { get; set; }
        public double MinPoints { get; set; }
        public double MaxPoints { get; set; }
        public int NumberOfEvents { get; set; }

        public List<EventPoint> EventPoints { get; set; } = new List<EventPoint>();

        public class EventPoint
        {
            public double Points { get; set; }
            public double HcpScore { get; set; }
        }
    }

}
