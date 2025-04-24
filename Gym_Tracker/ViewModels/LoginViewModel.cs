using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Tracker.Services;
using System.Diagnostics;

namespace Gym_Tracker.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        // Auto-implemented properties with change notification.
        [ObservableProperty]
        private string emailAddress = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        // This service handles login, token storage, etc.
        private readonly IAuthService _authService;

        // Inject the AuthService via constructor injection.
        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        // This RelayCommand-bound method is triggered when the Login button is pressed.
        [RelayCommand]
        public async Task LoginUserAsync()
        {
            try
            {
                // Call the AuthService to perform login.
                bool loginSuccess = await _authService.LoginAsync(emailAddress, Password);
                if (loginSuccess)
                {
                    // Show success popup.
                    await Application.Current!.MainPage!.DisplayAlert("Success", "Login successful", "OK");

                    // Clear the input fields.
                    EmailAddress = string.Empty;
                    Password = string.Empty;

                    // Optional: You can also perform additional operations with the JWT token here.
                    // For example: string currentToken = _authService.AuthToken;

                    // Navigate back (pop the modal), e.g. back to the TrainingPage.
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    // If login failed, display a failure popup.
                    await Application.Current!.MainPage!.DisplayAlert("Error", "Login failed. Try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during login: {ex.Message}");
                await Application.Current!.MainPage!.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
