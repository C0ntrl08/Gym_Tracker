using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Tracker.Pages;
using Gym_Tracker.Services;
using GymTracker_Shared_DTOs;

namespace Gym_Tracker.ViewModels
{
    public partial class TrainingViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string authToken = string.Empty;

        public TrainingViewModel(IAuthService authService)
        {
            _authService = authService;

            Task.Run(async () => await RefreshTokenAsync());
        }

        public async Task RefreshTokenAsync()
        {
            // Reinitialize the AuthService, which loads token from SecureStorage.
            await _authService.InitializeAsync();
            AuthToken = _authService.AuthToken;
        }

        // Command to log out.
        [RelayCommand]
        public async Task LogoutAsync()
        {
            // Clear the token and navigate back to the login page.
            await _authService.LogoutAsync();
            AuthToken = string.Empty; // Clear the displayed token.
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        // Command to navigate to Login.
        [RelayCommand]
        public async Task NavigateToLoginAsync()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }


        // Updating the TrainingPage.xaml
        //// Cache a single instance of JsonSerializerOptions for reuse.
        //private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        //{
        //    PropertyNameCaseInsensitive = true
        //};
        //// The list of trainings (returned from the backend as TrainingDto objects)
        //public ObservableCollection<TrainingExercise> Trainings { get; } = new ObservableCollection<TrainingExercise>();
        //public TrainingViewModel()
        //{
        //    // Start the data loading process.
        //    Task.Run(async () => await LoadTrainingsAsync());
        //}

        //public async Task LoadTrainingsAsync()
        //{
        //    try
        //    {
        //        // Create custom HTTP handler to bypass SSL validation
        //        var handler = new HttpClientHandler
        //        {
        //            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        //        };

        //        // Create HttpClient with custom handler
        //        using HttpClient client = new(handler);

        //        // Set your backend base URL
        //        client.BaseAddress = new Uri("https://192.168.0.67:7013/");

        //        // Call the backend endpoint
        //        HttpResponseMessage response = await client.GetAsync("api/Exercises");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string json = await response.Content.ReadAsStringAsync();
        //            // Reuse the cached JsonOptions instance for deserialization.
        //            var trainings = JsonSerializer.Deserialize<List<TrainingExercise>>(json, JsonOptions);

        //            // Update the observable collection
        //            Trainings.Clear();
        //            if (trainings != null)
        //            {
        //                foreach (var training in trainings)
        //                {
        //                    Trainings.Add(training);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Improved error logging
        //        Debug.WriteLine($"Error fetching trainings: {ex}");
        //    }

        //}

        //public event PropertyChangedEventHandler? PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
