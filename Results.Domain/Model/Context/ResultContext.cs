using Microsoft.EntityFrameworkCore;
using Results.Domain.Configuration;
using Results.Domain.Model.ReadObjects;

namespace Results.Domain.Model.Context
{
    public class ResultContext : DbContext
    {
        private IDatabaseConfiguration Config { get; }

        public ResultContext(IDatabaseConfiguration configuration)
        {
            Config = configuration;

            Database.EnsureCreated();
        }

        public DbSet<Serie> Series { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<RoundScore> RoundScore { get; set; }
        public DbSet<HoleResult> HoleResult { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseLayout> CourseLayouts { get; set; }
        public DbSet<CourseHole> CourseHoles { get; set; }
        public DbSet<PlayerEvent> PlayerEvents { get; set; }
        public DbSet<PlayerCourseLayoutHcp> PlayerCourseLayoutHcps { get; set; }

        //Read queries
        public DbSet<HoleResultRo> HoleResultRoView { get; set; }
        public DbSet<AverageHoleResultRo> AverageHoleResultRoView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={Config.DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HoleResultRo>().HasNoKey();
            modelBuilder.Entity<AverageHoleResultRo>().HasNoKey();
        }
    }
}
