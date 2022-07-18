namespace Results.Domain.Configuration
{
    public interface IDatabaseConfiguration
    {
        string DbName { get; set; }
        string DbFolder { get; set; }

        string DbPath { get; }
    }
}
