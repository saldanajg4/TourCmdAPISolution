using System.Collections.Generic;

namespace TourCmdAPI.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<StudentCourse> StudentCourses { get; set; }
    }
}