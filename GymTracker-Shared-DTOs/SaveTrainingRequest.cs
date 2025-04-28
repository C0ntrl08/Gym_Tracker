namespace GymTracker_Shared_DTOs
{
    public class SaveTrainingRequest
    {
        public List<TrainingExerciseDto> Exercises { get; set; } = new List<TrainingExerciseDto>();
    }
}
