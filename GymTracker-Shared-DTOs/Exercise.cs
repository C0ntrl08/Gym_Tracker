namespace GymTracker_Shared_DTOs
{
    public enum DifficultyLevel
    {
        Easy,  //default
        Medium,
        Hard
    }
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BaseImage { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public DifficultyLevel Difficulty { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
