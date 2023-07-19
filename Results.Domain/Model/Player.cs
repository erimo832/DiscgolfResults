namespace Results.Domain.Model
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int? PdgaNumber { get; set; }

        public string PdgaNumberAsString => PdgaNumber == null ? "" : PdgaNumber.Value.ToString();
        public string FullName => $"{FirstName} {LastName}";

        public List<EventScore> PlayerEvents { get; set; } = new List<EventScore>();
        public List<PlayerCourseLayoutHcp> PlayerCourseLayoutHcp { get; set; } = new List<PlayerCourseLayoutHcp>();
        public List<RoundScore> RoundScores { get; set; } = new List<RoundScore>();
    }
}
