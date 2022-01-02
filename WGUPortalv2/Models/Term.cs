using System;
using SQLite;

namespace WGUPortalv2.Models
{
    [Table("Terms")]
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string TermTitle { get; set; }
        public DateTime TermStartDate { get; set; }
        public DateTime TermEndDate { get; set ; }

        public Term()
        {
        }
    }
}