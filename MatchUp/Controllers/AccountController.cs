using MatchUp.Data;
using MatchUp.DTOs;
using MatchUp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace MatchUp.Controllers
{
    public class AccountController(DataContext context) : BaseController
    {
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto dto)
        {
            var username = dto.Username.ToLower();
            if (await IsUserExist(username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> IsUserExist(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username);
        }
    }
}
