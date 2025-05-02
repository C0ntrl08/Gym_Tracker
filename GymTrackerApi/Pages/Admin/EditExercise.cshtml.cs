namespace GymTrackerApi.Pages.Admin
{
    public class EditExerciseModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        // Use consistent JSON options for serialization.
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Bind the Exercise property to the form.
        [BindProperty]
        public Exercise Exercise { get; set; } = new Exercise();

        // Indicates whether the update succeeded
        public bool UpdateSucceeded { get; set; }

        // A message to show after a successful update.
        public string? SuccessMessage { get; set; }

        public EditExerciseModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // Retrieve the exercise details using its ID and pre-fill the form.
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Ensure the admin has a valid JWT token.
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("Index");
            }

            var client = _httpClientFactory.CreateClient("DevClient");
            var baseApiUrl = AppConfig.BaseApiUrl;
            var endpoint = $"{baseApiUrl}api/exercises/{id}";

            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Exercise = JsonSerializer.Deserialize<Exercise>(json, JsonOptions) ?? new Exercise();
                return Page();
            }
            else
            {
                // Optional - show an error message
                return RedirectToPage("ManageExercises");
            }
        }

        // Process the form submission by sending a PUT request
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("Index");
            }

            var client = _httpClientFactory.CreateClient("DevClient");
            var baseApiUrl = AppConfig.BaseApiUrl;
            var endpoint = $"{baseApiUrl}api/exercises/{Exercise.Id}";

            var jsonContent = new StringContent(JsonSerializer.Serialize(Exercise),
                Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, endpoint)
            {
                Content = jsonContent
            };

            request.Headers.Add("Authorization", "Bearer " + token);

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                // Instead of redirecting immediately, setting a flag and a success message.
                UpdateSucceeded = true;
                SuccessMessage = "Exercise updated successfully!";
                return Page();
            }

            ModelState.AddModelError(string.Empty, "Error updating exercise.");
            return Page();
        }
    }
}
