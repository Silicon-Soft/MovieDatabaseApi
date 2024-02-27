using Microsoft.AspNetCore.Identity;

namespace MovieDatabaseApi.Repository;

public interface ITokenRepository
{
    string CreateJwtToken(IdentityUser user, List<string> roles);
}