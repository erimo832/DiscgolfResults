namespace Results.Domain.Configuration
{
    public interface IDbSyncConfiguration
    {
        string CourseSettingsPath { get; set; }
        string SeriesSettingsPath { get; set; }
        string DuplicatePlayersSettingsPath { get; set; }
    }
}
