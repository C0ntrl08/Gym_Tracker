namespace Gym_Tracker.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Logs in the user using email and password. On success, stores the JWT token.
        /// </summary>
        Task<bool> LoginAsync(string email, string password);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        Task<bool> RegisterAsync(string firstName, string lastName, string email, string password);

        /// <summary>
        /// Clears any stored authentication token.
        /// </summary>
        Task LogoutAsync();

        /// <summary>
        /// Indicates whether the user is authenticated.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Returns the current JWT token (if any).
        /// </summary>
        string AuthToken { get; }

        /// <summary>
        /// Optionally, load any saved token at app start.
        /// </summary>
        Task InitializeAsync();
    }
}
