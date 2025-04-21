namespace GymTrackerApi.Controllers
{
    // This controller is for free users, without any Authorisation
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Exercises
        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var exercises = await _context.Exercises.ToListAsync();
            return Ok(exercises);
        }

        // GET: api/Exercises/{id}
        // Returns the details of a specific exercise by its id.
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExerciseById(int id)
        {
            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exercise == null)
            {
                return NotFound("Exercise not found");
            }

            return Ok(exercise);
        }
    }
}
