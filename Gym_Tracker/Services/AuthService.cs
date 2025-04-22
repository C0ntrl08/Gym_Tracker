using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using GymTracker_Shared_DTOs;

namespace Gym_Tracker.Services
{
    public class AuthService : IAuthService
    {
        private const string AuthTokenKey = "auth_token";
        // Adjust the base URL if needed.
        private const string BaseUrl = "https://192.168.0.67:7013/api/auth/";

        private readonly HttpClient _httpClient;

        // Holds the current JWT token in memory.
        private string _token = string.Empty;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(_token);
        public string AuthToken => _token;

        /// <summary>
        /// Initialize by loading a token (if one was saved previously).
        /// </summary>
        public async Task InitializeAsync()
        {
            _token = await SecureStorage.GetAsync(AuthTokenKey) ?? string.Empty;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var request = new LoginRequest
                {
                    EmailAddress = email,
                    Password = password
                };

                // Call the backend login endpoint.
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{BaseUrl}login", request);

                if (response.IsSuccessStatusCode)
                {
                    // In our API, we expect an object like { token: "yourToken" }
                    string resultJson = await response.Content.ReadAsStringAsync();
                    using JsonDocument jsonDoc = JsonDocument.Parse(resultJson);
                    if (jsonDoc.RootElement.TryGetProperty("token", out JsonElement tokenElement))
                    {
                        _token = tokenElement.GetString() ?? string.Empty;
                        // Save the token securely on the device.
                        await SecureStorage.SetAsync(AuthTokenKey, _token);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in LoginAsync: {ex}");
            }
            return false;
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string password)
        {
            try
            {
                var request = new RegisterRequest
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email,
                    Password = password
                };

                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{BaseUrl}register", request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in RegisterAsync: {ex}");
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            _token = string.Empty;
            SecureStorage.Remove(AuthTokenKey);
            await Task.CompletedTask;
        }
    }
}
