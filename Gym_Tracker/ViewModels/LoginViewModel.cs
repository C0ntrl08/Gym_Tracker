using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Tracker.Services;

namespace Gym_Tracker.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                // Use Shell.Current.DisplayAlert to show error feedback.
                await Shell.Current.DisplayAlert("Error", "Please enter both email and password.", "OK");
                return;
            }

            // Call the authentication service to login.
            bool loggedIn = await _authService.LoginAsync(Email, Password);
            if (loggedIn)
            {
                // Navigate to the previous page or a specific page after successful login.
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Login Failed", "Invalid credentials. Please try again.", "OK");
            }
        }
    }
}
