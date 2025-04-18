namespace Gym_Tracker.Models
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    public class Exercise
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BaseImage { get; set; } = string.Empty;
        // Adding new Properties to implement the DetailsPage feature
        public string LongDescription { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public DifficultyLevel Difficulty { get; set; }
    }
}
