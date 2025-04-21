namespace GymTrackerApi.DataTransferObjects
{
    public class SaveTrainingRequest
    {
        public List<TrainingExerciseDto> Exercises { get; set; } = new List<TrainingExerciseDto>();
    }
}
