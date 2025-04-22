namespace GymTracker_Shared_DTOs
{
    public class TrainingDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public List<TrainingExerciseDto> Exercises { get; set; } = new List<TrainingExerciseDto>();
    }
}
