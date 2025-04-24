namespace GymTrackerApi.DataTransferObjects
{
    public class TrainingDto2
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public List<TrainingExerciseDto> Exercises { get; set; } = new List<TrainingExerciseDto>();
    }
}
