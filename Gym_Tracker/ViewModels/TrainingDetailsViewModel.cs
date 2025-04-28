using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymTracker_Shared_DTOs;
using System.Text.Json;

namespace Gym_Tracker.ViewModels
{
    [QueryProperty(nameof(TrainingJson), "trainingJson")]
    public partial class TrainingDetailsViewModel : ObservableObject
    {
        // Initialize backing field with an empty string.
        private string trainingJson = string.Empty;
        public string TrainingJson
        {
            get => trainingJson;
            set
            {
                trainingJson = value;
                // Deserialize the JSON string into a TrainingDto.
                Training = JsonSerializer.Deserialize<TrainingDto>(trainingJson) ?? new TrainingDto();
                OnPropertyChanged(nameof(Training));
            }
        }

        // Initialize with a new TrainingDto to ensure non-nullability.
        private TrainingDto training = new TrainingDto();
        public TrainingDto Training
        {
            get => training;
            set => SetProperty(ref training, value);
        }

        // Command to close the modal page.
        [RelayCommand]
        public static async Task CloseAsync()
        {
            // Navigate back (closing the modal page)
            await Shell.Current.GoToAsync("..");
        }
    }
}
