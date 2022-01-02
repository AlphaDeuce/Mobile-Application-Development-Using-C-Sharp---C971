using System;
using SQLite;

namespace WGUPortalv2.Models
{
    [Table("Assessments")]
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int CourseId { get; set; }
        public string AssessmentTitle { get; set; }
        public DateTime AssessmentStartDate { get; set; }
        public DateTime AssessmentEndDate { get; set; }
        public string AssessmentType { get; set; }
        public bool NotificationEnabled { get; set; }

        public Assessment()
        {
        }
    }
}
