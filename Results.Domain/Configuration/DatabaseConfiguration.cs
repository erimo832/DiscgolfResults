namespace Results.Domain.Configuration
{
    public class DatabaseConfiguration : IDatabaseConfiguration
    {
        public string DbPath { get; set; } = "C:/db/dg_result.db";
    }
}
