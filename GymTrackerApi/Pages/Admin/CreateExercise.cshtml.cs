using System.Net.Http.Headers;
using System.Text.Json;

namespace GymTrackerApi.Pages.Admin
{
    public class CreateExerciseModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Exercise Exercise { get; set; } = new Exercise();

        public string StatusMessage { get; set; } = "";

        public CreateExerciseModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            // Ensure that admin is logged in.
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        // Handler for the "Create" button.
        public async Task<IActionResult> OnPostCreateAsync()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("Index");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // var client = _httpClientFactory.CreateClient();
            // Custom HttpClient - Bypass SSL Certificate Validation for Development
            var client = _httpClientFactory.CreateClient("DevClient");
            // Attach the token to the request header.
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var baseApiUrl = AppConfig.BaseApiUrl;
            var endpoint = $"{baseApiUrl}api/exercises";

            var json = JsonSerializer.Serialize(Exercise);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(endpoint, content);
            if (response.IsSuccessStatusCode)
            {
                StatusMessage = "Exercise successfully created.";
                ModelState.Clear();
                Exercise = new Exercise();  // Reset fields after a successful creation.
            }
            else
            {
                StatusMessage = "Error when creating exercise, please try again.";
            }
            return Page();
        }

        // Handler for the "Cancel" button.
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("AdminPanel");
        }

    }
}
