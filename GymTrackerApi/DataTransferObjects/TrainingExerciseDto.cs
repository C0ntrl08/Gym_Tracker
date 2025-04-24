namespace GymTrackerApi.DataTransferObjects
{
    public class TrainingExerciseDto2
    {
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public float? Weight { get; set; }
        public int? DurationMinutes { get; set; }
    }
}
