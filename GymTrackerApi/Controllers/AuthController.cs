using GymTrackerApi.Services;

namespace GymTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(ApplicationDbContext? context, IConfiguration? configuration, IJwtTokenService jwtTokenService)
        {
            // Ensure DI provides non-null values.
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            _passwordHasher = new PasswordHasher<ApplicationUser>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            // Check if a user with this email already exists.
            if (await _context.Users.AnyAsync(u => u.EmailAddress == registerRequest.EmailAddress))
            {
                return BadRequest("User already exists");
            }

            // Create the new user.
            var user = new ApplicationUser
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                EmailAddress = registerRequest.EmailAddress
            };

            // Hash the incoming password and store it.
            user.HashedPassword = _passwordHasher.HashPassword(user, registerRequest.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Retrieve the user by email.
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == loginRequest.EmailAddress);
            if (user is null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Verify the provided password.
            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, loginRequest.Password);
            if (result != PasswordVerificationResult.Success)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate a JWT token using the JwtTokenService.
            var token = _jwtTokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
