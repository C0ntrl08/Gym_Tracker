namespace GymTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TrainingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveTraining([FromBody] SaveTrainingRequest request)
        {
            // Retrieve the authenticated user's id.
            var userIdClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            // Create a new Training instance.
            var training = new Training
            {
                ApplicationUserId = userId,
                Created = DateTime.UtcNow,
                TrainingExercises = request.Exercises.Select(ex => new TrainingExercise
                {
                    ExerciseId = ex.ExerciseId,
                    Sets = ex.Sets,
                    Repetitions = ex.Repetitions,
                    Weight = ex.Weight,
                    DurationMinutes = ex.DurationMinutes
                }).ToList()
            };

            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();
            return Ok("Training saved successfully");
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetTrainingsForUser()
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            // Retrieve trainings for the authenticated user, including exercise details.
            var trainings = await _context.Trainings
                .Include(t => t.TrainingExercises)
                    .ThenInclude(te => te.Exercise)
                .Where(t => t.ApplicationUserId == userId)
                .ToListAsync();

            return Ok(trainings);
        }
    }
}
