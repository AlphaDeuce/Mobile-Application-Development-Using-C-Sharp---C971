using Xamarin.Forms;
using WGUPortalv2.Views;

namespace WGUPortalv2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TermDetailsPage),
                typeof(TermDetailsPage));
            Routing.RegisterRoute(nameof(CourseDetailsPage),
                typeof(CourseDetailsPage));
            Routing.RegisterRoute(nameof(AssessmentDetailsPage),
                typeof(AssessmentDetailsPage));
            Routing.RegisterRoute(nameof(AddTermPage),
                typeof(AddTermPage));
            Routing.RegisterRoute(nameof(AddCoursePage),
                typeof(AddCoursePage));
            Routing.RegisterRoute(nameof(AddAssessmentPage),
                typeof(AddAssessmentPage));
        }

    }
}
