namespace GymTracker_Shared_DTOs
{
    public class Training
    {
        public int Id { get; set; }
        // Foreign key to the ApplicationUser
        public int ApplicationUserId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        // A training session is composed of multiple training exercises
        public ICollection<TrainingExercise> TrainingExercises { get; set; } = new List<TrainingExercise>();
    }
}
