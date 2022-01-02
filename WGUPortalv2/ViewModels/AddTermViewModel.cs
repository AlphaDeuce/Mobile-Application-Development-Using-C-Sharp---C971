using System;
using System.IO;
using MvvmHelpers.Commands;
using WGUPortalv2.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WGUPortalv2.ViewModels
{
    public class AddTermViewModel : ViewModelBase
    {


        public AsyncCommand SaveTermCommand { get; }

        public AddTermViewModel()
        {
            Title = "Add Term";
            SaveTermCommand = new AsyncCommand(SaveTerm);

        }

        async Task SaveTerm()
        {
            if (string.IsNullOrWhiteSpace(TermTitle) ||
                TermStartDate == null || TermEndDate == null)
            {
                return;
            }

            await DatabaseHandler.AddTerm(TermTitle, TermStartDate, TermEndDate);

            await Shell.Current.GoToAsync("..");
        }
    }
}
