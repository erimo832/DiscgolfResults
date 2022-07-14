namespace Results.Domain.Model
{
    public class PlayerCourseLayoutHcp
    {
        public int PlayerCourseLayoutHcpId { get; set; }

        public int PlayerId { get; set; }
        public int CourseLayoutId { get; set; }
        public int EventId { get; set; }

        public double HcpBefore { get; set; }
        public double HcpAfter { get; set; }

        public Player Player { get; set; } = null;
        public CourseLayout CourseLayout { get; set; } = null;
        public Event Event { get; set; } = null;
    }
}
