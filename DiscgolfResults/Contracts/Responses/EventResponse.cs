namespace DiscgolfResults.Contracts.Responses
{
    public class EventResponse
    {
        public int EventId { get; set; }
        public int SerieId { get; set; }
        public string EventName { get; set; } = "";
        public DateTime StartTime { get; set; }
    }
}
