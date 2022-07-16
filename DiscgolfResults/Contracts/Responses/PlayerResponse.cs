namespace DiscgolfResults.Contracts.Responses
{
    public class PlayerResponse
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PdgaNumber { get; set; } = "";

        public string FullName => $"{FirstName} {LastName}";
    }
}
