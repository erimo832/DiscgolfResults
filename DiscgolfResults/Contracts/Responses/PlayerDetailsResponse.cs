namespace DiscgolfResults.Contracts.Responses
{
    public class PlayerDetailsResponse : PlayerResponse
    {
        public int TotalRounds { get; set; } 
        public int TotalCtps { get; set; }
        public double CtpPercentage { get; set; }
        public DateTime FirstAppearance { get; set; }
        public DateTime LastAppearance { get; set; }

        public double BestScore { get; set; }
        public double WorstScore { get; set; }
        public double AvgScore { get; set; }

        public IList<PlayerResult> EventResults { get; set; } = new List<PlayerResult>();
    }

    public class PlayerResult
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = "";
        public DateTime StartTime { get; set; }
        public int Placement { get; set; }
        public int PlacementHcp { get; set; }        
        public double Points { get; set; }
        public double Score { get; set; }
        public double HcpScore { get; set; }
        public double HcpBefore { get; set; }
        public double HcpAfter { get; set; }
        public int NumberOfCtps { get; set; } = 0;
        public bool InHcpCalc { get; set; } = false;
        public bool InHcpAvgCalc { get; set; } = false;
    }

}
