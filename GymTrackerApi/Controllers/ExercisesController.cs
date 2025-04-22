namespace GymTrackerApi.Controllers
{
    // This controller server for free users and for Admin users to change the exercises
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

            if (exercise is null)
            {
                return NotFound("Exercise not found");
            }

            return Ok(exercise);
        }

        // PUT: api/Exercises/{id}
        // Admin-only endpoint for editing an exercise.
        // Only a user with Role "Admin" is allowed to access this action.
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditExercise(int id, [FromBody] Exercise updatedExercise)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise is null)
            {
                return NotFound("Exercise not found.");
            }

            // Update the exercise as needed (note: validation omitted for brevity).
            exercise.Name = updatedExercise.Name;
            exercise.Description = updatedExercise.Description;
            exercise.BaseImage = updatedExercise.BaseImage;
            exercise.LongDescription = updatedExercise.LongDescription;
            exercise.VideoUrl = updatedExercise.VideoUrl;
            exercise.Difficulty = updatedExercise.Difficulty;
            // You may also want to update the Category if necessary.

            await _context.SaveChangesAsync();

            return Ok("Exercise updated successfully.");
        }

        // POST: api/Exercises
        // Admin-only endpoint for creating a new exercise.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] Exercise newExercise)
        {
            _context.Exercises.Add(newExercise);
            await _context.SaveChangesAsync();

            // Returns the newly created exercise with status 201 and proper location header.
            return CreatedAtAction(nameof(GetExerciseById), new { id = newExercise.Id }, newExercise);
        }

        // DELETE: api/Exercises/{id}
        // Admin-only endpoint for deleting an exercise.
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return NotFound("Exercise not found.");
            }

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return Ok("Exercise deleted successfully.");
        }
    }
}
