namespace DiscgolfResults.Contracts.Responses
{
    public class EventResultResponse
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = "";
        public DateTime StartTime { get; set; }
        public IList<Result> Results { get; set; } = new List<Result>();
    }

    public class Result
    {
        public int Placement { get; set; }
        public int PlacementHcp { get; set; }
        public int PlayerId { get; set; }
        public string FullName { get; set; } = "";
        public double Points { get; set; }
        public double Score { get; set; }
        public double HcpScore { get; set; }
        public double HcpBefore { get; set; }
        public double HcpAfter { get; set; }
        public int NumberOfCtps { get; set; } = 0;
    }
}
