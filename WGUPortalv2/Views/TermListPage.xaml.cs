using System;
using System.Collections.Generic;
using System.Windows.Input;
using Plugin.LocalNotifications;
using WGUPortalv2.Models;
using WGUPortalv2.Services;
using WGUPortalv2.ViewModels;
using Xamarin.Forms;

namespace WGUPortalv2.Views
{
    public partial class TermListPage : ContentPage
    {
        private bool pushNotification = true;

        public TermListPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            var vm = (TermListViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync();

            if (pushNotification == true)
            {
                pushNotification = false;
                int courseId = 0;
                foreach (Course course in DatabaseHandler.courseList)
                {
                    courseId++;
                    if (course.CourseNotification == true)
                    {
                        if (course.CourseStartDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Course Notification", $"{course.CourseTitle} begins today!", courseId, DateTime.Now.AddSeconds(5));
                        if (course.CourseEndDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Course Notification", $"{course.CourseTitle} ends today!", courseId, DateTime.Now.AddSeconds(5));
                    }
                }

                int assessmentId = courseId;
                foreach (Assessment assessment in DatabaseHandler.assessmentList)
                {
                    assessmentId++;
                    if (assessment.NotificationEnabled == true)
                    {
                        if (assessment.AssessmentStartDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Assessment Notification", $"{assessment.AssessmentTitle} begins today!", assessmentId);
                        if (assessment.AssessmentEndDate == DateTime.Today)
                            CrossLocalNotifications.Current.Show("Assessment Notification", $"{assessment.AssessmentTitle} ends today!", assessmentId);
                    }
                }
            }

            base.OnAppearing();

        }
    }
}