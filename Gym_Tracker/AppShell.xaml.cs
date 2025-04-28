using Gym_Tracker.Pages;

namespace Gym_Tracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ExerciseDetailPage), typeof(ExerciseDetailPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(TrainingDetailsPage), typeof(TrainingDetailsPage));
        }
    }
}
