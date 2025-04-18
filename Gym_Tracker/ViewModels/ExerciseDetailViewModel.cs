using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Gym_Tracker.Models;
using Gym_Tracker.Pages;
using Microsoft.Maui.Networking;

namespace Gym_Tracker.ViewModels
{
    [QueryProperty(nameof(Exercise), nameof(Exercise))]
    public partial class ExerciseDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        Exercise? exercise;

        
        public string Name { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        // Computed property that concatenates Name with " Details"
        public string PageTitle => string.IsNullOrEmpty(Name) ? "Details" : $"{Name} Details";
        [ObservableProperty]
        private bool isOnline;

        // DifficultyLevel
        public int DifficultyValue { get; set; } // 1 for Easy, 2 for Medium, 3 for Hard
        public string DifficultyText { get; set; } = string.Empty;

        public ExerciseDetailViewModel()
        {
            // Check connectivity at startup
            CheckConnectivity();

            // Subscribe to connectivity changes
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            // Update our property when connection changes
            IsOnline = e.NetworkAccess == NetworkAccess.Internet;
        }

        private void CheckConnectivity()
        {
            IsOnline = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;
        }

        // This partial method is automatically invoked whenever the "Exercise" property is updated.
        partial void OnExerciseChanged(Exercise? value)
        {
            if (value != null)
            {
                Name = value.Name;
                LongDescription = value.LongDescription;
                VideoUrl = value.VideoUrl;

                // Map the Difficulty enum to our properties
                DifficultyValue = value.Difficulty switch
                {
                    DifficultyLevel.Easy => 1,
                    DifficultyLevel.Medium => 2,
                    DifficultyLevel.Hard => 3,
                    _ => 1,
                };
                DifficultyText = value.Difficulty.ToString();

                // Notify the UI that these properties have changed.
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(LongDescription));
                OnPropertyChanged(nameof(VideoUrl));
                OnPropertyChanged(nameof(PageTitle));
                OnPropertyChanged(nameof(DifficultyValue));
                OnPropertyChanged(nameof(DifficultyText));
            }
        }

        [RelayCommand]
        async Task GoToDetails(Exercise exercise)
        {
            if (exercise is null)
            {
                return;
            }

            await Shell.Current.GoToAsync(nameof(ExerciseDetailPage), true, new Dictionary<string, object>
            {
                {"Exercise", exercise }
            });
        }

        // Command to refresh connectivity status when the user taps the Refresh button.
        [RelayCommand]
        async Task RefreshConnectivity()
        {
            CheckConnectivity();

            if (!IsOnline)
            {
                await Shell.Current.DisplayAlert("No Connection",
                    "No internet connection detected. Please check your connection.",
                    "OK");
            }
        }
    }
}
