using Results.Domain.Common.Extensions;
using Results.Domain.Configuration;
using Results.Domain.Model;
using Results.Domain.Proxies.Contracts;

namespace Results.Domain.Proxies
{
    internal class CoursesProxy : ICoursesProxy
    {
        private IDbSyncConfiguration Config { get; }

        public CoursesProxy(IDbSyncConfiguration syncConfiguration)
        {
            Config = syncConfiguration;
        }

        public IList<Course> GetCourses()
        {
            var result = new List<Course>();

            var courses = File.ReadAllText(Config.CourseSettingsPath).FromJson<CourseListExternal>();

            foreach (var course in courses.Courses)
            {
                var c = new Course
                {
                    CourseId = course.CourseId,
                    Name = course.Name
                };

                foreach (var layout in course.Layouts)
                {
                    var l = new CourseLayout
                    {
                        HcpAdjustedPar = layout.LayoutAdjustedPar,
                        HcpSlopeFactor = layout.LayoutSlopeFactor,
                        CourseLayoutId = layout.CourseLayoutId,
                        Name = layout.Name,
                        NumberOfHoles = layout.NumberOfHoles,                        
                        //Par is updated later
                    };

                    foreach (var hole in layout.Holes)
                    {
                        l.Holes.Add(new CourseHole
                        {
                            Number = hole.Number,
                            Par = hole.Par
                        });
                    }

                    l.Par = l.Holes.Sum(x => x.Par);

                    c.Layouts.Add(l);
                }

                result.Add(c);
            }

            return result;
        }
    }
}
