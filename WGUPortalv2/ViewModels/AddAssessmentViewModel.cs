using System.Threading.Tasks;
using MvvmHelpers.Commands;
using WGUPortalv2.Services;
using Xamarin.Forms;

namespace WGUPortalv2.ViewModels
{
    public class AddAssessmentViewModel : ViewModelBase
    {
        public AsyncCommand SaveAssessmentCommand { get; }

        public AddAssessmentViewModel()
        {
            SaveAssessmentCommand = new AsyncCommand(SaveAssessment);
        }

        async Task SaveAssessment()
        {
            if (string.IsNullOrWhiteSpace(assessmentTitle) ||
                string.IsNullOrWhiteSpace(assessmentType))
            {
                return;
            }

            await DatabaseHandler.AddAssessment(courseId,
                                                assessmentTitle,
                                                assessmentStartDate,
                                                assessmentEndDate,
                                                assessmentType,
                                                assessmentNotification);

            await Shell.Current.GoToAsync("..");
        }
    }
}
