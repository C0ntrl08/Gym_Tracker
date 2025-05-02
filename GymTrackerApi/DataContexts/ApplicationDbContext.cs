namespace GymTrackerApi.DataContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingExercise> TrainingExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed literal exercises.
            modelBuilder.Entity<Exercise>().HasData(
                // Bodyweight Exercises (Group: "Bodyweight Exercises")
                new Exercise
                {
                    Id = 1,
                    Name = "Push-up",
                    Description = "A basic upper-body exercise targeting chest, shoulders, and triceps.",
                    LongDescription = "Push-ups are a classic exercise to build upper-body strength. They primarily target the chest, shoulders, and triceps, and also engage the core muscles. To perform a push-up, start in a plank position, keep your body straight, and lower yourself until your chest almost touches the floor. Then push back up.",
                    BaseImage = "pullup.png",
                    VideoUrl = "https://www.youtube.com/watch?v=Eh00_rniF8E&t=0s",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 2,
                    Name = "Plank",
                    Description = "Strengthens the core and stabilizes the spine.",
                    LongDescription = "Plank is lorem ipsum",
                    BaseImage = "pullup.png",
                    VideoUrl = "https://www.youtube.com/watch?v=u6ZelKyUM6g",
                    Difficulty = DifficultyLevel.Medium,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 3,
                    Name = "Squat",
                    Description = "Engages lower body muscles, focusing on quads and glutes.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Hard,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 4,
                    Name = "Pull-up",
                    Description = "Builds upper-body strength, mainly in the back and biceps.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 5,
                    Name = "Dips",
                    Description = "Targets triceps, chest, and shoulders.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 6,
                    Name = "Leg Raises",
                    Description = "Focuses on core strength and lower abdominal muscles.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 7,
                    Name = "Mountain Climbers",
                    Description = "A dynamic core and cardio exercise mimicking climbing motion.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 8,
                    Name = "L-Sit",
                    Description = "An advanced core isometric exercise.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 9,
                    Name = "Handstand",
                    Description = "Builds shoulder and core strength with balance training.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 10,
                    Name = "Wall Sit",
                    Description = "Static exercise engaging the quads and core.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },
                new Exercise
                {
                    Id = 11,
                    Name = "Superman Hold",
                    Description = "Strengthens lower back and core while mimicking flight.",
                    LongDescription = "",
                    BaseImage = "pullup.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Bodyweight Exercises"
                },

                // Weight Training Exercises (Group: "Weight Training Exercises")
                new Exercise
                {
                    Id = 12,
                    Name = "Bench Press",
                    Description = "Develops chest, shoulders, and triceps with heavy resistance.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 13,
                    Name = "Deadlift",
                    Description = "A compound exercise targeting back, legs, and glutes.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 14,
                    Name = "Barbell Squat",
                    Description = "Builds leg and core strength with loaded resistance.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 15,
                    Name = "Shoulder Press",
                    Description = "Strengthens shoulders and upper arms.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 16,
                    Name = "Cable Pulldown",
                    Description = "Targets back muscles, mimicking pull-up motion.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 17,
                    Name = "Bicep Curl",
                    Description = "Isolates and builds the biceps.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 18,
                    Name = "Tricep Pushdown",
                    Description = "Isolates the triceps using a cable machine.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 19,
                    Name = "Leg Press",
                    Description = "Targets quads, hamstrings, and glutes.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 20,
                    Name = "Incline Dumbbell Press",
                    Description = "Focuses on the upper chest and shoulders.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 21,
                    Name = "Lat Pulldown",
                    Description = "Strengthens the back and improves posture.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 22,
                    Name = "Face Pull",
                    Description = "Enhances shoulder stability and targets rear delts.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },
                new Exercise
                {
                    Id = 23,
                    Name = "Hip Thrust",
                    Description = "Develops glutes with emphasis on hip extension.",
                    LongDescription = "",
                    BaseImage = "dumbbell.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Weight Training Exercises"
                },

                // Aerobic Exercises (Group: "Aerobic Exercises")
                new Exercise
                {
                    Id = 24,
                    Name = "Burpee",
                    Description = "A full-body exercise improving cardio and strength.",
                    LongDescription = "",
                    BaseImage = "running.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Aerobic Exercises"
                },
                new Exercise
                {
                    Id = 25,
                    Name = "Jumping Jacks",
                    Description = "A dynamic cardio exercise.",
                    LongDescription = "",
                    BaseImage = "running.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Aerobic Exercises"
                },
                new Exercise
                {
                    Id = 26,
                    Name = "Mountain Climbers",
                    Description = "Improves cardio and core strength.",
                    LongDescription = "",
                    BaseImage = "running.png",
                    VideoUrl = "",
                    Difficulty = DifficultyLevel.Easy,
                    Category = "Aerobic Exercises"
                }
            );
        }
    }
}
