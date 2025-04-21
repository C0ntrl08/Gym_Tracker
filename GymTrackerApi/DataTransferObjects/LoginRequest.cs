namespace GymTrackerApi.DataTransferObjects
{
    // Used during login.
    public class LoginRequest
    {
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
