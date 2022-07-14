namespace Results.Domain.Configuration.External
{
    internal class DuplicatePlayerConfiguration : IDuplicatePlayerConfiguration
    {
        public IList<DuplicatePlayer> DuplicatePlayers { get; set; } = new List<DuplicatePlayer>();
    }

    public class DuplicatePlayer
    {
        public IList<PlayerAlias> PlayerAliases { get; set; } = new List<PlayerAlias>();
    }

    public class PlayerAlias
    {
        public bool IsPrimary { get; set; } = false;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int? PdgaNumber { get; set; }

        public string PdgaNumberAsString => PdgaNumber == null ? "" : PdgaNumber.Value.ToString();
    }
}
