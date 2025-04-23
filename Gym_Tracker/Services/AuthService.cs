using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using GymTracker_Shared_DTOs;

namespace Gym_Tracker.Services
{
    public class AuthService : IAuthService
    {
        private const string AuthTokenKey = "MySuperSecretKeyHereMySuperSecretKeyHereMySuperSecretKeyHereMySuperSecretKeyHere";
        private const string BaseUrl = "https://192.168.0.67:7013/api/auth/";

        private string _token = string.Empty;

        public bool IsAuthenticated => !string.IsNullOrEmpty(_token);
        public string AuthToken => _token;

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

                // Create a custom HTTP handler to bypass SSL/TLS validation for debugging.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
                };

                using var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(BaseUrl)
                };

                // Send POST request to the login endpoint.
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("login", request);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    using JsonDocument jsonDoc = JsonDocument.Parse(resultJson);
                    if (jsonDoc.RootElement.TryGetProperty("token", out JsonElement tokenElement))
                    {
                        _token = tokenElement.GetString() ?? string.Empty;

                        // Store the JWT securely.
                        await SecureStorage.SetAsync(AuthTokenKey, _token);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoginAsync: {ex}");
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

                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
                };

                using var httpClient = new HttpClient(handler)
                {
                    BaseAddress = new Uri(BaseUrl)
                };

                HttpResponseMessage response = await httpClient.PostAsJsonAsync("register", request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in RegisterAsync: {ex}");
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
