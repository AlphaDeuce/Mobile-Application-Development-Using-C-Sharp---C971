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
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }

        public EditTermViewModel()
        {

            Course = new ObservableRangeCollection<Course>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);

        }

        async Task Add()
        {
            var route = $"{nameof(AddCoursePage)}";
            await Shell.Current.GoToAsync(route);
        }


        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(500);

            Course.Clear();

            var courses = await DatabaseHandler.GetCourses(TermId);

            Course.AddRange(courses);

            IsBusy = false;

        }
    }
}
