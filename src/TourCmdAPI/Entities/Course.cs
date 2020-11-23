using System.Collections.Generic;

namespace TourCmdAPI.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseId { get; set; }
        public virtual List<StudentCourse> StudentCourses { get; set; }
    }
}