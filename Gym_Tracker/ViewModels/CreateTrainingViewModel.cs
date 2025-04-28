using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Tracker.Services;
using GymTracker_Shared_DTOs;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Gym_Tracker.ViewModels
{
    public partial class CreateTrainingViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly HttpClient _httpClient;

        public CreateTrainingViewModel(IAuthService authService)
        {
            _authService = authService;

            // Configure the HttpClient—if you need to bypass certificate errors during development.
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://192.168.0.67:7013/api/Training/")
            };

            // Load the list of available exercises.
            LoadExercises();
        }

        // Collection for the exercise dropdown (assume Exercise is defined in your shared DTOs)
        public ObservableCollection<Exercise> Exercises { get; } = new ObservableCollection<Exercise>();

        private void LoadExercises()
        {
            // For demonstration, using static data.
            // In a real app, you might fetch this list from a backend API.
            Exercises.Add(new Exercise { Id = 1, Name = "Squat" });
            Exercises.Add(new Exercise { Id = 2, Name = "Bench Press" });
            Exercises.Add(new Exercise { Id = 3, Name = "Deadlift" });
        }

        // Properties for the chosen exercise and input fields.
        [ObservableProperty]
        private Exercise? selectedExercise;

        [ObservableProperty]
        private string sets = string.Empty;

        [ObservableProperty]
        private string repetitions = string.Empty;

        [ObservableProperty]
        private string weight = string.Empty;

        [ObservableProperty]
        private string durationMinutes = string.Empty;

        // Command to save the new training.
        [RelayCommand]
        public async Task SaveTrainingAsync()
        {
            if (SelectedExercise == null)
            {
                await Shell.Current.DisplayAlert("Error", "Please select an exercise", "OK");
                return;
            }

            // Validate and parse the numeric inputs.
            if (!int.TryParse(Sets, out int parsedSets) ||
                !int.TryParse(Repetitions, out int parsedRepetitions) ||
                !float.TryParse(Weight, out float parsedWeight) ||
                !int.TryParse(DurationMinutes, out int parsedDuration))
            {
                await Shell.Current.DisplayAlert("Error", "Please make sure all values are correctly entered.", "OK");
                return;
            }

            // Build the SaveTrainingRequest object. Note: If you moved this DTO to the shared library, use that version.
            var request = new SaveTrainingRequest
            {
                Exercises = new List<TrainingExerciseDto>
                {
                    new TrainingExerciseDto
                    {
                        ExerciseId = SelectedExercise.Id,
                        Sets = parsedSets,
                        Repetitions = parsedRepetitions,
                        Weight = parsedWeight,
                        DurationMinutes = parsedDuration
                    }
                }
            };

            try
            {
                // Set the Authorization header using the token from the authentication service.
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _authService.AuthToken);

                // Post the training to the backend.
                var response = await _httpClient.PostAsJsonAsync("save", request);
                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Success", "Training created successfully", "OK");
                    // Navigate back to the Trainings page.
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Training creation failed", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
