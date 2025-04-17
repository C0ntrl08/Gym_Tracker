using CommunityToolkit.Mvvm.Input;
using Gym_Tracker.Models;
using Gym_Tracker.Pages;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Gym_Tracker.ViewModels
{
    [QueryProperty(nameof(Models.Exercise), "Exercise")]
    public partial class ExerciseDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        Exercise? exercise;

        public string Name { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;

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
