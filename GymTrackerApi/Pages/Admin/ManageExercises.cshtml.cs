namespace GymTrackerApi.Pages.Admin
{
    public class ManageExercisesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        //CA1869
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ManageExercisesModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Ensure that the admin is logged in.
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("Index");
            }

            //var client = _httpClientFactory.CreateClient();
            // Custom HttpClient - Bypass SSL Certificate Validation for Development
            var client = _httpClientFactory.CreateClient("DevClient");

            var baseApiUrl = AppConfig.BaseApiUrl;
            var endpoint = $"{baseApiUrl}api/exercises";

            var response = await client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Exercises = JsonSerializer.Deserialize<List<Exercise>>(json, JsonOptions) ?? new List<Exercise>();
            }
            return Page();
        }
    }
}
