using System.Threading.Tasks;
using MvvmHelpers.Commands;
using WGUPortalv2.Services;
using Xamarin.Forms;

namespace WGUPortalv2.ViewModels
{
    public class AddAssessmentViewModel : ViewModelBase
    {
        public AsyncCommand SaveAssessmentCommand { get; }
        public AsyncCommand UpdateAssessmentCommand { get; }

        public AddAssessmentViewModel()
        {
            SaveAssessmentCommand = new AsyncCommand(SaveAssessment);
            UpdateAssessmentCommand = new AsyncCommand(UpdateAssessment);
        }

        async Task SaveAssessment()
        {
            if (string.IsNullOrWhiteSpace(AssessmentTitle) ||
                string.IsNullOrWhiteSpace(AssessmentType))
            {
                return;
            }

            await DatabaseHandler.AddAssessment(CourseId,
                                                AssessmentTitle,
                                                AssessmentStartDate,
                                                AssessmentEndDate,
                                                AssessmentType,
                                                AssessmentNotification);

            await Shell.Current.GoToAsync("..");
        }

        async Task UpdateAssessment()
        {
            await DatabaseHandler.UpdateAssessment(AssessmentId,
                                                  CourseId,
                                                  AssessmentTitle,
                                                  AssessmentStartDate,
                                                  AssessmentEndDate,
                                                  AssessmentType,
                                                  AssessmentNotification);
            await Shell.Current.GoToAsync("..");
        }
    }
}
