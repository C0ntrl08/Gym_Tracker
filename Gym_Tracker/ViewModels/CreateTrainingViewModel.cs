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

            // Configure HttpClient with handler that bypasses certificate errors during development.
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri($"{AppConfig.BaseApiUrl}api/Training/")
            };

            // Load available exercises (for example, statically here).
            Task.Run(async () => await LoadExercisesAsync());

            // Initialize our collection for added exercises.
            AddedExercises = new ObservableCollection<TrainingExerciseDto>();
        }

        // List of exercises for the Picker.
        public ObservableCollection<Exercise> Exercises { get; } = new ObservableCollection<Exercise>();

        private async Task LoadExercisesAsync()
        {
            try
            {
                // Create a new HttpClient instance specifically for fetching exercises.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
                };

                using var client = new HttpClient(handler)
                {
                    // Append the appropriate endpoint for exercises.
                    BaseAddress = new Uri($"{AppConfig.BaseApiUrl}api/Exercises/")
                };

                // Optionally, if your exercises endpoint requires authentication:
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _authService.AuthToken);

                // Fetch the list of exercises from the backend.
                var exercises = await client.GetFromJsonAsync<List<Exercise>>("");
                if (exercises != null)
                {
                    // Clear any existing items.
                    Exercises.Clear();
                    // Add each fetched exercise to the observable collection.
                    foreach (var exercise in exercises)
                    {
                        Exercises.Add(exercise);
                    }
                }
            }
            catch (Exception ex)
            {
                // Optionally, show an alert if there is an error.
                await Shell.Current.DisplayAlert("Error", $"Failed to load exercises: {ex.Message}", "OK");
            }
        }


        // This collection keeps all the added exercises.
        public ObservableCollection<TrainingExerciseDto> AddedExercises { get; }

        // Input properties for the current exercise entry.
        [ObservableProperty]
        private Exercise? selectedExercise;

        // We use string properties for simplicity, and then parse them when adding.
        [ObservableProperty]
        private string sets = string.Empty;

        [ObservableProperty]
        private string repetitions = string.Empty;

        [ObservableProperty]
        private string weight = string.Empty;

        [ObservableProperty]
        private string durationMinutes = string.Empty;

        // Command for the "Add Exercise" button.
        [RelayCommand]
        public void AddExercise()
        {
            if (SelectedExercise == null)
            {
                Shell.Current.DisplayAlert("Error", "Please select an exercise", "OK");
                return;
            }

            if (!int.TryParse(Sets, out int parsedSets) ||
                !int.TryParse(Repetitions, out int parsedReps) ||
                !float.TryParse(Weight, out float parsedWeight) ||
                !int.TryParse(DurationMinutes, out int parsedDuration))
            {
                Shell.Current.DisplayAlert("Error", "Please enter valid numeric values for all fields.", "OK");
                return;
            }

            // Create a new training exercise entry.
            var trainingExercise = new TrainingExerciseDto
            {
                ExerciseId = SelectedExercise.Id,
                Sets = parsedSets,
                Repetitions = parsedReps,
                Weight = parsedWeight,
                DurationMinutes = parsedDuration
                // Optionally, if you have an ExerciseName in your DTO, you can set it here.
                // ExerciseName = SelectedExercise.Name
            };

            // Add it to the collection.
            AddedExercises.Add(trainingExercise);

            // Clear inputs so the user can add another exercise.
            SelectedExercise = null;
            Sets = "";
            Repetitions = "";
            Weight = "";
            DurationMinutes = "";

            Shell.Current.DisplayAlert("Added", "Exercise added to training", "OK");
        }

        // Command for saving the complete training.
        [RelayCommand]
        public async Task SaveTrainingAsync()
        {
            if (AddedExercises.Count == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Please add at least one exercise.", "OK");
                return;
            }

            var request = new SaveTrainingRequest
            {
                Exercises = AddedExercises.ToList()
            };

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _authService.AuthToken);

                var response = await _httpClient.PostAsJsonAsync("save", request);
                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Success", "Training created successfully", "OK");
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
