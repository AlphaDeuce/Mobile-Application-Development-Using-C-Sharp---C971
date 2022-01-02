using System.Collections.Generic;
using System.Linq;
using WGUPortalv2.Models;
using WGUPortalv2.Services;
using WGUPortalv2.ViewModels;
using Xamarin.Forms;


namespace WGUPortalv2.Views
{
    public partial class EditTermPage : ContentPage
    {
        private IEnumerable<Course> courseList;
        private int termId = ViewModelBase.TermId;
        Term selectedTerm;

        public EditTermPage()
        {
            InitializeComponent();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            selectedTerm = await DatabaseHandler.GetTerm(termId);
            TermTitleLabel.Text = selectedTerm.TermTitle;
            TermStartDatePicker.Date = selectedTerm.TermStartDate;
            TermEndDatePicker.Date = selectedTerm.TermEndDate;

            courseList = await DatabaseHandler.GetCourses(termId);
            if (courseList.Count() > 0)
            {
                courseListView.ItemsSource = courseList;
                noCourseLabel.IsVisible = false;
                return;
            }
        }

    }
}
