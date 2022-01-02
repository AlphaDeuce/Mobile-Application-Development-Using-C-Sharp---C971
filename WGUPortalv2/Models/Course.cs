using System;
using SQLite;

namespace WGUPortalv2.Models
{
    [Table("Courses")]
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int TermId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseStatus { get; set; }
        public string CourseInstructorName { get; set; }
        public string CourseInstructorPhone { get; set; }
        public string CourseInstructorEmail { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
        public string CourseNotes { get; set; }
        public bool CourseNotification { get; set; }


        public Course()
        {
        }
    }
}
