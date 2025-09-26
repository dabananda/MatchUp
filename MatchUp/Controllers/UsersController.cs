using MatchUp.DTOs;
using MatchUp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchUp.Controllers
{
    [Authorize]
    public class UsersController(IUserRepo userRepo) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var usersDto = await userRepo.GetMembers();
            return Ok(usersDto);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var userDto = await userRepo.GetMember(username);
            if (userDto == null) return NotFound();
            return Ok(userDto);
        }
    }
}
