namespace Results.Domain.Model
{
    public class Serie
    {
        public int SerieId { get; set; }
        public string Name { get; set; } = "";
        public int RoundsToCount { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
