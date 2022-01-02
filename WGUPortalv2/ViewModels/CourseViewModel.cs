using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using WGUPortalv2.Models;
using WGUPortalv2.Services;
using WGUPortalv2.Views;
using Xamarin.Forms;

namespace WGUPortalv2.ViewModels
{
    public class CourseViewModel : ViewModelBase
    {
        public AsyncCommand<Course> EditCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Course> RemoveCommand { get; }

        public CourseViewModel()
        {

            EditCommand = new AsyncCommand<Course>(Edit);
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Course>(Remove);


            Course = new ObservableRangeCollection<Course>();

            //Course.Add(new Course { CourseTitle = "Course One", TermId = 1, CourseStartDate = DateTime.Parse("2020, 01, 01"), CourseEndDate = DateTime.Parse("2020, 06, 30") });
            //Course.Add(new Course { CourseTitle = "Course Two", TermId = 1, CourseStartDate = DateTime.Parse("2020, 01, 01"), CourseEndDate = DateTime.Parse("2020, 06, 30") });
            //Course.Add(new Course { CourseTitle = "Course Three", TermId = 1, CourseStartDate = DateTime.Parse("2020, 01, 01"), CourseEndDate = DateTime.Parse("2020, 06, 30") });
            //Course.Add(new Course { CourseTitle = "Course Four", TermId = 1, CourseStartDate = DateTime.Parse("2020, 01, 01"), CourseEndDate = DateTime.Parse("2020, 06, 30") });

            //Course.Add(new Course { CourseTitle = "Course 6", TermId = 3, CourseStartDate = DateTime.Parse("2020, 01, 01"), CourseEndDate = DateTime.Parse("2020, 06, 30") });
            //Course.Add(new Course { CourseTitle = "Course 7", TermId = 3, CourseStartDate = DateTime.Parse("2020, 01, 01"), CourseEndDate = DateTime.Parse("2020, 06, 30") });
            //Course.Add(new Course { CourseTitle = "Course 8", TermId = 2, CourseStartDate = DateTime.Parse("2020, 01, 01"), CourseEndDate = DateTime.Parse("2020, 06, 30") });
            //Course.Add(new Course { CourseTitle = "Course 9", TermId = 2, CourseStartDate = DateTime.Parse("2020, 01, 01"), CourseEndDate = DateTime.Parse("2020, 06, 30") });

        }


        async Task Edit(Course course)
        {
            var route = $"{nameof(CourseDetailsPage)}?CourseId={course.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(500);

            Course.Clear();

            var courses = await DatabaseHandler.GetCourses();

            Course.AddRange(courses);

            IsBusy = false;

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


    }
}
