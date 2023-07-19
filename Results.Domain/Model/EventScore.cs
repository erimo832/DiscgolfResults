namespace Results.Domain.Model
{
    public class EventScore
    {
        public int EventScoreId { get; set; }
        public int PlayerId { get; set; }
        public int EventId { get; set; }
        public string Division { get; set; } = "";

        public double TotalScore { get; set; }
        public double TotalHcpScore { get; set; }

        public int Placement { get; set; }
        public int PlacementHcp { get; set; }
        public double HcpPoints { get; set; }
        public int NumberOfCtp { get; set; }

        public Player Player { get; set; } = null;
        public Event Event { get; set; } = null;
    }
}
