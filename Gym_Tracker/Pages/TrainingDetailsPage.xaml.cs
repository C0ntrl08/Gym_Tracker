using Gym_Tracker.ViewModels;
namespace Gym_Tracker.Pages;

public partial class TrainingDetailsPage : ContentPage
{
	public TrainingDetailsPage()
	{
		InitializeComponent();
        BindingContext = Application.Current?.Handler?.MauiContext?.Services
                             .GetRequiredService<TrainingDetailsViewModel>();
    }
}