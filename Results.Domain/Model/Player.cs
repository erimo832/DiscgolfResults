namespace Results.Domain.Model
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int? PdgaNumber { get; set; }

        public string PdgaNumberAsString => PdgaNumber == null ? "" : PdgaNumber.Value.ToString();
    }
}
