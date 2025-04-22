namespace GymTracker_Shared_DTOs
{
    public class TrainingExercise
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public int ExerciseId { get; set; }

        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public float? Weight { get; set; }
        public int? DurationMinutes { get; set; }

        // Navigation properties: EF Core will link these automatically
        public Training? Training { get; set; }
        public Exercise? Exercise { get; set; }
    }
}
