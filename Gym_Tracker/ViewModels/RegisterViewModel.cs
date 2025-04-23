using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTracker_Shared_DTOs;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Gym_Tracker.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        // Cached JSON serializer options (similar to your TrainingViewModel).
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        [ObservableProperty]
        private string firstName = string.Empty;

        [ObservableProperty]
        private string lastName = string.Empty;

        [ObservableProperty]
        private string emailAddress = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [RelayCommand]
        public async Task RegisterUserAsync()
        {
            // Prepare the register request DTO using the data input by the user.
            var registerRequest = new RegisterRequest
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                EmailAddress = this.EmailAddress,
                Password = this.Password
            };

            // Create a custom HTTP handler to bypass SSL validation when debugging on a physical device.
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            using HttpClient client = new HttpClient(handler)
            {
                // Adjust the BaseAddress to match your backend's URL.
                BaseAddress = new Uri("https://192.168.0.67:7013/")
            };

            // Serialize the DTO to JSON.
            string json = JsonSerializer.Serialize(registerRequest, JsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Post the register request to the API.
                HttpResponseMessage response = await client.PostAsync("api/Auth/register", content);
                if (response.IsSuccessStatusCode)
                {
                    // Handle successful registration. For instance, navigate to a login page or display a success message.
                    // Display a popup alert indicating successful registration.
                    await Application.Current!.MainPage!.DisplayAlert("Success", "Registration succeeded", "OK");
                    // Clear the registration fields so another user can register.
                    FirstName = string.Empty;
                    LastName = string.Empty;
                    EmailAddress = string.Empty;
                    Password = string.Empty;
                }
                else
                {
                    // Optionally, show an error alert. You can also parse the response content for detailed messages.
                    string responseContent = await response.Content.ReadAsStringAsync();
                    await Application.Current!.MainPage!.DisplayAlert("Error",
                        $"Registration failed: {response.StatusCode}\n{responseContent}", "OK");
                    Debug.WriteLine($"Registration failed: {response.StatusCode}, {responseContent}");
                }
            }
            catch (Exception ex)
            {
                // Alert the user if an exception occurs and log the error.
                await Application.Current!.MainPage!.DisplayAlert("Error",
                    $"An error occurred during registration: {ex.Message}", "OK");
                Debug.WriteLine($"Error during registration: {ex.Message}");
            }
        }
    }
}
