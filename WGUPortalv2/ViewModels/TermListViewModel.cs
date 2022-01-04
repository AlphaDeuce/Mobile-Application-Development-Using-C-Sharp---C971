using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using WGUPortalv2.Models;
using WGUPortalv2.Services;
using WGUPortalv2.Views;
using Xamarin.Forms;

namespace WGUPortalv2.ViewModels
{
    public class TermListViewModel : ViewModelBase
    {
  
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddTermCommand { get; }
 
        public TermListViewModel()
        {
            Term = new ObservableRangeCollection<Term>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddTermCommand = new AsyncCommand(AddTerm);
        }

        Term selectedTerm;

        public Term SelectedTerm
        {
            get => selectedTerm;
            set
            {
                if (value != null)
                {
                    TermId = value.Id;
                    var route = $"{nameof(TermDetailsPage)}";
                    Shell.Current.GoToAsync(route);
                }
                selectedTerm = value;
                OnPropertyChanged();
            }
        }

        async Task Refresh()
        {
            IsBusy = true;

            Term.Clear();

            var terms = await DatabaseHandler.GetTerms();

            Term.AddRange(terms);

            IsBusy = false;

        }

        async Task AddTerm()
        {
            var route = $"{nameof(AddTermPage)}";
            await Shell.Current.GoToAsync(route);
        }

     
    }
}
