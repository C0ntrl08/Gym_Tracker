using CommunityToolkit.Mvvm.Input;
using Gym_Tracker.Models;
using Gym_Tracker.Pages;
using CommunityToolkit.Mvvm.ComponentModel;

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

        public ExerciseDetailViewModel()
        {
            
        }

        // This partial method is automatically invoked whenever the "Exercise" property is updated.
        partial void OnExerciseChanged(Exercise? value)
        {
            if (value != null)
            {
                Name = value.Name;
                LongDescription = value.LongDescription;
                VideoUrl = value.VideoUrl;

                // Notify the UI that these properties have changed.
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(LongDescription));
                OnPropertyChanged(nameof(VideoUrl));
                OnPropertyChanged(nameof(PageTitle));
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
    }
}
