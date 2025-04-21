namespace GymTrackerApi.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}
