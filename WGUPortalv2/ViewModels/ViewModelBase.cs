using System;
using MvvmHelpers;
using MvvmHelpers.Commands;
using WGUPortalv2.Models;
using Xamarin.Forms;

namespace WGUPortalv2.ViewModels
{

    public class ViewModelBase : BaseViewModel
    {

        private static int _termId;
        private static int _courseId;
        private static int _assessmentId;

        // Term ViewModel Base Info:
        public ObservableRangeCollection<Term> Term { get; set; }

        string termTitle;
        DateTime termStartDate, termEndDate;

        public string TermTitle { get => termTitle; set => SetProperty(ref termTitle, value); }
        public DateTime TermStartDate { get => termStartDate; set => SetProperty(ref termStartDate, value); }
        public DateTime TermEndDate { get => termEndDate; set => SetProperty(ref termEndDate, value); }

        // Course ViewModel Base Info:
        public ObservableRangeCollection<Course> Course { get; set; }

        string courseTitle, courseStatus, courseInstructorName, courseInstructorPhone, courseInstructorEmail, courseNotes;
        DateTime courseStartDate, courseEndDate;
        //int termId;
        bool courseNotification;

        public static int TermId { get { return _termId; } set => _termId = value; }
        public string CourseTitle { get => courseTitle; set => SetProperty(ref courseTitle, value); }
        public string CourseStatus { get => courseStatus; set => SetProperty(ref courseStatus, value); }
        public string CourseInstructorPhone { get => courseInstructorPhone; set => SetProperty(ref courseInstructorPhone, value); }
        public string CourseInstructorEmail { get => courseInstructorEmail; set => SetProperty(ref courseInstructorEmail, value); }
        public string CourseNotes { get => courseNotes; set => SetProperty(ref courseNotes, value); }
        public DateTime CourseStartDate { get => courseStartDate; set => SetProperty(ref courseStartDate, value); }
        public DateTime CourseEndDate { get => courseEndDate; set => SetProperty(ref courseEndDate, value); }
        public string CourseInstructorName { get => courseInstructorName; set => SetProperty(ref courseInstructorName, value); }
        public bool CourseNotification { get => courseNotification; set => SetProperty(ref courseNotification, value); }

        // Assessment ViewModel Bas Info:
        public ObservableRangeCollection<Assessment> Assessment { get; set; }

        public string assessmentTitle, assessmentType;
        public DateTime assessmentStartDate, assessmentEndDate;
        public int courseId;
        public bool assessmentNotification;

        public static int AssessmentId { get { return _assessmentId; } set => _assessmentId = value; }
        public static int CourseId { get { return _courseId; } set => _courseId = value; }
        public string AssessmentTitle { get => assessmentTitle; set => SetProperty(ref assessmentTitle, value); }
        public string AssessmentType { get => assessmentType; set => SetProperty(ref assessmentType, value); }
        public DateTime AssessmentStartDate { get => assessmentStartDate; set => SetProperty(ref assessmentStartDate, value); }
        public DateTime AssessmentEndDate { get => AssessmentEndDate; set => SetProperty(ref assessmentEndDate, value); }
        public bool AssessmentNotification { get => AssessmentNotification; set => SetProperty(ref assessmentNotification, value); }

        
        
    }
}
