using System;
using System.IO;
using MvvmHelpers.Commands;
using WGUPortalv2.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using WGUPortalv2.Models;
using WGUPortalv2.Views;

namespace WGUPortalv2.ViewModels
{
   
    public class AddCourseViewModel : ViewModelBase
    {

        public AsyncCommand SaveCourseCommand { get; }
        
        public AddCourseViewModel()
        {
            SaveCourseCommand = new AsyncCommand(SaveCourse);
            
        }

        async Task SaveCourse()
        {
            if (string.IsNullOrWhiteSpace(CourseTitle) ||
                string.IsNullOrWhiteSpace(CourseStatus) ||
                string.IsNullOrWhiteSpace(CourseInstructorName) ||
                string.IsNullOrWhiteSpace(CourseInstructorPhone) ||
                string.IsNullOrWhiteSpace(CourseInstructorEmail))

            {
                return;
            }

            await DatabaseHandler.AddCourse(TermId,
                                            CourseTitle,
                                            CourseStartDate,
                                            CourseEndDate,
                                            CourseInstructorName,
                                            CourseInstructorPhone,
                                            CourseInstructorEmail,
                                            CourseStatus,
                                            CourseNotes,
                                            CourseNotification);

            await Shell.Current.GoToAsync("..");
        }

    }
}
