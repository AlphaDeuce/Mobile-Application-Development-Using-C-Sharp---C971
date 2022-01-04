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
        private int termId = ViewModelBase.TermId;
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

        async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            if (CourseMenuItem.Text == "Edit Course")
            {
                CourseTable.IsEnabled = true;
                CourseMenuItem.Text = "Save Changes";
            }
            else if (CourseMenuItem.Text == "Save Changes")
            {
                CourseTable.IsEnabled = false;
                await DatabaseHandler.UpdateCourse(courseId,
                    termId,
                    CourseTitle.Text,
                    CourseStart.Date,
                    CourseEnd.Date,
                    InstructorName.Text,
                    InstructorPhone.Text,
                    InstructorEmail.Text,
                    CourseStatus.SelectedItem.ToString(),
                    CourseNotes.Text, CourseNotification.On);
                await Shell.Current.DisplayAlert($"{CourseTitle.Text} Updated", null, "OK");
            }

        }
    }
}
