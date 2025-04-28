namespace GymTracker_Shared_DTOs
{
    public class TrainingExerciseDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; } = string.Empty;
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public float? Weight { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
