using Gym_Tracker.ViewModels;

namespace Gym_Tracker.Pages;

public partial class CreateTrainingPage : ContentPage
{
	public CreateTrainingPage()
	{
		InitializeComponent();
        BindingContext = Application.Current?.Handler?.MauiContext?.Services
                         .GetRequiredService<CreateTrainingViewModel>();
    }
}