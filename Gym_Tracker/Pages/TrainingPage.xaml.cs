namespace Gym_Tracker.Pages;

public partial class TrainingPage : ContentPage
{
	public TrainingPage()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}