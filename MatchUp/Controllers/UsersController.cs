using AutoMapper;
using MatchUp.DTOs;
using MatchUp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchUp.Controllers
{
    [Authorize]
    public class UsersController(IUserRepo userRepo, IMapper mapper) : BaseController
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

        [HttpPut]
        public async Task<ActionResult> UpdateMember(MemberUpdateDto dto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (username == null) return BadRequest("No username found in the token");
            var user = await userRepo.GetUserByUsernameAsync(username);
            if (user == null) return BadRequest("User not found");
            mapper.Map(dto, user);
            if (await userRepo.SaveAllAsync()) return NoContent();
            return BadRequest("Couldn't able to update the user. Please try again.");
        }
    }
}
