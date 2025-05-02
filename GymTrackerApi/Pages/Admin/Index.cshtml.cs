using System.Text.Json;

namespace GymTrackerApi.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public LoginRequest Login { get; set; } = new LoginRequest();
        public string? ErrorMessage { get; set; }
        public IndexModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public void OnGet()
        {
            // Render login
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create HttpClient to call the backend API.
            // var client = _httpClientFactory.CreateClient();
            // Custom HttpClient - Bypass SSL Certificate Validation for Development
            var client = _httpClientFactory.CreateClient("DevClient");

            // Get base API URL from shared AppConfig.
            var baseApiUrl = AppConfig.BaseApiUrl;
            var endpoint = $"{baseApiUrl}api/auth/login";

            // Serialize the login request.
            var json = JsonSerializer.Serialize(Login);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(endpoint, httpContent);
            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Login failed. Please check your credentials.";
                return Page();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            // Expected response format: { "token": "the_jwt_token" }
            using var doc = JsonDocument.Parse(responseContent);
            if (!doc.RootElement.TryGetProperty("token", out var tokenElement))
            {
                ErrorMessage = "Failed to retrieve token.";
                return Page();
            }

            var token = tokenElement.GetString();
            if (string.IsNullOrEmpty(token))
            {
                ErrorMessage = "Invalid token received.";
                return Page();
            }

            // Decode the token and verify that the user has the Admin role.
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role");
            if (roleClaim == null || roleClaim.Value != "Admin")
            {
                ErrorMessage = "Access denied. You are not an admin.";
                return Page();
            }

            // Save the token in session for subsequent API calls.
            HttpContext.Session.SetString("JwtToken", token);

            // Successful login: navigate to the admin panel.
            return RedirectToPage("AdminPanel");
        }
    }
}
