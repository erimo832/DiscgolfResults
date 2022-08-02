namespace DiscgolfResults.Contracts.Responses
{
    public class CourseDetailsResponse : CourseResponse
    {
        public int NumberOfEvents { get; set; }
        public double AverageNumerOfPlayers { get; set; }

        public IList<EventResponse> Events { get; set; }
    }
}
