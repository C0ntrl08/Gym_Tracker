namespace GymTrackerApi.DataTransferObjects
{
    // Used during registration. Notice that it contains only the fields the client should provide.
    public class RegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
