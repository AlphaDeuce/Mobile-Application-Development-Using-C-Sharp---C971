using System;
using System.Collections.Generic;
using System.Windows.Input;
using WGUPortalv2.Models;
using WGUPortalv2.Services;
using WGUPortalv2.ViewModels;
using Xamarin.Forms;

namespace WGUPortalv2.Views
{
    public partial class CourseListPage : ContentPage
    {
        public CourseListPage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = (CourseViewModel)BindingContext;
            await vm.RefreshCommand.ExecuteAsync();
        }
    }
}
