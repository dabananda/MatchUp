using MatchUp.Entities;

namespace MatchUp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
