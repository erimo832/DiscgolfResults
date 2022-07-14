namespace Results.Domain.Configuration
{
    public class DbSyncConfiguration : IDbSyncConfiguration
    {
        public string CourseSettingsPath { get; set; } = "M:/www/discgolf/courses.json";
        public string SeriesSettingsPath { get; set; } = "M:/www/discgolf/series.json";
        public string DuplicatePlayersSettingsPath { get; set; } = "M:/www/discgolf/duplicatePlayers.json";
    }
}
