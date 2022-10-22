namespace DiscgolfResults.Contracts.Responses
{
    public class CourseDetailsResponse : CourseResponse
    {
        public int NumberOfEvents { get; set; }
        public int NumberOfPlayedRounds { get; set; }
        public double AverageNumberOfPlayers { get; set; }
        public int UniqueNumberOfPlayers { get; set; }

        public IList<EventResponse> Events { get; set; } = new List<EventResponse>();
        public IList<ScoreDistribution> ScoreDistibution { get; set; } = new List<ScoreDistribution>();
    }
}
