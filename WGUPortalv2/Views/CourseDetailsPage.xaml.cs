using System;
using System.Collections.Generic;
using System.Linq;
using WGUPortalv2.Models;
using WGUPortalv2.Services;
using WGUPortalv2.ViewModels;
using Xamarin.Forms;

namespace WGUPortalv2.Views
{
    public partial class CourseDetailsPage : ContentPage
    {
        private IEnumerable<Assessment> assessmentList;
        private int courseId = ViewModelBase.CourseId;
        Course selectedCourse;

        public CourseDetailsPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            selectedCourse = await DatabaseHandler.GetCourse(courseId);

            CourseTitle.Text = selectedCourse.CourseTitle;
            CourseStart.Date = selectedCourse.CourseStartDate;
            CourseEnd.Date = selectedCourse.CourseEndDate;
            CourseStatus.SelectedItem = selectedCourse.CourseStatus;
            InstructorName.Text = selectedCourse.CourseInstructorName;
            InstructorPhone.Text = selectedCourse.CourseInstructorPhone;
            InstructorEmail.Text = selectedCourse.CourseInstructorEmail;
            CourseNotes.Text = selectedCourse.CourseNotes;
            CourseNotification.On = selectedCourse.CourseNotification;

            assessmentList = await DatabaseHandler.GetAssessments(courseId);
            if (assessmentList.Count() > 0)
            {
                assessmentListView.ItemsSource = assessmentList;
                noAssessmentLabel.IsVisible = false;
                return;
            }
        }

    }
}
