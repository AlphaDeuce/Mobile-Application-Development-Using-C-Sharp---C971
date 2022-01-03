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
        public AsyncCommand<Term> EditCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Term> RemoveCommand { get; }

        public TermListViewModel()
        {
            Term = new ObservableRangeCollection<Term>();

            EditCommand = new AsyncCommand<Term>(Edit);
            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Term>(Remove);
        }

        Term selectedTerm;

        public Term SelectedTerm
        {
            get => selectedTerm;
            set
            {
                if(value != null)
                {
                    TermId = value.Id;
                    var route = $"{nameof(TermDetailsPage)}";
                    Shell.Current.GoToAsync(route);
                }
            }
        }

       async Task Edit(Term term)
        {
            TermId = term.Id;
            var route = $"{nameof(EditTermPage)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Refresh()
        {
            IsBusy = true;

            Term.Clear();

            var terms = await DatabaseHandler.GetTerms();

            Term.AddRange(terms);

            IsBusy = false;

        }

        async Task Remove(Term term)
        {
            await DatabaseHandler.RemoveTerm(term.Id);
            await Refresh();
        }

        async Task Add()
        {
            var route = $"{nameof(AddTermPage)}";
            await Shell.Current.GoToAsync(route);
        }

     
    }
}
