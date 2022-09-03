namespace DiscgolfResults.Contracts.Responses
{
    public class CourseDetailsResponse : CourseResponse
    {
        public int NumberOfEvents { get; set; }
        public double AverageNumerOfPlayers { get; set; }

        public IList<EventResponse> Events { get; set; } = new List<EventResponse>();
        public IList<ScoreDistribution> ScoreDistibution { get; set; } = new List<ScoreDistribution>();
    }
}
