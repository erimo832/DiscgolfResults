namespace Results.Domain.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string DbName { get; set; } = "dg_result.db";
        public string DbFolder { get; set; } = "C:/db/";
        public string DbPath => $"{DbFolder}{DbName}";
    }
}
