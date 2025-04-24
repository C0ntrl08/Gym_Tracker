using Gym_Tracker.ViewModels;
namespace Gym_Tracker.Pages;

public partial class TrainingPage : ContentPage
{
	public TrainingPage()
	{
		InitializeComponent();
        BindingContext = Application.Current?.Handler?.MauiContext?.Services
                             .GetRequiredService<TrainingViewModel>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TrainingViewModel vm)
        {
            await vm.RefreshStatusAsync();
        }
    }

}