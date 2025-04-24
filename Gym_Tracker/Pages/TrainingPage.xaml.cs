using Gym_Tracker.ViewModels;
namespace Gym_Tracker.Pages;

public partial class TrainingPage : ContentPage
{
    public TrainingPage()
    {
        InitializeComponent();
        // Retrieve the TrainingViewModel from DI.
        BindingContext = Application.Current?.Handler?.MauiContext?.Services
                         .GetRequiredService<TrainingViewModel>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TrainingViewModel viewModel)
        {
            await viewModel.RefreshStatusAsync();
        }
    }

}