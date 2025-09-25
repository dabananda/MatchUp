using MatchUp.DTOs;
using MatchUp.Entities;

namespace MatchUp.Interfaces
{
    public interface IUserRepo
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<MemberDto>> GetMembers();
        Task<MemberDto?> GetMember(string username);
    }
}
