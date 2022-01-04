using System;
using System.Collections.Generic;
using System.Linq;
using WGUPortalv2.Models;
using WGUPortalv2.Services;
using WGUPortalv2.ViewModels;
using Xamarin.Forms;


namespace WGUPortalv2.Views
{
    public partial class TermDetailsPage : ContentPage
    {
        private IEnumerable<Course> courseList;
        private int termId = ViewModelBase.TermId;
        Term selectedTerm;

        public TermDetailsPage()
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

            courseListView.ItemsSource = null;
            noCourseLabel.IsVisible = true;

            courseList = await DatabaseHandler.GetCourses(termId);
            if (courseList.Count() > 0)
            {
                courseListView.ItemsSource = courseList;
                noCourseLabel.IsVisible = false;
                return;
            }
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (TermMenuItem.Text == "Edit Term")
            {
                TermTitleLabel.IsEnabled = true;
                TermStartDatePicker.IsEnabled = true;
                TermEndDatePicker.IsEnabled = true;
                TermMenuItem.Text = "Save Changes";

            }
            else if (TermMenuItem.Text == "Save Changes")
            {
                TermTitleLabel.IsEnabled = false;
                TermStartDatePicker.IsEnabled = false;
                TermEndDatePicker.IsEnabled = false;
                await DatabaseHandler.UpdateTerm(termId,
                                                TermTitleLabel.Text,
                                                TermStartDatePicker.Date,
                                                TermEndDatePicker.Date);
                await Shell.Current.DisplayAlert($"{TermTitleLabel.Text} Updated",null ,"OK");
                TermMenuItem.Text = "Edit Term";
                OnAppearing();
            }
        }
    }
}
