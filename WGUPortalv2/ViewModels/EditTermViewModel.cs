using System;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using WGUPortalv2.Models;
using WGUPortalv2.Services;
using WGUPortalv2.Views;
using Xamarin.Forms;

namespace WGUPortalv2.ViewModels
{
    public class EditTermViewModel : ViewModelBase
    {
        public AsyncCommand<Course> EditCourseCommand { get; }
        public AsyncCommand<Course> RemoveCourseCommand { get; }
        public AsyncCommand UpdateTermCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }

        public EditTermViewModel()
        {

            Course = new ObservableRangeCollection<Course>();

            EditCourseCommand = new AsyncCommand<Course>(Edit);
            RemoveCourseCommand = new AsyncCommand<Course>(Remove);
            UpdateTermCommand = new AsyncCommand(Update);
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);

        }

        Course selectedCourse;
        public Course SelectedCourse
        {
            get => selectedCourse;
            set
            {
                if (value != null)
                {
                    CourseId = value.Id;
                    var route = $"{nameof(CourseDetailsPage)}";
                    Shell.Current.GoToAsync(route);
                }
                SelectedCourse = null;
            }
            
        }

        async Task Edit(Course course)
        {
            CourseId = course.Id;
            var route = $"{nameof(EditCoursePage)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Course course)
        {
            await DatabaseHandler.RemoveCourse(course.Id);
            await Refresh();
        }

        async Task Add()
        {
            var route = $"{nameof(AddCoursePage)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Update()
        {
            await DatabaseHandler.UpdateTerm(TermId, TermTitle, TermStartDate, TermEndDate);
            await Shell.Current.DisplayAlert("", $"{TermTitle} Updated Successfully", "Ok");
            await Shell.Current.GoToAsync("..");
        }


        async Task Refresh()
        {
            IsBusy = true;

            Course.Clear();

            var courses = await DatabaseHandler.GetCourses(TermId);

            Course.AddRange(courses);

            IsBusy = false;

        }
    }
}
