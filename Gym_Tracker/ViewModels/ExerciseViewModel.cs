using Gym_Tracker.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Gym_Tracker.ViewModels
{
    partial class ExerciseViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ExerciseGroup> GroupedExercises { get; set; }

        private readonly string bodyWeightExercises = "Bodyweight Exercises";
        private readonly string weightTrainingExercises = "Weight Training Exercises";
        private readonly string areobicExercises = "Aerobic Exercises";

        public ExerciseViewModel()
        {
            GroupedExercises = new ObservableCollection<ExerciseGroup>
            {
                new ExerciseGroup(bodyWeightExercises,new List<Exercise>
                {
                    new Exercise
                    {
                        Name = "Push-up",
                        Description = "A basic upper-body exercise targeting chest, shoulders, and triceps.",
                        LongDescription = "Push-ups are a classic exercise to build upper-body strength. They primarily target the chest, shoulders, and triceps, and also engage the core muscles. To perform a push-up, start in a plank position, keep your body straight, and lower yourself until your chest almost touches the floor. Then push back up.",
                        BaseImage = "pullup.png",
                        VideoUrl = "https://www.youtube.com/watch?v=Eh00_rniF8E&t=0s" // Replace with a valid URL
                    },
                    new Exercise { Name = "Plank", Description = "Strengthens the core and stabilizes the spine.", BaseImage = "pullup.png" },
                    new Exercise { Name = "Squat", Description = "Engages lower body muscles, focusing on quads and glutes.", BaseImage = "pullup.png" },
                    new Exercise { Name = "Pull-up", Description = "Builds upper-body strength, mainly in the back and biceps.", BaseImage = "pullup.png" },
                    new Exercise { Name = "Dips", Description = "Targets triceps, chest, and shoulders.", BaseImage = "pullup.png" },
                    new Exercise { Name = "Leg Raises", Description = "Focuses on core strength and lower abdominal muscles.", BaseImage = "pullup.png" },
                    new Exercise { Name = "Mountain Climbers", Description = "A dynamic core and cardio exercise mimicking climbing motion.", BaseImage = "pullup.png" },
                    new Exercise { Name = "L-Sit", Description = "An advanced core isometric exercise.", BaseImage = "pullup.png" },
                    new Exercise { Name = "Handstand", Description = "Builds shoulder and core strength with balance training.", BaseImage = "pullup.png" },
                    new Exercise { Name = "Wall Sit", Description = "Static exercise engaging the quads and core.", BaseImage = "pullup.png" },
                    new Exercise { Name = "Superman Hold", Description = "Strengthens lower back and core while mimicking flight.", BaseImage = "pullup.png" }
                }),
                new ExerciseGroup(weightTrainingExercises,new List<Exercise>
                {
                    new Exercise { Name = "Bench Press", Description = "Develops chest, shoulders, and triceps with heavy resistance.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Deadlift", Description = "A compound exercise targeting back, legs, and glutes.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Barbell Squat", Description = "Builds leg and core strength with loaded resistance.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Shoulder Press", Description = "Strengthens shoulders and upper arms.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Cable Pulldown", Description = "Targets back muscles, mimicking pull-up motion.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Bicep Curl", Description = "Isolates and builds the biceps.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Tricep Pushdown", Description = "Isolates the triceps using a cable machine.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Leg Press", Description = "Targets quads, hamstrings, and glutes.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Incline Dumbbell Press", Description = "Focuses on the upper chest and shoulders.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Lat Pulldown", Description = "Strengthens the back and improves posture.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Face Pull", Description = "Enhances shoulder stability and targets rear delts.", BaseImage = "dumbbell.png" },
                    new Exercise { Name = "Hip Thrust", Description = "Develops glutes with emphasis on hip extension.", BaseImage = "dumbbell.png" }
                }),
                new ExerciseGroup(areobicExercises,new List<Exercise>
                {
                    new Exercise { Name = "Burpee", Description = "A full-body exercise improving cardio and strength.", BaseImage = "running.png" },
                    new Exercise { Name = "Jumping Jacks", Description = "A dynamic cardio exercise.", BaseImage = "running.png" },
                    new Exercise { Name = "Mountain Climbers", Description = "Improves cardio and core strength.", BaseImage = "running.png"}
                })
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
