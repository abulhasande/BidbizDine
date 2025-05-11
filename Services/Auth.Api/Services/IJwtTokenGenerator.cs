using Auth.Api.Models;

namespace Auth.Api.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
