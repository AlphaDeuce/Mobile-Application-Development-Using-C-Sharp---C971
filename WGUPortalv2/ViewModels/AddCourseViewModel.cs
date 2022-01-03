using System.Threading.Tasks;
using MvvmHelpers.Commands;
using WGUPortalv2.Services;
using Xamarin.Forms;

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
